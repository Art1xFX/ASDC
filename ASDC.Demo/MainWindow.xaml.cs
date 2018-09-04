using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using ASDC.Data;
using Microsoft.Win32;
using ASDC.Demo.Controls;
using ASDC.Search;
using ASDC.Sort;
using ASDC.Collections;
using System.Windows.Media.Imaging;

namespace ASDC.Demo
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		OpenFileDialog openFileDialog;

		public MainWindow()
		{
			InitializeComponent();
			openFileDialog = new OpenFileDialog()
			{
				Filter = "Бинарные файлы (*.bin)|*.bin"
			};
#if DEBUG
			sortKeyComboBox.ItemsSource = typeof(Citizen).GetProperties().Select(p => p.Name).Union(new List<string> { "FirstName + LastName" });
			sortKeyComboBox.Width = 140.0;
#else
			sortKeyComboBox.ItemsSource = typeof(Citizen).GetProperties().Select(p => p.Name);
#endif
		}

		private void openMenuItem_Click(object sender, RoutedEventArgs e)
		{
			statusTextBlock.Text = "Открытие...";
			if (openFileDialog.ShowDialog() == true)
				try
				{
					using (BinaryReader reader = new BinaryReader(File.OpenRead(openFileDialog.FileName)))
					{
						var table = new CitizenTable(reader.ReadInt32());
						for (int i = 0; i < table.Length; i++)
						{
							Citizen item = new Citizen();
							item.PIN = reader.ReadInt64();
							int l1 = reader.ReadInt32();
							for (int j = 0; j < l1; j++)
								item.FirstName += reader.ReadChar();
							int l2 = reader.ReadInt32();
							for (int j = 0; j < l2; j++)
								item.LastName += reader.ReadChar();
							var day = reader.ReadInt32();
							var month = reader.ReadInt32();
							var year = reader.ReadInt32();
							item.Birth = new DateTime(year, month, day);
							item.Gender = (Gender)reader.ReadInt32();
							table.Add(item);
						}
						tabControl.SelectedIndex = tabControl.Items.Add(new TabItem<Citizen, long>(table)
						{
							Header = System.IO.Path.GetFileName(openFileDialog.FileName)
						});
						Title = $"ASDC - {System.IO.Path.GetFileName(openFileDialog.FileName)}";
						statusTextBlock.Text = "Готово";
					}
				}
				catch (Exception ex)
				{
					tabControl.Items.Clear();
					MessageBox.Show(ex.ToString(), "ACDC", MessageBoxButton.OK, MessageBoxImage.Error);
					statusTextBlock.Text = "Ошибка";
				}
			else
				statusTextBlock.Text = "Отменено";
		}

		private void closeMenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}


		// Пример преобразования XML в понятные для программы BIN файлы.
		//private void transformMenuItem_Click(object sender, RoutedEventArgs e)
		//{
		//	statusTextBlock.Text = "Преобразование...";
		//	List<Citizen> result = new List<Citizen>();
		//	OpenFileDialog openFileDialog = new OpenFileDialog()
		//	{
		//		Title = "Преобразовать",
		//		Filter = "XML файлы (*.xml)|*.xml"
		//	};
		//	if (openFileDialog.ShowDialog() == true)
		//	{
		//		try
		//		{

		//			XDocument doc = XDocument.Load(openFileDialog.FileName);
		//			foreach (var record in doc.Element("dataset").Elements("record"))
		//			{
		//				result.Add(new Citizen()
		//				{
		//					PIN = long.Parse(record.Element("PIN").Value),
		//					FirstName = record.Element("FirstName").Value,
		//					LastName = record.Element("LastName").Value,
		//					Gender = (Gender)int.Parse(record.Element("Gender").Value),
		//					Birth = DateTime.Parse(record.Element("Birth").Value),
		//				});
		//			}

		//			using (var writer = new BinaryWriter(new FileStream("citizens.bin", FileMode.OpenOrCreate)))
		//			{
		//				writer.Write(result.Count);
		//				foreach (var item in result)
		//				{
		//					writer.Write(item.PIN);
		//					writer.Write(item.FirstName.Length);
		//					foreach (var c in item.FirstName)
		//						writer.Write(c);

		//					writer.Write(item.LastName.Length);
		//					foreach (var c in item.LastName)
		//						writer.Write(c);
		//					writer.Write(item.Birth.Day);
		//					writer.Write(item.Birth.Month);
		//					writer.Write(item.Birth.Year);
		//					writer.Write((int)item.Gender);
		//				}
		//			}
		//			statusTextBlock.Text = "Готово";
		//		}
		//		catch (Exception)
		//		{
		//			MessageBox.Show("Не удалось преобразовать указанный файл!", "ASDC", MessageBoxButton.OK, MessageBoxImage.Error);
		//		}
		//	}
		//}


		private async void searchMenuItem_Click(object sender, RoutedEventArgs e)
		{

			statusTextBlock.Text = "Поиск...";
			if (long.TryParse(searchKeyTextBox.Text, out long keyValue))
			{
				var selectedItem = tabControl.SelectedItem as Controls.TabItem<Citizen, long>;
				(IEnumerable<Citizen> result, int comparisonsCount) searchResult = default((IEnumerable<Citizen>, int));
				
				switch ((SearchAlgorithm)searchAlgorithmComboBox.SelectedIndex)
				{
					case SearchAlgorithm.Linear:
						searchResult = await selectedItem.Table.LinearSearchAsync(selectedItem.Table.KeySelector, keyValue);
						break;
					case SearchAlgorithm.Binary:
						searchResult = await selectedItem.Table.BinarySearchAsync(selectedItem.Table.KeySelector, keyValue);
						break;
					case SearchAlgorithm.BinaryTree:
						searchResult = selectedItem.Table.TreeSearch(selectedItem.Table.KeySelector, keyValue);
						break;
					case SearchAlgorithm.HashTable:
						searchResult = await selectedItem.Table.HashSearchAsync(selectedItem.Table.KeySelector, keyValue);
						break;
				}

				tabControl.SelectedIndex = tabControl.Items.Add(new TabItem<Citizen, long>(new CitizenTable(searchResult.result), searchKeyTextBox.Text, searchAlgorithmComboBox.SelectedIndex, searchResult.comparisonsCount)
				{
					Header = $"{selectedItem.Header} - поиск"
				});
				
				statusTextBlock.Text = "Готово";
			}
			else
			{
				MessageBox.Show("Введённое значение не является целым числом!", "ASDC", MessageBoxButton.OK, MessageBoxImage.Error);
				statusTextBlock.Text = "Ошибка";
			}

		}

		private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItem = tabControl.SelectedItem as TabItem<Citizen, long>;

			if (selectedItem == null)
			{
				openMenuItem.IsEnabled = openMenu2.IsEnabled = true;
				searchAlgorithmComboBox.IsEnabled = searchKeyTextBox.IsEnabled = searchMenuItem.IsEnabled = searchTestMenuItem.IsEnabled = sortAlgorithmComboBox.IsEnabled = sortKeyComboBox.IsEnabled = sortMenuItem.IsEnabled = sortTestMenuItem.IsEnabled = stackMenuItem.IsEnabled = queueMenuItem.IsEnabled = dequeMenuItem.IsEnabled = inorderMenuItem.IsEnabled = preorderMenuItem.IsEnabled = postorderMenuItem.IsEnabled = false;
			}
			else if ( selectedItem.TabItemType == TabItemType.Search)
			{
				openMenuItem.IsEnabled = openMenu2.IsEnabled = false;
				searchAlgorithmComboBox.SelectedIndex = selectedItem.Algorithm;
				searchKeyTextBox.Text = selectedItem.Key.ToString();
				sortAlgorithmComboBox.SelectedIndex = -1;
				sortKeyComboBox.SelectedIndex = -1;

				searchAlgorithmComboBox.IsEnabled = searchKeyTextBox.IsEnabled = searchMenuItem.IsEnabled = searchTestMenuItem.IsEnabled = sortAlgorithmComboBox.IsEnabled = sortKeyComboBox.IsEnabled = sortMenuItem.IsEnabled = sortTestMenuItem.IsEnabled = stackMenuItem.IsEnabled = queueMenuItem.IsEnabled = dequeMenuItem.IsEnabled = inorderMenuItem.IsEnabled = preorderMenuItem.IsEnabled = postorderMenuItem.IsEnabled = false;

				comparisionsCountTextBlock.Text = $"Сравнений {selectedItem.ComparisionsCount.ToString()}";
				comparisionsCountTextBlock.Visibility = Visibility.Visible;
			}
			else if (selectedItem != null && selectedItem.TabItemType == TabItemType.Sort)
			{
				openMenuItem.IsEnabled = openMenu2.IsEnabled = false;
				searchAlgorithmComboBox.SelectedIndex = -1;
				searchKeyTextBox.Text = string.Empty;
				sortAlgorithmComboBox.SelectedIndex = selectedItem.Algorithm;
				sortKeyComboBox.SelectedIndex = (int)selectedItem.Key;

				searchAlgorithmComboBox.IsEnabled = searchKeyTextBox.IsEnabled = searchMenuItem.IsEnabled = searchTestMenuItem.IsEnabled = sortAlgorithmComboBox.IsEnabled = sortKeyComboBox.IsEnabled = sortMenuItem.IsEnabled = sortTestMenuItem.IsEnabled = stackMenuItem.IsEnabled = queueMenuItem.IsEnabled = dequeMenuItem.IsEnabled = inorderMenuItem.IsEnabled = preorderMenuItem.IsEnabled = postorderMenuItem.IsEnabled = false;
				
				comparisionsCountTextBlock.Text = $"Сравнений {selectedItem.ComparisionsCount.ToString()}";
				comparisionsCountTextBlock.Visibility = Visibility.Visible;
				timeTextBlock.Text = $"Перестановок {selectedItem.SwapsCount.ToString()}";
				timeTextBlock.Visibility = Visibility.Visible;
			}
			else
			{
				openMenuItem.IsEnabled = openMenu2.IsEnabled = false;
				searchAlgorithmComboBox.IsEnabled = searchKeyTextBox.IsEnabled = searchTestMenuItem.IsEnabled =  sortAlgorithmComboBox.IsEnabled = sortKeyComboBox.IsEnabled = sortTestMenuItem.IsEnabled = stackMenuItem.IsEnabled = queueMenuItem.IsEnabled = dequeMenuItem.IsEnabled = inorderMenuItem.IsEnabled = preorderMenuItem.IsEnabled = postorderMenuItem.IsEnabled = true;
				searchMenuItem.IsEnabled = searchAlgorithmComboBox.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(searchKeyTextBox.Text);
				sortMenuItem.IsEnabled = sortAlgorithmComboBox.SelectedIndex != -1 && sortKeyComboBox.SelectedIndex != -1;
				comparisionsCountTextBlock.Visibility = Visibility.Hidden;
				timeTextBlock.Visibility = Visibility.Hidden;
			}
		}

		private void searchAlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			searchMenuItem.IsEnabled = searchAlgorithmComboBox.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(searchKeyTextBox.Text);
		}

		private void searchKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			searchMenuItem.IsEnabled = searchAlgorithmComboBox.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(searchKeyTextBox.Text);
		}

		private void sortKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			sortMenuItem.IsEnabled = sortAlgorithmComboBox.SelectedIndex != -1 && sortKeyComboBox.SelectedIndex != -1;
		}

		private void sortMenuItem_Click(object sender, RoutedEventArgs e)
		{
			statusTextBlock.Text = "Сортировка...";
			var selectedItem = tabControl.SelectedItem as TabItem<Citizen, long>;

			(IEnumerable<Citizen> result, int comparisonsCount, int swapsCount) searchResult = default((IEnumerable<Citizen>, int, int));

#if DEBUG
			if (sortKeyComboBox.Text == "FirstName + LastName")
			{
				Func<Citizen, (string, string)> keySelector = (row => (row.FirstName, row.LastName));
				switch ((SortAlgorithm)sortAlgorithmComboBox.SelectedIndex)
				{
					case SortAlgorithm.Bubble:
						searchResult = selectedItem.Table.BubbleSort(keySelector);
						break;
					case SortAlgorithm.Quick:
						searchResult = selectedItem.Table.QuickSort(keySelector);
						break;
					case SortAlgorithm.Selection:
						searchResult = selectedItem.Table.SelectionSort(keySelector);
						break;
					case SortAlgorithm.Heap:
						searchResult = selectedItem.Table.HeapSort(keySelector);
						break;
					case SortAlgorithm.Insert:
						searchResult = selectedItem.Table.InsertSort(keySelector);
						break;
					case SortAlgorithm.Shell:
						searchResult = selectedItem.Table.ShellSort(keySelector);
						break;
				}
			}
			else
#endif
				switch ((SortAlgorithm)sortAlgorithmComboBox.SelectedIndex)
				{
					case SortAlgorithm.Bubble:
						searchResult = selectedItem.Table.BubbleSort(row => (IComparable)row.GetType().GetProperty(sortKeyComboBox.Text).GetValue(row));
						break;
					case SortAlgorithm.Quick:
						searchResult = selectedItem.Table.QuickSort(row => (IComparable)row.GetType().GetProperty(sortKeyComboBox.Text).GetValue(row));
						break;
					case SortAlgorithm.Selection:
						searchResult = selectedItem.Table.SelectionSort(row => (IComparable)row.GetType().GetProperty(sortKeyComboBox.Text).GetValue(row));
						break;
					case SortAlgorithm.Heap:
						searchResult = selectedItem.Table.HeapSort(row => (IComparable)row.GetType().GetProperty(sortKeyComboBox.Text).GetValue(row));
						break;
					case SortAlgorithm.Insert:
						searchResult = selectedItem.Table.InsertSort(row => (IComparable)row.GetType().GetProperty(sortKeyComboBox.Text).GetValue(row));
						break;
					case SortAlgorithm.Shell:
						searchResult = selectedItem.Table.ShellSort(row => (IComparable)row.GetType().GetProperty(sortKeyComboBox.Text).GetValue(row));
						break;
				}
			
			tabControl.SelectedIndex = tabControl.Items.Add(new TabItem<Citizen, long>(new CitizenTable(searchResult.result), sortKeyComboBox.SelectedIndex, sortAlgorithmComboBox.SelectedIndex, searchResult.comparisonsCount, searchResult.swapsCount)
			{
				Header = $"{selectedItem.Header} - сортировка"
			});

			statusTextBlock.Text = "Готово";

		}

		private void sortTestMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new SortTestWindow()
			{
				Owner = this
			}.ShowDialog((CitizenTable)(tabControl.SelectedItem as TabItem<Citizen, long>).Table);
		}

		private void searchTestMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new SearchTestWindow()
			{
				Owner = this
			}.ShowDialog((tabControl.SelectedItem as TabItem<Citizen, long>).Table);
		}

		private void stackMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new ListWindow(new ObservableStack<Citizen>((tabControl.SelectedItem as TabItem<Citizen, long>).Table.Rows))
			{
				Title = "Стек",
				Owner = this,
				Icon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Stack.png"))
			}.ShowDialog();
		}

		private void queueMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new ListWindow(new ObservableQueue<Citizen>((tabControl.SelectedItem as TabItem<Citizen, long>).Table.Rows))
			{
				Title = "Очередь",
				Owner = this,
				Icon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Queue.png"))
			}.ShowDialog();
		}

		private void dequeMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new ListWindow(new ObservableDeque<Citizen>((tabControl.SelectedItem as TabItem<Citizen, long>).Table.Rows))
			{
				Title = "Двухсторонняя очередь",
				Owner = this,
				Icon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Deque.png"))
			}.ShowDialog();
		}

		private void preorderMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new TreeWindow(new BinarySearchTree<Citizen>((tabControl.SelectedItem as TabItem<Citizen, long>).Table.Rows).PreorderTraverse())
			{
				Title = "Прямой обход",
				Owner = this,
				Icon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Preorder.png"))
			}.ShowDialog();
		}

		private void inorderMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new TreeWindow(new BinarySearchTree<Citizen>((tabControl.SelectedItem as TabItem<Citizen, long>).Table.Rows).InorderTraverse())
			{
				Title = "Симметричный обход",
				Owner = this,
				Icon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Inorder.png"))
			}.ShowDialog();
		}

		private void postorderMenuItem_Click(object sender, RoutedEventArgs e)
		{
			new TreeWindow(new BinarySearchTree<Citizen>((tabControl.SelectedItem as TabItem<Citizen, long>).Table.Rows).PostorderTraverse())
			{
				Title = "Обратный обход",
				Owner = this,
				Icon = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Postorder.png"))
			}.ShowDialog();
		}
	}
}
