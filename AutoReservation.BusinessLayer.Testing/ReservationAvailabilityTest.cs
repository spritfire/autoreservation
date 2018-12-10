using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void ScenarioOkay01Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 20);
            reservation.Bis = new DateTime(2020, 01, 30);
            Target.Update(reservation);

            Reservation changedReservation = Target.GetByReservationNr(1);
            Assert.Equal(2, changedReservation.AutoId);
            Assert.Equal(new DateTime(2020, 01, 20), changedReservation.Von);
            Assert.Equal(new DateTime(2020, 01, 30), changedReservation.Bis);
        }

        [Fact]
        public void ScenarioOkay02Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 01);
            reservation.Bis = new DateTime(2020, 01, 10);
            Target.Update(reservation);

            Reservation changedReservation = Target.GetByReservationNr(1);
            Assert.Equal(2, changedReservation.AutoId);
            Assert.Equal(new DateTime(2020, 01, 01), changedReservation.Von);
            Assert.Equal(new DateTime(2020, 01, 10), changedReservation.Bis);
        }

        [Fact]
        public void ScenarioOkay03Test()
        {
            Reservation reservation = new Reservation();
            reservation.AutoId = 1;
            reservation.Von = new DateTime(2020, 01, 01);
            reservation.Bis = new DateTime(2020, 01, 09);
            reservation.KundeId = 1;
            Target.Insert(reservation);

            Reservation newReservation = Target.GetByReservationNr(5);
            Assert.NotNull(newReservation);
        }

        [Fact]
        public void ScenarioOkay04Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 21);
            reservation.Bis = new DateTime(2020, 01, 31);
            Target.Update(reservation);

            Reservation changedReservation = Target.GetByReservationNr(1);
            Assert.Equal(2, changedReservation.AutoId);
            Assert.Equal(new DateTime(2020, 01, 21), changedReservation.Von);
            Assert.Equal(new DateTime(2020, 01, 31), changedReservation.Bis);
        }
        
        [Fact]
        public void ScenarioOkay05Test()
        {
            Reservation reservation = Target.GetByReservationNr(3);
            reservation.Von = new DateTime(2020, 01, 12);
            reservation.Bis = new DateTime(2020, 01, 17);
            Target.Update(reservation);

            Reservation changedReservation = Target.GetByReservationNr(3);
            Assert.Equal(new DateTime(2020, 01, 12), changedReservation.Von);
            Assert.Equal(new DateTime(2020, 01, 17), changedReservation.Bis);
        }

        [Fact]
        public void ScenarioNotOkay01Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 10);
            reservation.Bis = new DateTime(2020, 01, 20);

            Assert.Throws<AutoUnavailableException>(() => Target.Update(reservation));           
        }

        [Fact]
        public void ScenarioNotOkay02Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 05);
            reservation.Bis = new DateTime(2020, 01, 15);

            Assert.Throws<AutoUnavailableException>(() => Target.Update(reservation));
        }

        [Fact]
        public void ScenarioNotOkay03Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 05);
            reservation.Bis = new DateTime(2020, 01, 25);

            Assert.Throws<AutoUnavailableException>(() => Target.Update(reservation));
        }

        [Fact]
        public void ScenarioNotOkay04Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 12);
            reservation.Bis = new DateTime(2020, 01, 17);

            Assert.Throws<AutoUnavailableException>(() => Target.Update(reservation));
        }

        [Fact]
        public void ScenarioNotOkay05Test()
        {
            Reservation reservation = Target.GetByReservationNr(1);
            reservation.AutoId = 2;
            reservation.Von = new DateTime(2020, 01, 15);
            reservation.Bis = new DateTime(2020, 01, 25);

            Assert.Throws<AutoUnavailableException>(() => Target.Update(reservation));
        }
    }
}
