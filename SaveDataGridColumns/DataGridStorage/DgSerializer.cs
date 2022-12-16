using SaveDataGrid.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveDataGrid.DataGridStorage
{
	public class DgSerializer : BaseSerializer<List<DgSettingsItem>>
	{
		public DgSerializer(string fileName) 
			: base(BaseApp.UserDataLocation + "\\" + fileName + ".json")
		{
		}
	}
}
