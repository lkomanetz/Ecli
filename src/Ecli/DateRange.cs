using System;

namespace Ecli {

	public class DateRange {

		public DateRange(DateTime from, DateTime to) {
			this.From = from;
			this.To = to;
		}

		public DateTime From { get; private set; }
		public DateTime To { get; private set; }

	}

}
