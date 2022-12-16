using SaveDataGrid.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveDataGrid.DataGridStorage
{
	public class DgOptionsVM
	{
		public List<DgSettingsItem> SettingsList { get; }
		private Action _updateSettingsToGridCallback;


		public DgOptionsVM(List<DgSettingsItem> settingsList, Action updateSettingsToGridCallback)
		{
			SettingsList = settingsList;
			_updateSettingsToGridCallback = updateSettingsToGridCallback;
		}


		RelayCommand _UseCommand;
		public RelayCommand UseCommand => _UseCommand ?? (_UseCommand = new(_updateSettingsToGridCallback));
	}
}
