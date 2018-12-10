using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void ScenarioOkay01Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.Von = new DateTime(2020, 01, 10, 00, 00, 00);
            reservation.Bis = new DateTime(2020, 01, 11, 00, 00, 00);
            Target.Update(reservation);

            Reservation changedReservation = Target.GetByReservationNr(1);
            Assert.Equal(new DateTime(2020, 01, 10, 00, 00, 00), reservation.Von);
            Assert.Equal(new DateTime(2020, 01, 11, 00, 00, 00), reservation.Bis);
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.Von = new DateTime(2020, 01, 10);
            reservation.Bis = new DateTime(2020, 01, 11);
            Target.Update(reservation);

            Reservation changedReservation = Target.GetByReservationNr(1);
            Assert.Equal(new DateTime(2020, 01, 10), reservation.Von);
            Assert.Equal(new DateTime(2020, 01, 11), reservation.Bis);
        }

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.Von = new DateTime(2020, 01, 11);
            reservation.Bis = new DateTime(2020, 01, 10);

            Assert.Throws<InvalidDateRangeException>(() => Target.Update(reservation));
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.Von = new DateTime(2020, 01, 10);
            reservation.Bis = new DateTime(2020, 01, 10);
            
            Assert.Throws<InvalidDateRangeException>(() => Target.Update(reservation));
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.Von = new DateTime(2020, 01, 10, 00, 00, 00);
            reservation.Bis = new DateTime(2020, 01, 10, 23, 59, 59);
            
            Assert.Throws<InvalidDateRangeException>(() => Target.Update(reservation));
        }
    }
}
