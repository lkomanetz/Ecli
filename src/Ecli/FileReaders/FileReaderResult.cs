using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.FileReaders {

	public class FileReaderResult {

		public FileReaderResult() {
			this.Exception = new EmptyException();
			this.Contents = new EmptyFileContents();
		}

		public FileReaderResult(Exception err) {
			this.Exception = err;
			this.Contents = new EmptyFileContents();
		}

		public FileReaderResult(IFileReaderContents contents) {
			this.Contents = contents;
			this.Exception = new EmptyException();
		}

		public string RawContents => this.Contents.RawContents;
		public IFileReaderContents Contents { get; private set; }
		public Exception Exception { get; private set; }
		public bool Success => this.Exception.GetType() == typeof(EmptyException);

	}

}
