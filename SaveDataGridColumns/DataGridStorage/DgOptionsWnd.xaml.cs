using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaveDataGrid.DataGridStorage
{
	/// <summary>
	/// Interaction logic for GridOptionsWnd.xaml
	/// </summary>
	public partial class DgOptionsWnd : Window
	{
		public DgOptionsWnd()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
