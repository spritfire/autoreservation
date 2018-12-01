using System;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService Target { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            var autoList = Target.AutoList();

            Assert.NotNull(autoList);
            Assert.True(autoList.Count > 0);
        }

        [Fact]
        public void GetKundenTest()
        {
            var kundeList = Target.KundeList();

            Assert.NotNull(kundeList);
            Assert.True(kundeList.Count > 0);
        }

        [Fact]
        public void GetReservationenTest()
        {
            var reservationList = Target.ReservationList();

            Assert.NotNull(reservationList);
            Assert.True(reservationList.Count > 0);
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            AutoDto auto = Target.GetAutoById(1);

            Assert.NotNull(auto);
            Assert.Equal(1, auto.Id);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            KundeDto kunde = Target.GetKundeById(1);

            Assert.NotNull(kunde);
            Assert.Equal(1, kunde.Id);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.GetReservationById(1);

            Assert.NotNull(reservation);
            Assert.Equal(1, reservation.ReservationsNr);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            AutoDto auto = Target.GetAutoById(-1);

            Assert.Null(auto);
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            KundeDto kunde = Target.GetKundeById(-1);

            Assert.Null(kunde);
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            ReservationDto reservation = Target.GetReservationById(-1);

            Assert.Null(reservation);
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            AutoDto newAuto = new AutoDto
            {
                Id = 5,
                Marke = "Renault",
                Tagestarif = 80
            };

            Target.InsertAuto(newAuto);
            AutoDto autoRead = Target.GetAutoById(5);
            Assert.Equal(newAuto.ToString(), autoRead.ToString());
        }

        [Fact]
        public void InsertKundeTest()
        {
            KundeDto newKunde = new KundeDto
            {
                Id = 5,
                Nachname = "Illi",
                Vorname = "Dominique"
            };

            Target.InsertKunde(newKunde);
            KundeDto kundeRead = Target.GetKundeById(5);
            Assert.Equal(kundeRead.ToString(), newKunde.ToString());
        }

        [Fact]
        public void InsertReservationTest()
        {
            ReservationDto newReservation = new ReservationDto
            {
                ReservationsNr = 5,
                Von = new DateTime(2019, 02, 01),
                Bis = new DateTime(2019, 02, 14),
            };

            Target.InsertReservation(newReservation);
            ReservationDto reservationRead = Target.GetReservationById(5);
            Assert.Equal(reservationRead.ToString(), newReservation.ToString());
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            AutoDto autoToDelete = Target.GetAutoById(4);
            Target.RemoveAuto(autoToDelete);

            AutoDto autoDeleted = Target.GetAutoById(4);
            Assert.Null(autoDeleted);
        }

        [Fact]
        public void DeleteKundeTest()
        {
            KundeDto kundeToDelete = Target.GetKundeById(4);
            Target.RemoveKunde(kundeToDelete);

            KundeDto kundeDeleted = Target.GetKundeById(4);
            Assert.Null(kundeDeleted);
        }

        [Fact]
        public void DeleteReservationTest()
        {
            ReservationDto reservationToDelete = Target.GetReservationById(4);
            Target.RemoveReservation(reservationToDelete);

            ReservationDto reservationDeleted = Target.GetReservationById(4);
            Assert.Null(reservationDeleted);
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto newReservation = new ReservationDto
            {
                ReservationsNr = 6,
                Von = new DateTime(2019, 02, 01),
                Bis = new DateTime(2019, 01, 14),
            };

            Target.InsertReservation(newReservation);
            ReservationDto reservationRead = Target.GetReservationById(5);
            Assert.Equal(reservationRead.ToString(), newReservation.ToString());
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}
