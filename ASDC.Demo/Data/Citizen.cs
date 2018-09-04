using System;
using System.Net;

namespace ASDC.Data
{

    [Serializable]
    public struct Citizen : IComparable<Citizen>, IComparable
    {
		public long PIN { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime Birth { get; set; }
		public Gender Gender { get; set; }

		public int CompareTo(Citizen other)
		{
#if DEBUG
			return (FirstName, LastName).CompareTo((other.FirstName, other.LastName));
#else
			return PIN.CompareTo(other.PIN);
#endif
		}

		public int CompareTo(object obj) => CompareTo((Citizen)obj);

	}
}
