using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyCode
{
	public class Flight
	{
		public int CityNo { get; set; }
	}
	public class FlightCompare : IEqualityComparer<Flight>
	{
		public bool Equals(Flight x, Flight y)
		{
			if (x == null)
				return y == null;
			return x.CityNo == y.CityNo;
		}

		public int GetHashCode(Flight obj)
		{
			if (obj == null)
				return 0;
			return obj.CityNo.GetHashCode();
		}
	}
}
