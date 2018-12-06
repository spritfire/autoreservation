using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void UpdateKundeNachnameTest()
        {
            Kunde kunde = Target.GetById(1);
            Assert.NotNull(kunde);
            kunde.Nachname = "Meier";
            Target.Update(kunde);

            kunde = Target.GetById(1);
            Assert.NotNull(kunde);
            Assert.Equal("Meier", kunde.Nachname);
        }
        
        [Fact]
        public void UpdateKundeVornameTest()
        {
            Kunde kunde = Target.GetById(1);
            Assert.NotNull(kunde);
            kunde.Vorname = "Hans";
            Target.Update(kunde);

            kunde = Target.GetById(1);
            Assert.NotNull(kunde);
            Assert.Equal("Hans", kunde.Vorname);
        }
    }
}
