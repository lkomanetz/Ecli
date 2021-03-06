﻿using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Ecli.Exceptions;
using Ecli.FileReaders;
using Ecli.Commands;

namespace Ecli.FileReaders.SettingsFileReaders {

	/*
	 * This class is responsible for parsing the high level JSON properties for each module
	 * and pass them to the correct reader.
	 *
	 * IE: If SettingsFileReader comes across the property "DbUpdateSettings" it would
	 * pass the entire string contents to the DbUpdateSettingsReader to get a
	 * DbUpdateSettingsReaderResult object.
	 */
	public class SettingsFileReader : IFileReader {

		// String is the CliCommandName for the corresponding settings reader
		private IDictionary<string, ISettingsReader> _availableReaders;

		public SettingsFileReader(IFinder<ICommand>[] cmdFinders) {
			_availableReaders = cmdFinders
				.SelectMany(f => f.FindAll())
				.ToDictionary(c => c.CliCommandName, c => c.SettingsReader);
		}

		public FileReaderResult Read(string settings) {
			try {
				JObject settingsJson = JObject.Parse(settings);
				IEnumerable<JProperty> properties = settingsJson.Root.Select(p => (JProperty)p);
				IList<ISettingsReaderResult> contents = new List<ISettingsReaderResult>();

				foreach (var kvp in _availableReaders) {
					ISettingsReader reader = kvp.Value;
					ISettingsReaderResult readResult;
					JProperty prop = properties.Where(p => p.Name == kvp.Key).SingleOrDefault();

					if (prop == null) readResult = reader.Read(kvp.Key, String.Empty);
					else readResult = reader.Read(prop.Name, prop.Value.ToString());

					contents.Add(readResult);
				}

				return new SettingsFileReaderResult(contents, settingsJson);	
			}
			catch (Exception err) {
				return new SettingsFileReaderResult(err);
			}
		}

	}

}
