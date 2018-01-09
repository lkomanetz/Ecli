using System;
using Ecli.Parsers;

namespace Ecli {

	public class HelpPage : IConsolePage {

		private string _textToShow;

		public HelpPage(string textToShow) {
			this.Id = Guid.NewGuid();
			_textToShow = textToShow;
		}

		public Guid Id { get; }

		public void Show() {
			DisplayHeader();
			Console.WriteLine(_textToShow);
		}

		private void DisplayHeader() {
			string header = String.Empty;
			for (short i = 0; i < 20; ++i) header += "=";
			header += "\n\tHELP\n";
			for (short i = 0; i < 20; ++i) header += "=";
			Console.WriteLine(header);

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Command\t\tParameters");
			Console.ForegroundColor = ConsoleColor.White;
		}

	}

}