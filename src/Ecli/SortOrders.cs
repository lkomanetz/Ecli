using Executioner.Contracts;
using Executioner.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecli {

	internal static class SortOrders {

		internal static Sorter<ScriptDocument> SortByDateThenOrder = (documents) => {
			var sortedDocs = documents
				.OrderBy(doc => doc.DateCreatedUtc)
				.ThenBy(doc => doc.Order);

			foreach (var doc in sortedDocs) {
				doc.Scripts = doc.Scripts
					.OrderBy(s => s.DateCreatedUtc)
					.ThenBy(s => s.Order)
					.ToList();
			}

			return sortedDocs;
		};

	}

}
