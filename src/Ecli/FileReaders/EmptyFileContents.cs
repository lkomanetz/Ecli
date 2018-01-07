using System;

namespace Ecli.FileReaders {

	public class EmptyFileContents : IFileReaderContents {

		public string RawContents => String.Empty;

	}

}