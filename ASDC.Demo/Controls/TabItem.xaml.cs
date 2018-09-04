using System;
using System.Collections.Generic;
using System.Windows;
using ASDC.Data;

namespace ASDC.Demo.Controls
{
	/// <summary>
	/// Логика взаимодействия для TabItem.xaml
	/// </summary>
	public partial class TabItem<TSource, TKey> : TabItem where TKey : IComparable
	{
		public Table<TSource, TKey> Table { get; }

		public TabItemType TabItemType { get; }

		public int Algorithm { get; }

		public int ComparisionsCount { get; }

		public long SwapsCount { get; }

		public object Key { get; }

		private TabItem()
		{
			InitializeComponent();
			Style = (Style)Application.Current.TryFindResource("tabItemStyle");
			
		}

		public TabItem(Table<TSource, TKey> table) : this()
		{
			dataGrid.ItemsSource = (Table = table).Rows;
			TabItemType = TabItemType.File;
		}

		public TabItem(Table<TSource, TKey> table, string key, int algorithm, int comparisionsCount) : this()
		{
			Key = key;
			dataGrid.ItemsSource = (Table = table).Rows ?? new List<TSource>();
			TabItemType = TabItemType.Search;
			Algorithm = algorithm;
			ComparisionsCount = comparisionsCount;
		}

		public TabItem(Table<TSource, TKey> table, int key, int algorithm, int comparisionsCount, long swapsCount) : this()
		{
			Key = key;
			dataGrid.ItemsSource = (Table = table).Rows ?? new List<TSource>();
			TabItemType = TabItemType.Sort;
			Algorithm = algorithm;
			ComparisionsCount = comparisionsCount;
			SwapsCount = swapsCount;
		}
	}
}
