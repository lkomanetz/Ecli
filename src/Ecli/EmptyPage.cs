using System;

namespace Ecli {

	public class EmptyPage : IConsolePage {

		public EmptyPage() => this.Id = Guid.NewGuid();

		public Guid Id { get; }

		public void Show() { }

	}

}