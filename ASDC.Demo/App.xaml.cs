using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ASDC.Demo
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			Window window = (sender as FrameworkElement).TemplatedParent as Window;
			if (window != null)
				SystemCommands.CloseWindow(window);
		}

		private void maximizeButton_Click(object sender, RoutedEventArgs e)
		{
			Window window = (sender as FrameworkElement).TemplatedParent as Window;
			if (window != null)
				if (window.WindowState == WindowState.Maximized)
					SystemCommands.RestoreWindow(window);
				else
					SystemCommands.MaximizeWindow(window);
		}

		private void minimizeButton_Click(object sender, RoutedEventArgs e)
		{
			Window window = (sender as FrameworkElement).TemplatedParent as Window;
			if (window != null)
				SystemCommands.MinimizeWindow(window);
		}

		private void border_Loaded(object sender, RoutedEventArgs e)
		{
			Window window = (sender as FrameworkElement).TemplatedParent as Window;
			window.MaxWidth = SystemParameters.WorkArea.Width + 11;
			window.MaxHeight = SystemParameters.WorkArea.Height + 8;
		}

		private void closeButton_Click1(object sender, RoutedEventArgs e)
		{
			Controls.TabItem tabItem = (Controls.TabItem)(sender as FrameworkElement).TemplatedParent;
			var tabControl = GetAncestor<TabControl>(tabItem);
			int index = tabControl.Items.IndexOf(tabItem);
			tabControl.Items.RemoveAt(index);
		}

		public static T GetAncestor<T>(FrameworkElement element) where T : FrameworkElement
		{
			var result = (FrameworkElement)element.Parent;
			while (!(result is T))
				result = (FrameworkElement)result.Parent;
			return (T)result;

		}
	}

}
