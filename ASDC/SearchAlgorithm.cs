namespace ASDC
{
	/// <summary>
	/// Задаёт алгоритм поиска.
	/// </summary>
	public enum SearchAlgorithm
    {
        /// <summary>
        /// Последовательный поиск.
        /// </summary>
        Linear,

        /// <summary>
        /// Двоичный поиск.
        /// </summary>
        /// <remarks>Поиск методом деления пополам.</remarks>
        Binary,

        /// <summary>
        /// Поиск по двоичному дереву.
        /// </summary>
        BinaryTree,

        /// <summary>
        /// Поиск в хэш таблице.
        /// </summary>
        HashTable
    }
}
