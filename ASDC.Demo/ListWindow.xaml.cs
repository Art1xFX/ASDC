using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ASDC.Collections;
using ASDC.Data;


namespace ASDC
{
	/// <summary>
	/// Логика взаимодействия для ListWindow.xaml
	/// </summary>
	public partial class ListWindow : Window
	{
		System.Collections.Generic.ICollection<Citizen> collection1;
		System.Collections.Generic.ICollection<Citizen> collection2;

		public ListWindow(System.Collections.Generic.ICollection<Citizen> collection) : this()
		{
			switch (collection1 = collection)
			{
				case ObservableStack<Citizen> stack:
					collection2 = new ObservableStack<Citizen>();
					clearMenuItem.ToolTip = clearMenuItem2.ToolTip += " стек";
					break;
				case ObservableQueue<Citizen> queue:
					collection2 = new ObservableQueue<Citizen>();
					addMenuItem.ToolTip = addMenuItem2.ToolTip = "Добавить элемент (Enqueue)";
					removeMenuItem.ToolTip = removeMenuItem2.ToolTip = "Удалить элемент (Dequeue)";
					clearMenuItem.ToolTip = clearMenuItem2.ToolTip += " очередь";
					addMenuItem.Icon = new Image()
					{
						Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/AddBack.png"))
					};
					addMenuItem2.Icon = new Image()
					{
						Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/AddBack.png"))
					};
					break;
				case ObservableDeque<Citizen> deque:
					collection2 = new ObservableDeque<Citizen>();
					clearMenuItem.ToolTip = clearMenuItem2.ToolTip += " двухстороннюю очередь";
					addMenuItem.Header = "Добавить элемент в начало";
					removeMenuItem.Header = "Удалить элемент с начала";
					addMenuItem.ToolTip = addMenuItem2.ToolTip = "Добавить элемент в начало (PushFront)";
					removeMenuItem.ToolTip = removeMenuItem2.ToolTip = "Удалить элемент с начала (PopFront)";
					addBackMenuItem.Visibility = addBackMenuItem2.Visibility = removeBackMenuItem.Visibility = removeBackMenuItem2.Visibility = separator.Visibility = separator2.Visibility = Visibility.Visible;
					break;
			}
			dataGrid.ItemsSource = collection2;
			((INotifyPropertyChanged)collection2).PropertyChanged += (sender, e) =>
			{
				addMenuItem.IsEnabled = addMenuItem2.IsEnabled = addBackMenuItem.IsEnabled = addBackMenuItem2.IsEnabled = collection1.Count > 0;
				removeMenuItem.IsEnabled = removeMenuItem2.IsEnabled = removeBackMenuItem.IsEnabled = removeBackMenuItem2.IsEnabled = clearMenuItem.IsEnabled = clearMenuItem2.IsEnabled = collection2.Count > 0;
				countTextBlock.Text = "Количество " + collection2.Count.ToString();
			};
		}


		public ListWindow()
		{
			InitializeComponent();
		}

		private void addMenuItem_Click(object sender, RoutedEventArgs e)
		{
			switch (collection1)
			{
				case ObservableStack<Citizen> stack:
					((ObservableStack<Citizen>)collection2).Push(stack.Pop());
					break;
				case ObservableQueue<Citizen> queue:
					((ObservableQueue<Citizen>)collection2).Enqueue(queue.Dequeue());
					break;
				case ObservableDeque<Citizen> deque:
					((ObservableDeque<Citizen>)collection2).PushFront(deque.PopFront());
					break;
			}
		}

		private void removeMenuItem_Click(object sender, RoutedEventArgs e)
		{
			switch (collection1)
			{
				case ObservableStack<Citizen> stack:
					stack.Push(((ObservableStack<Citizen>)collection2).Pop());
					break;
				case ObservableQueue<Citizen> queue:
					queue.Enqueue(((ObservableQueue<Citizen>)collection2).Dequeue());
					break;
				case ObservableDeque<Citizen> deque:
					deque.PushFront(((ObservableDeque<Citizen>)collection2).PopFront());
					break;
			}
		}

		private void clearMenuItem_Click(object sender, RoutedEventArgs e)
		{
			// Не сокращать до лямбды
			// Такой вызов вызывает делегат CollectionChanged Observable коллекций
			switch (collection2)
			{
				case ObservableStack<Citizen> stack:
					stack.Clear();
					break;
				case ObservableQueue<Citizen> queue:
					queue.Clear();
					break;
				case ObservableDeque<Citizen> deque:
					deque.Clear();
					break;
			}
		}


		private void closeMenuItem_Click(object sender, RoutedEventArgs e) => Close();

		private void addBackMenuItem_Click(object sender, RoutedEventArgs e) => ((ObservableDeque<Citizen>)collection2).PushBack(((ObservableDeque<Citizen>)collection1).PopBack());
	
		private void removeBackMenuItem_Click(object sender, RoutedEventArgs e) => ((ObservableDeque<Citizen>)collection1).PushBack(((ObservableDeque<Citizen>)collection2).PopBack());

	}
}
