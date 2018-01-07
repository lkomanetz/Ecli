using Executioner.Contracts;
using Ecli.Contracts;
using System;
using System.Collections.Generic;

namespace Ecli {

	public class ResultsPage : IConsolePage {

		private IDictionary<string, ExecutionResult> _results;

		public ResultsPage(IDictionary<string, ExecutionResult> results) {
			_results = results;
			this.Id = Guid.NewGuid();
		}

		public Guid Id { get; }

		public void Show() {
			PrintHeader();
			ShowNumberOfUpgradersRan();

			foreach (var keyValuePair in _results) {
				string msg = String.Format(
					"{0} -> \n\tDocs Completed: {1}\tScripts Completed: {2}",
					keyValuePair.Key,
					keyValuePair.Value.ScriptDocumentsCompleted,
					keyValuePair.Value.ScriptsCompleted
				);

				Console.WriteLine(msg);
			}

			Console.Write('\n');
		}

		private void ShowNumberOfUpgradersRan() => Console.WriteLine($"Upgraders ran: {_results.Count}");

		private void PrintHeader() {
			string header = String.Empty;
			for (short i = 0; i < 10; ++i) header += "=";
			header += "Results";
			for (short i = 0; i < 10; ++i) header += "=";

			Console.WriteLine(header);
		}

	}
}
