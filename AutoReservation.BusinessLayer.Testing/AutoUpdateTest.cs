using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTarifTest()
        {
            Auto auto = Target.GetById(1);
            Assert.NotNull(auto);
            auto.Tagestarif = 100;
            Target.Update(auto);

            auto = Target.GetById(1);
            Assert.NotNull(auto);
            Assert.Equal(100, auto.Tagestarif);
        }
        
        [Fact]
        public void UpdateAutoMarkeTest()
        {
            Auto auto = Target.GetById(1);
            Assert.NotNull(auto);
            auto.Marke = "Ford Fiesta";
            Target.Update(auto);

            auto = Target.GetById(1);
            Assert.NotNull(auto);
            Assert.Equal("Ford Fiesta", auto.Marke);
        }
    }
}