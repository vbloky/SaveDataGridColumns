using SaveDataGrid.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SaveDataGrid.DataGridStorage
{
	public class DgSettings : IDisposable
	{
		private Window _parentWindow;
		private DataGrid _grid;
		private DgSerializer _serializer;
		public List<DgSettingsItem> SettingsList { get; private set; } = new();

		public DgSettings(DataGrid _dataGrid, string storageFileName)
		{
			_serializer = new(storageFileName);
			_grid = _dataGrid;

			_grid.Loaded += _grid_Loaded;

			if ((_parentWindow = Window.GetWindow(_grid)) != null)
				_parentWindow.Closing += _gridParentWndClosing;
		}


		public void LoadSettings()
		{
			SettingsList = _serializer.Deserialize();
			UpdateSettingsToGrid();
		}
		public void SaveSettings()
		{
			GetSettingsFromGrid();
			_serializer.Serialize(SettingsList);
		}
		public void ShowDataGridOptionsWindow()
		{
			GetSettingsFromGrid();
			var vm = new DgOptionsVM(SettingsList, UpdateSettingsToGrid);
			var wnd = new DgOptionsWnd();
			wnd.DataContext = vm;
			if (wnd.ShowDialog() == true)
				UpdateSettingsToGrid();
		}
		public void Dispose()
		{
			_grid.Loaded -= _grid_Loaded;
			if (_parentWindow != null)
				_parentWindow.Closing -= _gridParentWndClosing;
			_grid = null;
			_parentWindow = null;
			SettingsList.Clear();
		}



		private void _grid_Loaded(object sender, RoutedEventArgs e) => LoadSettings();
		//The unloaded event is fired only if app is not closing!
		private void _grid_Unloaded(object sender, RoutedEventArgs e) => SaveSettings();
		private void _gridParentWndClosing(object? sender, CancelEventArgs e) => SaveSettings();

		private void GetSettingsFromGrid()
		{
			for (int i = 0; i < _grid.Columns.Count; i++)
			{
				var item = getColumnSettings(i);
				if (item.Index < SettingsList.Count)
					//update values in column setting, but don't copy visibility to user
					SettingsList[item.Index].CopyPropertiesExclude(item, nameof(item.IsVisibleToUser));
				else
					//add new item to column settings
					SettingsList.Add(item);
			}


			DgSettingsItem getColumnSettings(int colIdx)
			{
				var col = _grid.Columns[colIdx];
				return new DgSettingsItem()
				{
					Index = colIdx,
					Header = (string)col.Header,
					DisplayIndex = col.DisplayIndex,
					Width = col.ActualWidth,
					IsVisible = col.Visibility == Visibility.Visible,
					IsVisibleToUser = true,
				};
			}
		}


		private void UpdateSettingsToGrid()
		{
			int count = Math.Min(SettingsList.Count, _grid.Columns.Count);

			//Because setting DisplayIndex changes the order of all other higher DisplayIndexes,
			//DisplayIndexes must be set from lowest to highest
			foreach (var src in SettingsList.OrderBy(p => p.DisplayIndex))
			{
				if (src.Index > 0 && src.Index < count)
				{
					_grid.Columns[src.Index].Visibility = src.IsVisible ? Visibility.Visible : Visibility.Collapsed;
					_grid.Columns[src.Index].DisplayIndex = src.DisplayIndex;
					_grid.Columns[src.Index].Width = new DataGridLength(src.Width, DataGridLengthUnitType.Pixel);
				}
			}
		}
	}
}
