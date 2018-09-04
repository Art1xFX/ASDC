namespace ASDC
{
	/// <summary>
	/// Предоставляет строку древовидной таблицы.
	/// </summary>
	/// <typeparam name="T">Тип данных в строке.</typeparam>
	public class TreeTableRow<T>
	{
		/// <summary>
		/// Инициализирует новый класс <see cref="TreeTableRow{T}"/> со значением <paramref name="value"/>.
		/// </summary>
		/// <param name="value"></param>
		public TreeTableRow(T value)
		{
			Value = value;
			Left = -1;
			Right = -1;
		}

		/// <summary>
		/// Индекс левой строки.
		/// </summary>
		public int Left { get; set; }

		/// <summary>
		/// Индекс правой строки.
		/// </summary>
		public int Right { get; set; }

		/// <summary>
		/// Данные, хранящиеся в текущей строке.
		/// </summary>
		public T Value { get; set; }
	}
}
