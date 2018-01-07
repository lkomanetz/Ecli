using Executioner;
using Executioner.Contracts;
using Executioner.Sorters;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Ecli.Upgraders {

	public class Upgrader {

		private IDataStore _logger;
		private IScriptLoader _scriptLoader;
		private ScriptExecutioner _executioner;

		public Upgrader(
			string logDirectoryLocation,
			string scriptDocumentDirectoryLocation,
			string connectionString,
			string name,
			Sorter<ScriptDocument> sorter
		) {
			this.Name = name;
			_logger = new FileSystemStore(logDirectoryLocation);
			_scriptLoader = new FileSystemLoader(scriptDocumentDirectoryLocation, sorter);
			_executioner = new ScriptExecutioner(_scriptLoader, _logger);
			SetupSqlExecutors(connectionString);
		}

		public ScriptExecutioner Executioner => _executioner;
		public string Name { get; private set; } 

		public ExecutionResult Run(ExecutionRequest request) => _executioner.Run(request);

		private void SetupSqlExecutors(string connectionString) {
			var executor = (SqlServerExecutor)_executioner.ScriptExecutors
				.Where(x => x.GetType() == typeof(SqlServerExecutor))
				.Single();
			executor.ConnectionString = connectionString;
		}

	}

}