using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SaveDataGrid.Lib
{
	public class BaseApp
	{
		//where to store user data?
		public static bool StoreToAppExeLocation = true;

		public static string AppFullName => System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

		public static string AppDirectory => System.IO.Path.GetDirectoryName(AppFullName);
		public static string AppNameWithExt => System.IO.Path.GetFileName(AppFullName);
		public static string AppNameWithoutExt => System.IO.Path.GetFileNameWithoutExtension(AppNameWithExt);


		public static string UserDataLocation => StoreToAppExeLocation ?
			AppDirectory
			:
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
			+ "\\" + AppNameWithoutExt;
	}

}
