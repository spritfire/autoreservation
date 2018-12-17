using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TabItems;

namespace AutoReservation.Client.Testing
{
    [TestClass]
    public class TestUI
    {
        public string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public string SutPath => Path.Combine(BaseDir, "AutoReservation.Client.UI.exe");

        [TestMethod]
        public void TestButtonNewReservation()
        {
            var app = Application.Launch(@"C:\Users\Michel\Desktop\autoreservation\AutoReservation.Client.UI\bin\Debug\AutoReservation.Client.UI.exe");
            var mainWindow = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var btnNewReservation = mainWindow.Get<Button>(SearchCriteria.ByText("New Reservation"));

            var windows = app.GetWindows();
            var query = from w in windows where w.Name.Equals("ReservationDetailWindow") select w;
            Assert.IsFalse(query.Any());
            btnNewReservation.Click();
            var reservatoinDetailWindow = app.GetWindow("ReservationDetailWindow", InitializeOption.NoCache);
            Assert.IsNotNull(reservatoinDetailWindow);
            app.Close();
        }

        [TestMethod]
        public void TestButtonNewCar()
        {
            var app = Application.Launch(@"C:\Users\Michel\Desktop\autoreservation\AutoReservation.Client.UI\bin\Debug\AutoReservation.Client.UI.exe");
            var mainWindow = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var tabControl = mainWindow.Get<Tab>(SearchCriteria.ByClassName("TabControl"));
            tabControl.SelectTabPage(1);
            var btnNewCar = mainWindow.Get<Button>(SearchCriteria.ByText("New Car"));

            var windows = app.GetWindows();
            var query = from w in windows where w.Name.Equals("AutoDetailWindow") select w;
            Assert.IsFalse(query.Any());
            btnNewCar.Click();
            var reservatoinDetailWindow = app.GetWindow("AutoDetailWindow", InitializeOption.NoCache);
            Assert.IsNotNull(reservatoinDetailWindow);
            app.Close();
        }

        [TestMethod]
        public void TestButtonNewCustomer()
        {
            var app = Application.Launch(@"C:\Users\Michel\Desktop\autoreservation\AutoReservation.Client.UI\bin\Debug\AutoReservation.Client.UI.exe");
            var mainWindow = app.GetWindow("MainWindow", InitializeOption.NoCache);
            var tabControl = mainWindow.Get<Tab>(SearchCriteria.ByClassName("TabControl"));
            tabControl.SelectTabPage(2);
            var btnNewCustomer = mainWindow.Get<Button>(SearchCriteria.ByText("New Customer"));

            var windows = app.GetWindows();
            var query = from w in windows where w.Name.Equals("KundeDetailWindow") select w;
            Assert.IsFalse(query.Any());
            btnNewCustomer.Click();
            var reservatoinDetailWindow = app.GetWindow("KundeDetailWindow", InitializeOption.NoCache);
            Assert.IsNotNull(reservatoinDetailWindow);
            app.Close();
        }
    }
}
