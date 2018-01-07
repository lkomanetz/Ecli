using System;
using System.Collections.Generic;
using Ecli.Contracts;

namespace Ecli {

	public class DisplayManager {

		private Stack<IConsolePage> _pageStack;

		public DisplayManager() => _pageStack = new Stack<IConsolePage>();

		public IConsolePage CurrentPage => (_pageStack.Count != 0) ? _pageStack.Peek() : new EmptyPage();

		public void Show(IConsolePage pageToShow) {
			if (this.CurrentPage.Id != pageToShow.Id) _pageStack.Push(pageToShow);
			this.CurrentPage.Show();
		}

		public void CloseCurrentPage() => _pageStack.Pop();

	}

}