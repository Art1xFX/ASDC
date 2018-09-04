using System.Collections.Generic;

namespace ASDC
{
	/// <summary>
	/// Общие методы расширения.
	/// </summary>
	public static class Extensions
	{
		#region ~IList~

		/// <summary>
		/// Меняет два элемента списка местами.
		/// </summary>
		/// <typeparam name="T">Тип элементов в списке.</typeparam>
		/// <param name="source">Список, в котором будет осуществленна перестановка.</param>
		/// <param name="indexA">Индекс первого из переставляемых объектов.</param>
		/// <param name="indexB">Индекс второго из переставляемых объектов.</param>
		internal static void Swap<T>(this IList<T> source, int indexA, int indexB)
		{
			T temp = source[indexA];
			source[indexA] = source[indexB];
			source[indexB] = temp;
		}

		#endregion

		#region ~SearchAlgorithm~

		/// <summary>
		/// Получает понятное имя элемента перечислителя.
		/// </summary>
		public static string GetName(this SearchAlgorithm searchAlgorithm)
		{
			switch (searchAlgorithm)
			{
				case SearchAlgorithm.Linear:
					return "Линейный";
				case SearchAlgorithm.Binary:
					return "Бинарный";
				case SearchAlgorithm.BinaryTree:
					return "Двоичное дерево";
				case SearchAlgorithm.HashTable:
					return "Хэш-таблица";
				default:
					return null;
			}
		}

		#endregion

		#region ~SortAlgorithm~

		/// <summary>
		/// Получает понятное имя элемента перечислителя.
		/// </summary>
		public static string GetName(this SortAlgorithm sortAlgorithm)
		{
			switch (sortAlgorithm)
			{
				case SortAlgorithm.Bubble:
					return "Пузырьковая сортировка";
				case SortAlgorithm.Quick:
					return "Быстрая сортировка";
				case SortAlgorithm.Selection:
					return "Cортировка выбором";
				case SortAlgorithm.Heap:
					return "Пирамидальная сортировка";
				case SortAlgorithm.Insert:
					return "Сортировка вставками";
				case SortAlgorithm.Shell:
					return "Сортировка Шелла";
				default:
					return null;
			}
		}

		#endregion

	}
}
