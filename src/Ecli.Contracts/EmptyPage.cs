using System;

namespace Ecli.Contracts {

	public class EmptyPage : IConsolePage {

		public EmptyPage() => this.Id = Guid.NewGuid();

		public Guid Id { get; }

		public void Show() { }

	}

}