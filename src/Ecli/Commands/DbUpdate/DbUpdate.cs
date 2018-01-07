using Executioner.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecli;
using Ecli.FileReaders;
using Ecli.FileReaders.SettingsFileReaders;
using Ecli.Parsers;
using Ecli.Contracts;
using Microsoft.Extensions.Configuration;
using System.IO;
using Ecli.Upgraders;
using System.Text;

namespace Ecli.Commands {

	public class DbUpdate : ICommand {

		private Upgrader[] _upgraders;
		private IParser _argumentParser;
		private IHelpTextBuilder _helpTextBuilder;
		private DisplayManager _displayManager;
		private ISettingsReader _settingsReader;
		private static readonly object key = new Object();

		public DbUpdate() {
			_displayManager = new DisplayManager();
			_argumentParser = new ArgumentParser();
			_helpTextBuilder = new DbUpdateHelpBuilder(new ParserFinder());
			_settingsReader = new DbUpdateSettingsReader();
		}

		public string HelpText => _helpTextBuilder.Build();
		public ISettingsReader SettingsReader => _settingsReader;
		public string CliCommandName => "dbupdate";

		//TODO(Logan) -> Figure out why InvalidOperationException happens with no arguments passed in.
		public bool Execute(string commandArguments, ISettingsReaderResult settingsResult) {
			_upgraders = CreateUpgraders(settingsResult);
			InitializeEventHandlers(_upgraders);
			ExecutionRequest request = GenerateRequest(commandArguments);

			var results = new Dictionary<string, ExecutionResult>();
			IList<Task> tasks = new List<Task>();
			foreach (var upgrader in _upgraders) {
				Task<ExecutionResult> t = Task.Factory.StartNew(() => upgrader.Run(request));
				t.ContinueWith((task) => results.Add(upgrader.Name, task.Result));
				tasks.Add(t);
			}

			Task.WaitAll(tasks.ToArray());
			_displayManager.Show(new ResultsPage(results));
			return true;
		}

		private Upgrader[] CreateUpgraders(ISettingsReaderResult settings) {
			var dbUpdateSettings = (DbUpdateSettingsReaderResult)settings;
			DbUpdateSettings[] updateSettings = dbUpdateSettings.Settings.DbUpgraders;
			Upgrader[] upgraders = new Upgrader[updateSettings.Length];

			for (short i = 0; i < updateSettings.Length; ++i) {
				upgraders[i] = new Upgrader(
					updateSettings[i].LogLocation,
					updateSettings[i].ScriptsLocation,
					updateSettings[i].SqlConnectionString,
					updateSettings[i].Name,
					SortOrders.SortByDateThenOrder
				);
			}

			return upgraders;
		}

		private ExecutionRequest GenerateRequest(string args) {
			ArgumentParserResult result = (ArgumentParserResult)_argumentParser.Parse(args);
			DateRangeParserResult rangeResult = result.DateRangeResult;
			ExecutionPolicyParserResult executionPolicyResult = result.ExecutionPolicyResult;
			return new ExecutionRequest() {
				ExecuteAllScripts = executionPolicyResult.ExecuteAllScripts,
				ExecuteScriptsBetween = (s) => s.DateCreatedUtc >= rangeResult.DateRange.From && s.DateCreatedUtc <= rangeResult.DateRange.To
			};
		}

		private void InitializeEventHandlers(Upgrader[] upgraders) {
			foreach (var upgrader in upgraders) {
				upgrader.Executioner.OnScriptExecuting += (obj, args) => {
					Console.WriteLine($"Executing script: {args.Script.SysId}");
				};
				upgrader.Executioner.OnScriptExecuted += (obj, args) => {
					Console.WriteLine($"Executed script: {args.Script.SysId}");
				};
			}
		}

	}

}