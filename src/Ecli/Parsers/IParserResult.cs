using Ecli.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecli.Parsers {

  public interface IParserResult {
    bool Success { get; }
    Exception Exception { get; }
  }

}