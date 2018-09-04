using System;
using System.Windows;
using ASDC.Data;
using ASDC.Sort;
using static System.Math;

namespace ASDC.Demo
{
	/// <summary>
	/// Логика взаимодействия для SortTestWindow.xaml
	/// </summary>
	public partial class SortTestWindow : Window
	{
		public SortTestWindow()
		{
			InitializeComponent();
			
		}

		

#if DEBUG
		public bool? ShowDialog(CitizenTable table)
		{
			Func<Citizen, (string, string)> keySelector = (row => (row.FirstName, row.LastName));
			dataGrid.Items.Add(new { Algorithm = "Bubble", MinComplexity = table.Rows.Count, Complexity = table.BubbleSort(keySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Quick", MinComplexity = table.Rows.Count * Log(table.Rows.Count, 2), Complexity = table.QuickSort(keySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Selection", MinComplexity = Pow(table.Rows.Count, 2), Complexity = table.SelectionSort(keySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Heap", MinComplexity = table.Rows.Count * Log(table.Rows.Count, 2), Complexity = table.HeapSort(keySelector).comparisonsCount, MaxComplexity = table.Rows.Count * Log(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Insertion", MinComplexity = table.Rows.Count, Complexity = table.InsertSort(keySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Shell", MinComplexity = table.Rows.Count * Log(table.Rows.Count, 2), Complexity = table.ShellSort(keySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });

			return ShowDialog();
		}
#else
		/// <summary>
		/// Открывает окно с результатами сложности сортировки в таблице <paramref name="table"/> и возвращается только после закрытия нового открытого окна.
		/// </summary>
		/// <typeparam name="TSource">Тип строк в таблице <paramref name="table"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа в таблице <paramref name="table"/>.</typeparam>
		/// <param name="table">Таблица для по которой должна быть представлена информация о сложности сортировки.</param>
		/// <returns>Значение типа <see cref="Nullable{Boolean}"/> указывающий, принято ли действие (true) или отменено (false). Возвращаемое значение является значением свойства <see cref="Window.DialogResult"/> перед закрытием окна.</returns>
		public bool? ShowDialog<TSource, TKey>(Table<TSource, TKey> table) where TKey : IComparable
		{
			
			dataGrid.Items.Add(new { Algorithm = "Bubble", MinComplexity = table.Rows.Count, Complexity = table.BubbleSort(table.KeySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Quick", MinComplexity = table.Rows.Count * Log(table.Rows.Count, 2), Complexity = table.QuickSort(table.KeySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Selection", MinComplexity = Pow(table.Rows.Count, 2), Complexity = table.SelectionSort(table.KeySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Heap", MinComplexity = table.Rows.Count * Log(table.Rows.Count, 2), Complexity = table.HeapSort(table.KeySelector).comparisonsCount, MaxComplexity = table.Rows.Count * Log(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Insert", MinComplexity = table.Rows.Count, Complexity = table.InsertSort(table.KeySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Shell", MinComplexity = table.Rows.Count * Log(table.Rows.Count, 2), Complexity = table.ShellSort(table.KeySelector).comparisonsCount, MaxComplexity = Pow(table.Rows.Count, 2) });

			return ShowDialog();
		}
#endif

	}
}
