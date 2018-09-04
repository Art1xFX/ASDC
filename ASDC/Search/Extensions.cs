using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASDC.Collections;

namespace ASDC.Search
{
	/// <summary>
	/// Методы расширения поиска.
	/// </summary>
	public static class Extensions
	{

		#region ~ITable<T>~

		/// <summary>
		/// Выполняет линейный поиск <param name="keyValue"> в таблице <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount) LinearSearch<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			// Кто переделает все методы поиска для нахождения ВСЕХ элементов, сохраняя технологию поиска, тот... молодец!
			// P.S.: Имено для этого все методы возвращают перечисление элементов, а не один элемент.
			List<TSource> result = new List<TSource>();
			int comparisonsCount = 0;

			foreach (var row in source.Rows)
			{
				comparisonsCount++;
				if (keyValue.CompareTo(keySelector(row)) == 0)
				{
					result.Add(row);
					break;
				}
			}
			return (result, comparisonsCount);
		}

		/// <summary>
		/// Асинхронно выполняет линейный поиск <param name="keyValue"> в таблице <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> LinearSearchAsync<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			return await Task.Run(() => source.LinearSearch(keySelector, keyValue));
		}

		/// <summary>
		/// Выполняет двоичный поиск <param name="keyValue"> в отсортированной таблице <paramref name="source"/>.
		/// </summary>
		/// <remarks>Сортировка будет произведена автоматически.</remarks>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		/// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> или <paramref name="keySelector"/> имеет значение <see cref="null"/>.</exception>
		public static (IEnumerable<TSource> result, int comparisonsCount) BinarySearch<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			List<TSource> result = new List<TSource>();
			int comparisonsCount = 0;

			IList<TSource> searchingArea = new List<TSource>(source.Rows.OrderBy(keySelector));
			int startIndex = 0;
			int endIndex = source.Rows.Count;
			int i = 0;
			do
			{
				i = startIndex + (endIndex - startIndex) / 2;
				comparisonsCount++;
				switch (keyValue.CompareTo(keySelector(searchingArea[i])))
				{
					case -1:
						endIndex = i;
						break;
					case 0:
						result.Add(searchingArea[i]);
						startIndex = endIndex;
						break;
					case 1:
						startIndex = i + 1;
						break;
				}
			} while (endIndex - startIndex > 0);
			return (result, comparisonsCount);
		}

		/// <summary>
		/// Асинхронно выполняет двоичный поиск <param name="keyValue"> в отсортированной таблице <paramref name="source"/>.
		/// </summary>
		/// <remarks>Сортировка будет произведена автоматически.</remarks>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		/// <exception cref="ArgumentNullException">Параметр <paramref name="source"/> или <paramref name="keySelector"/> имеет значение <see cref="null"/>.</exception>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> BinarySearchAsync<TSource, TKey>(this ITable<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			return await Task.Run(() => source.BinarySearch(keySelector, keyValue));
		}

		#endregion

		#region ~IHashTable~

		/// <summary>
		/// Выполняет вставку элемента <param name="item"> в хэш таблицу <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Хэш-таблица, в которой следует произвести вставку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="item">Вставляемый объект. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public static void Insert<TSource, TKey>(this IHashTable<TSource, TKey> source, Func<TSource, TKey> keySelector, TSource item) where TKey : IComparable
		{
			int hash = Math.Abs(source.HashFunction(keySelector(item))) % source.Entries.Length;
			if (source.Entries[hash] == null)
				source.Entries[hash] = new List<TSource>();
			source.Entries[hash].Add(item);
		}

		/// <summary>
		/// Асинхронно выполняет вставку элемента <param name="item"> в хэш таблицу <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Хэш-таблица, в которой следует произвести вставку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="item">Вставляемый объект. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		public static async Task InsertAsync<TSource, TKey>(this IHashTable<TSource, TKey> source, Func<TSource, TKey> keySelector, TSource item) where TKey : IComparable => await Task.Run(() => source.Insert(keySelector, item));

		/// <summary>
		/// Выполняет поиск <param name="keyValue"> в хэш таблице <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Хэш-таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount) HashSearch<TSource, TKey>(this IHashTable<TSource, TKey> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			int comparisonsCount = 0;

			foreach (var item in source.Entries[Math.Abs(source.HashFunction(keyValue)) % source.Entries.Length])
				if (keySelector(item).CompareTo(keyValue) == 0)
					return (new List<TSource> { item }, ++comparisonsCount);
				else
					comparisonsCount++;
			return default((IEnumerable<TSource>, int));
		}

		/// <summary>
		/// Асинхронно выполняет поиск <param name="keyValue"> в хэш таблице <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Хэш-таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> HashSearchAsync<TSource, TKey>(this IHashTable<TSource, TKey> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable => await Task.Run(() => source.HashSearch(keySelector, keyValue));

		#endregion

		#region ~ITreeTable~

		/// <summary>
		/// Выполняет вставку элемента <param name="item"> в древовидную таблицу <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Древовидная таблица, в которой следует произвести вставку.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="item">Вставляемый объект. Для ссылочных типов допускается значение <see cref="null"/>.</param>
		/// <exception cref="ArgumentException">Элемент с таким ключом уже существует в <see cref="ITreeTable{TSource}"/>.</exception>
		public static void Insert<TSource, TKey>(this ITreeTable<TSource> source, Func<TSource, TKey> keySelector, TSource item) where TKey : IComparable
		{
			if (source.Rows.Count > 0)
			{
				int index = 0;
				do
				{
					switch (keySelector(item).CompareTo(keySelector(source.Rows[index].Value)))
					{
						case -1:
							if (source.Rows[index].Left == -1)
							{
								source.Rows[index].Left = source.Rows.Count;
								source.Rows.Add(new TreeTableRow<TSource>(item));
								return;
							}
							else
								index = source.Rows[index].Left;
							break;
						case 0:
							throw new ArgumentException("Элемент с таким ключом уже существует!", nameof(item));
						case 1:
							if (source.Rows[index].Right == -1)
							{
								source.Rows[index].Right = source.Rows.Count;
								source.Rows.Add(new TreeTableRow<TSource>(item));
								return;
							}
							else
								index = source.Rows[index].Right;
							break;
						default:
							throw new Exception("Если вы видите эту ошибку, срочно напишите об этом на адрес kozakovi4@yandex.ru!");
					}
				} while (true);
			}
			else
				source.Rows.Add(new TreeTableRow<TSource>(item));
		}

		/// <summary>
		/// Выполняет поиск <param name="keyValue"> в древовидной таблице <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source"Древовидная таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>	
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount) TreeSearch<TSource, TKey>(this ITreeTable<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			var result = new List<TSource>();
			var comparisonsCount = 0;

			if (source.Rows.Count > 0)
			{
				int index = 0;
				do
				{
					comparisonsCount++;
					switch (keyValue.CompareTo(keySelector(source.Rows[index].Value)))
					{
						case -1:
							index = source.Rows[index].Left;
							break;
						case 0:
							result.Add(source.Rows[index].Value);
							return (result, comparisonsCount);
						case 1:
							index = source.Rows[index].Right;
							break;
					}
				} while (index != -1);
			}
			return (result, comparisonsCount);
		}
		
		/// <summary>
		/// Асинхронно выполняет поиск <param name="keyValue"> в древовидной таблице <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Древовидная таблица, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> TreeSearchAsync<TSource, TKey>(this ITreeTable<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable => await Task.Run(() => source.TreeSearch(keySelector, keyValue));

		#endregion

		#region ~SinglyLinkedList~

		/// <summary>
		/// Выполняет поиск <param name="keyValue"> в односвязном списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Односвязный список, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		  /// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount) Search<TSource, TKey>(this SinglyLinkedList<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			int comparisonsCount = 0;
			foreach (var item in source)
			{
				comparisonsCount++;
				if (keySelector(item).CompareTo(keyValue) == 0)
					return (new List<TSource> { item }, comparisonsCount);
			}
			return default((IEnumerable<TSource>, int));
		}

		/// <summary>
		/// Асинхронно выполняет поиск <param name="keyValue"> в односвязном списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Односвязный список, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> SearchAsync<TSource, TKey>(this SinglyLinkedList<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable => await Task.Run(() => source.Search(keySelector, keyValue));

		#endregion

		#region ~DoublyLinkedList~

		/// <summary>
		/// Выполняет поиск <param name="keyValue"> в двусвязном списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Двусвязный список, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount) Search<TSource, TKey>(this DoublyLinkedList<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable
		{
			int comparisonsCount = 0;
			foreach (var item in source)
			{
				comparisonsCount++;
				if (keySelector(item).CompareTo(keyValue) == 0)
					return (new List<TSource> { item }, comparisonsCount);
			}
			return default((IEnumerable<TSource>, int));
		}

		/// <summary>
		/// Асинхронно выполняет поиск <param name="keyValue"> в двусвязном списке <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Двусвязный список, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> SearchAsync<TSource, TKey>(this DoublyLinkedList<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TKey : IComparable => await Task.Run(() => source.Search(keySelector, keyValue));

		#endregion

		#region ~BinarySearchTree~

		/// <summary>
		/// Выполняет поиск <param name="keyValue"> в двоичном дереве поиска <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Двоичное дерево поиска, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static (IEnumerable<TSource> result, int comparisonsCount) Search<TSource, TKey>(this BinarySearchTree<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TSource : IComparable where TKey : IComparable
		{
			int comparisonsCount = 0;
			using (var enumerator = source.GetBinaryEnumerator())
				if (enumerator.MoveLeft())
					while (enumerator.Current.CompareTo(default(TSource)) != 0)
					{
						comparisonsCount++;
						switch (keySelector(enumerator.Current).CompareTo(keyValue))
						{
							case -1:
								enumerator.MoveLeft();
								break;
							case 0:
								return (new List<TSource> { enumerator.Current }, comparisonsCount);
							case 1:
								enumerator.MoveRight();
								break;
						}
					}
			return default((IEnumerable<TSource>, int));
		}

		/// <summary>
		/// Асинхронно выполняет поиск <param name="keyValue"> в двоичном дереве поиска <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TSource">Тип элементов <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">Тип ключа, возвращаемого функцией <paramref name="keySelector"/>.</typeparam>
		/// <param name="source">Двоичное дерево поиска, в которой следует произвести поиск.</param>
		/// <param name="keySelector">Функция, извлекающая ключ из элемента.</param>
		/// <param name="keyValue">Значение ключа для поиска.</param>
		/// <returns>Кортеж, содержащий последовательность элементов, удовлетворяющих критериям поиска и количество сравнений.</returns>
		public static async Task<(IEnumerable<TSource> result, int comparisonsCount)> SearchAsync<TSource, TKey>(this BinarySearchTree<TSource> source, Func<TSource, TKey> keySelector, TKey keyValue) where TSource : IComparable where TKey : IComparable => await Task.Run(() => source.Search(keySelector, keyValue));

		#endregion

	}
}
