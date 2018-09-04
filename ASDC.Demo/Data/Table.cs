using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ASDC.Search;

namespace ASDC.Data
{
	public abstract class Table<TSource, TKey> : ITable<TSource>, ITreeTable<TSource>, IHashTable<TSource, TKey> where TKey : IComparable
	{
		public int Length { get; }

		public Table(int length)
		{
			Length = length;
			Rows = new List<TSource>(length);
			Entries = new List<TSource>[(int)(length * 1.5)];
			((ITreeTable<TSource>)this).Rows = new List<TreeTableRow<TSource>>();
		}

		public Table(IEnumerable<TSource> Rows)
		{
			if (Rows != null)
				this.Rows = new List<TSource>(Rows);
			else
				this.Rows = new List<TSource>();
		}

		#region ~IHashTable~

		public IList<TSource>[] Entries { get; set; }
		public abstract Func<TKey, int> HashFunction { get; }

		#endregion

		#region ~ITable~

		public IList<TSource> Rows { get; set; }
		public abstract Func<TSource, TKey> KeySelector { get; }

		#endregion

		#region ~IBinaryTree~

		IList<TreeTableRow<TSource>> ITreeTable<TSource>.Rows { get; set; }

		#endregion

		#region ~Methods~

		public void Add(TSource item)
		{
			Rows.Add(item);
			((IHashTable<TSource, TKey>)this).Insert(KeySelector, item);
			((ITreeTable<TSource>)this).Insert(KeySelector, item);
		}

		#endregion
	}
}
