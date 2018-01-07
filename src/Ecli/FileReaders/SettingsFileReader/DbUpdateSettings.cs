using System;
using System.IO;
using System.Runtime.Serialization;

namespace Ecli.FileReaders {

	[DataContract]
	public class DbUpdateSettings {

		[DataMember] public string Name { get; set; }
		[DataMember] public string LogLocation { get; set; }
		[DataMember] public string ScriptsLocation { get; set; }
		[DataMember] public string SqlConnectionString { get; set; }

	}

}