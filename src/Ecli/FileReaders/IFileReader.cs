using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.FileReaders {

	public interface IFileReader {

		FileReaderResult Read(string settings);

	}

}
