using System;
using System.Collections.Generic;

namespace ASDC
{
	/// <summary>
	/// Предоставляет структуру хэш-таблицы.
	/// </summary>
	/// <typeparam name="TSource">Тип элементов <see cref="Entries"/>.</typeparam>
	/// <typeparam name="TKey">Тип ключа элементов <see cref="Entries"/>.</typeparam>
	public interface IHashTable<TSource, TKey>
    {
		/// <summary>
		/// Массив записей хэш-таблицы.
		/// </summary>
        IList<TSource>[] Entries { get; set; }

		/// <summary>
		/// Функция распределения хэш-таблицы.
		/// </summary>
        Func<TKey, int> HashFunction { get; } 
    }
}
