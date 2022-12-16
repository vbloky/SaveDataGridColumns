using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SaveDataGrid.ViewModels
{

	public partial class MainVM
	{
		public ObservableCollection<GridItem> DataList { get; set; } = new();
		public MainVM()
		{
			CreateDemoData();
		}


		private MainVM CreateDemoData()
		{
			DataList.Add(new GridItem("Peter", "Holly", 180));
			DataList.Add(new GridItem("Joseph", "Addler", 182));
			DataList.Add(new GridItem("Jacob", "New", 185));
			DataList.Add(new GridItem("Jannet", "Hoss", 168));

			return this;
		}
	}
}
