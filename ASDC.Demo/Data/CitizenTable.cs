using System;
using System.Collections.Generic;
using System.IO;

namespace ASDC.Data
{
	/// <summary>
	/// Представляет рализацию таблицы со строками типа <see cref="Citizen"/> и ключём типа <see cref="long"/>.
	/// </summary>
	public class CitizenTable : Table<Citizen, long>
	{
		public CitizenTable(int length) : base(length)
		{

		}

		public CitizenTable(IEnumerable<Citizen> rows) : base(rows)
		{

		}

		#region ~IHashTable~

		public override Func<long, int> HashFunction => (Id => Id.GetHashCode());

		#endregion

		#region ~ITable~

		public override Func<Citizen, long> KeySelector => (user => user.PIN);

		public void Open(string filename)
		{
			
		}

		#endregion
	}
}
