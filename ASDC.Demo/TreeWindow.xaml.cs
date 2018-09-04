using System.Collections.Generic;
using System.Windows;
using ASDC.Data;

namespace ASDC
{
	/// <summary>
	/// Логика взаимодействия для TreeWindow.xaml
	/// </summary>
	public partial class TreeWindow : Window
	{
		public TreeWindow(IEnumerable<Citizen> collection) : this()
		{
			dataGrid.ItemsSource = collection;
		}
		
		public TreeWindow()
		{
			InitializeComponent();
		}
	}
}
