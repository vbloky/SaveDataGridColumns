using SaveDataGrid.DataGridStorage;
using SaveDataGrid.ViewModels;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaveDataGrid.Views
{
    /// <summary>
    /// Interaction logic for MainWnw.xaml
    /// </summary>
    public partial class MainWnd : Window
	{
		DgSettings _dgSettings;
		public MainWnd()
		{
			InitializeComponent();
			DataContext = new MainVM();
			_dgSettings = new(dataGrid, "Grid_test_storage");
		}


		#region --- manual control by mouse ---
		private void btnLoad_Click(object sender, RoutedEventArgs e)
		{
			_dgSettings.LoadSettings();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			_dgSettings.SaveSettings();
		}

		private void btnOptions_Click(object sender, RoutedEventArgs e)
		{
			_dgSettings.ShowDataGridOptionsWindow();
		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		#endregion
	}
}
