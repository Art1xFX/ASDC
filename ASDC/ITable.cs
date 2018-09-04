using System.Collections.Generic;

namespace ASDC
{
    /// <summary>
    /// Предоставляет структуру таблицы.
    /// </summary>
    /// <typeparam name="T">Тип строк в таблице.</typeparam>
    public interface ITable<T>
    {
		/// <summary>
		/// Строки таблицы.
		/// </summary>
        IList<T> Rows { get; set; }
    }
}
