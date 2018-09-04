using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ASDC.Data;

namespace ASDC.Demo
{
    /// <summary>
    /// Логика взаимодействия для SearchTestWindow.xaml
    /// </summary>
    public partial class SearchTestWindow : Window 
    {

		public SearchTestWindow()
        {
            InitializeComponent();
			
		}

		/// <summary>
		/// Открывает окно с результатами сложности поиска в таблице <paramref name="table"/> и возвращается только после закрытия нового открытого окна.
		/// </summary>
		/// <typeparam name="TSource">Тип строк в таблице <paramref name="table"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа в таблице <paramref name="table"/>.</typeparam>
		/// <param name="table">Таблица для по которой должна быть представлена информация о сложности поиска.</param>
		/// <returns>Значение типа <see cref="Nullable{Boolean}"/> указывающий, принято ли действие (true) или отменено (false). Возвращаемое значение является значением свойства <see cref="Window.DialogResult"/> перед закрытием окна.</returns>
		public bool? ShowDialog<TSource, TKey>(Table<TSource, TKey> source) where TKey : IComparable
		{
			dataGrid.Items.Add(new { Algorithm = "Linear", Complexity = SearchComplexity(source, Search.Extensions.LinearSearch, source.KeySelector), TheoreticalComplexity = (source.Rows.Count + 1) / 2.0 });
			dataGrid.Items.Add(new { Algorithm = "Binary", Complexity = SearchComplexity(source, Search.Extensions.BinarySearch, source.KeySelector), TheoreticalComplexity = Math.Log(source.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Tree", Complexity = SearchComplexity(source, Search.Extensions.TreeSearch, source.KeySelector), TheoreticalComplexity = Math.Log(source.Rows.Count, 2) });
			dataGrid.Items.Add(new { Algorithm = "Hash", Complexity = SearchComplexity(source, Search.Extensions.HashSearch, source.KeySelector), TheoreticalComplexity = 1 + (source.Rows.Count - 1) / (2.0 * ((IHashTable<TSource, TKey>)source).Entries.Length) });
			return ShowDialog();
		}

		private double SearchComplexity<TSource, TKey>(Table<TSource, TKey> source, Func<Table<TSource, TKey>, Func<TSource, TKey>, TKey, (IEnumerable<TSource> result, int comparisionsCount)> searchFunction, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			return source.Rows.Sum(row => searchFunction(source, keySelector, keySelector(row)).comparisionsCount) / (double)source.Rows.Count;
		}

	}
}
