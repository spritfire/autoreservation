using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void UpdateReservationVonTest()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            reservation.Von = DateTime.Today;
            Target.Update(reservation);

            reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            Assert.Equal(DateTime.Today, reservation.Von);
        }
        
        [Fact]
        public void UpdateReservationBisTest()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            reservation.Bis = DateTime.Today.AddYears(3);
            Target.Update(reservation);

            reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            Assert.Equal(DateTime.Today.AddYears(3), reservation.Bis);
        }
        
        [Fact]
        public void UpdateReservationKundeTest()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            reservation.KundeId = 2;
            Target.Update(reservation);

            reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            Assert.Equal(2, reservation.KundeId);
        }
        
        [Fact]
        public void UpdateReservationAutoTest()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            reservation.AutoId = 4;
            Target.Update(reservation);

            reservation = Target.GetByReservationNr(1);
            Assert.NotNull(reservation);
            Assert.Equal(4, reservation.AutoId);
        }
    }
}
