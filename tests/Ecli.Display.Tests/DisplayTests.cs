using Ecli;
using Ecli.Contracts;
using System;
using Xunit;

namespace Ecli.Display.Tests {

    public class DisplayTests {

        [Fact]
        public void DisplayClassInstantiates() {
            var display = new DisplayManager();
        }

        [Fact]
        public void CurrentPageIsEmptyPageWhenStackIsEmpty() {
            var display = new DisplayManager();
            Assert.True(display.CurrentPage.GetType() == typeof(EmptyPage));
        }

        [Fact]
        public void AddingMultiplePagesSucceeds() {
            IConsolePage pageOne = new EmptyPage();
            IConsolePage pageTwo = new EmptyPage();

            var display = new DisplayManager();
            display.Show(pageOne);
            Assert.True(display.CurrentPage.Id == pageOne.Id);

            display.Show(pageTwo);
            Assert.True(display.CurrentPage.Id == pageTwo.Id);
        }

        [Fact]
        public void ClosingPagesSucceeds() {
            IConsolePage pageOne = new EmptyPage();
            IConsolePage pageTwo = new EmptyPage();

            var display = new DisplayManager();
            display.Show(pageOne);
            display.Show(pageTwo);

            display.CloseCurrentPage();

            Assert.True(display.CurrentPage.Id == pageOne.Id);
        }

    }

}