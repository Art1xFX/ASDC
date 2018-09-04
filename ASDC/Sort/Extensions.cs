using System;
using System.Collections.Generic;

namespace ASDC.Sort
{
	/// <summary>
	/// Методы расширения сортировки.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Выполняет сортировку пузырьком в списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <returns>Кортеж, содержащий последовательность отсортированных элементов, количество сравнений и количество перестановок.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount, int swapsCount) BubbleSort<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			List<TSource> result = new List<TSource>(source.Rows);
			int comparisonsCount = 0;
			int swapsCount = 0;

			bool changed;
			for (int i = 1; i < result.Count; i++)
			{
				changed = false;
				for (int j = 0; j < result.Count - i; j++)
				{
					comparisonsCount++;
					if (keySelector(result[j]).CompareTo(keySelector(result[j + 1])) == 1)
					{
						swapsCount++;
						result.Swap(j, j + 1);
						changed = true;
					}
				}
				if (!changed)
					break;
			}
			return (result, comparisonsCount, swapsCount);
		}

		/// <summary>
		/// Выполняет быструю сортировку в списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <returns>Кортеж, содержащий последовательность отсортированных элементов, количество сравнений и количество перестановок.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount, int swapsCount) QuickSort<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			// Кто сделает быструю сортировку без рекурсии, тот... молодец!
			List<TSource> result = new List<TSource>(source.Rows);
			result.QuickSort(keySelector, 0, result.Count - 1).ToTuple().Deconstruct(out int comparisonsCount, out int swapsCount);
			return (result, comparisonsCount, swapsCount);
		}

		/// <summary>
		/// Выполняет быструю сортировку в списке <paramref name="source"/> на интервале <paramref name="indexA"/> и <paramref name="indexB"/> включительно.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="indexA">Нижняя грацница интервала, в которой будет произведена сортировка.</param>
		/// <param name="indexB">Верхняя грацница интервала, в которой будет произведена сортировка.</param>
		/// <returns>Кортеж, содержащий количество сравнений и количество перестановок.</returns>
		private static (int comparisonsCount, int swapsCount) QuickSort<TSource, TKey>(this IList<TSource> source, Func<TSource, TKey> keySelector, int indexA, int indexB) where TKey : IComparable
		{
			int comparisonsCount = 0;
			int swapsCount = 0;
			
			if (indexB - indexA > 1)
			{
				source.GetPivotKey(keySelector, indexA, indexB).ToTuple().Deconstruct(out TKey pivotKey, out comparisonsCount);
				int i = indexA;
				int j = indexB;
				while (i <= j)
				{
					while (true)
					{
						comparisonsCount++;
						if (keySelector(source[i]).CompareTo(pivotKey) < 0)
							i++;
						else
							break;
					}
					while (true)
					{
						comparisonsCount++;
						if (keySelector(source[j]).CompareTo(pivotKey) > 0)
							j--;
						else
							break;
					}

					if (i <= j)
					{
						swapsCount++;
						source.Swap(i++, j--);
					}
				}
				var left = source.QuickSort(keySelector, indexA, i - 1);
				comparisonsCount += left.comparisonsCount;
				swapsCount += left.swapsCount;
				var right = source.QuickSort(keySelector, i, indexB);
				comparisonsCount += right.comparisonsCount;
				swapsCount += right.swapsCount;
			}
			else
			{
				comparisonsCount++;
				if (keySelector(source[indexA]).CompareTo(keySelector(source[indexB])) > 0)
				{
					swapsCount++;
					source.Swap(indexA, indexB);
				}
			}
			return (comparisonsCount, swapsCount);
		}

		/// <summary>
		/// Возвращает опорный элемент на интервале <paramref name="indexA"/> и <paramref name="indexB"/> включительно.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="indexA">Нижняя грацница интервала, в которой будет произведён поиск опорного элемента.</param>
		/// <param name="indexB">Верхняя грацница интервала, в которой будет произведён поиск опорного элемента.</param>
		/// <returns>Опорный элемент на указанном интервале.</returns>
		private static (TKey result, int comparisonsCount) GetPivotKey<TSource, TKey>(this IList<TSource> source, Func<TSource, TKey> keySelector, int indexA, int indexB) where TKey : IComparable
		{
			TKey keyA = keySelector(source[indexA]);
			TKey keyB = keySelector(source[indexB]);

			if ((indexB - indexA) > 1)
			{
				TKey keyAB = keySelector(source[(indexB + indexA) / 2]);

				if (keyB.CompareTo(keyA) > 0)
					if (keyB.CompareTo(keyAB) < 0)
						return (keyB, 2);
					else if (keyA.CompareTo(keyAB) < 0)
						return (keyAB, 3);
					else
						return (keyA, 3);
				else if (keyB.CompareTo(keyAB) > 0)
					return (keyB, 2);
				else if (keyA.CompareTo(keyAB) > 0)
					return (keyAB, 3);
				else
					return (keyA, 3);
			}
			return (keyB, 0);
		}

		/// <summary>
		/// Выполняет сортировку простыми выборками в списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <returns>Кортеж, содержащий последовательность отсортированных элементов, количество сравнений и количество перестановок.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount, int swapsCount) SelectionSort<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			List<TSource> result = new List<TSource>(source.Rows);
			int comparisonsCount = 0;
			int swapsCount = 0;

			int min;
			for (int i = 0; i < result.Count - 1; i++)
			{
				min = i;
				for (int j = i + 1; j < result.Count; j++)
				{
					comparisonsCount++;
					if (keySelector(result[j]).CompareTo(keySelector(result[min])) == -1)
						min = j;
				}
				swapsCount++;
				result.Swap(i, min);
			}
			return (result, comparisonsCount, swapsCount);
		}

		/// <summary>
		/// Выполняет пирамидальную сортировку (сортировку кучей) в списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <returns>Кортеж, содержащий последовательность отсортированных элементов, количество сравнений и количество перестановок.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount, int swapsCount) HeapSort<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			List<TSource> result = new List<TSource>(source.Rows);
			int comparisonsCount = 0;
			int swapsCount = 0;

			for (var i = 0; i < result.Count; i++)
			{
				var index = i;
				var item = result[i];
				while (index > 0 && keySelector(result[(index - 1) / 2]).CompareTo(keySelector(item)) < 0)
				{
					var top = (index - 1) / 2;
					swapsCount++;
					result[index] = result[top];
					index = top;
				}
				result[index] = item;
			}
			for (var i = result.Count - 1; i > 0; i--)
			{
				var last = result[i];
				result[i] = result[0];
				var index = 0;
				while (index * 2 + 1 < i)
				{
					int left = index * 2 + 1, right = left + 1;
					if (right < i && ++comparisonsCount > 0 && keySelector(result[left]).CompareTo(keySelector(result[right])) < 0)
					{
						comparisonsCount++;
						if (keySelector(last).CompareTo(keySelector(result[right])) > 0)
							break;
						swapsCount++;
						result[index] = result[right];
						index = right;
					}
					else
					{
						comparisonsCount++;
						if (keySelector(last).CompareTo(keySelector(result[left])) > 0)
							break;
						swapsCount++;
						result[index] = result[left];
						index = left;
					}
				}
				swapsCount++;
				result[index] = last;
			}
			return (result, comparisonsCount, swapsCount);
		}

		/// <summary>
		/// Выполняет сортировку простыми вставками в списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <returns>Кортеж, содержащий последовательность отсортированных элементов, количество сравнений и количество перестановок.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount, int swapsCount) InsertSort<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			List<TSource> result = new List<TSource>(source.Rows);
			int comparisonsCount = 0;
			int swapsCount = 0;

			for (int i, j = 0; j < result.Count; j++)
			{
				TSource temp = result[j];
				i = j;
				while (i > 0 && keySelector(result[i - 1]).CompareTo(keySelector(temp)) > 0)
				{
					comparisonsCount++;
					result[i] = result[i - 1];
					i--;
				}
				swapsCount++;
				result[i] = temp;
			}
			return (result, comparisonsCount, swapsCount);
		}
		
		/// <summary>
		/// Выполняет сортировку Шелла в списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">>Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Список в котором следует произвести сортировку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <returns>Кортеж, содержащий последовательность отсортированных элементов, количество сравнений и количество перестановок.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount, int swapsCount) ShellSort<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
		{
			List<TSource> result = new List<TSource>(source.Rows);
			int comparisonsCount = 0;
			int swapsCount = 0;
			for (int k = result.Count / 2; k > 0; k /= 2)
				for (int i = k, j; i < result.Count; i++)
				{
					TSource temp = result[i];
					for (j = i; j >= k; j -= k)
					{
						comparisonsCount++;
						if (keySelector(temp).CompareTo(keySelector(result[j - k])) < 0)
						{
							swapsCount++;
							result[j] = result[j - k];
						}
						else
							break;
					}
					result[j] = temp;
				}
			return (result, comparisonsCount, swapsCount);
		}
	}
}
