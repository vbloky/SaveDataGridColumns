using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaveDataGrid.Lib
{
	public class RelayCommand : ICommand
	{
		readonly Action _command = null;

		public RelayCommand(Action command) => _command = command;


		public event EventHandler? CanExecuteChanged;
		public bool CanExecute(object? parameter) => true;
		public void Execute(object? parameter) => _command();
	}
}
