using System;
using System.IO;
using System.Runtime.Serialization;

namespace Ecli.FileReaders {

	[DataContract]
	public class DbUpgraderSettings : IFileReaderContents {

		[DataMember] public DbUpdateSettings[] DbUpgraders { get; set; } = new DbUpdateSettings[0];
		public string RawContents { get; set; } = String.Empty;

	}

}