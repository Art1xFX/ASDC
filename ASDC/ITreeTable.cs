using System.Collections.Generic;

namespace ASDC
{
	/// <summary>
	/// Предоставляет структуру древовидной таблицы.
	/// </summary>
	/// <typeparam name="T">Тип строк в таблице.</typeparam>
	public interface ITreeTable<T>
	{
		/// <summary>
		/// Строки древовидной таблицы.
		/// </summary>
		IList<TreeTableRow<T>> Rows { get; set; }
	}
}
