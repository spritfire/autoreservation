using System;
using System.ServiceModel;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
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
                Marke = "Renault",
                Tagestarif = 80
            };

            Target.InsertAuto(newAuto);
            AutoDto autoRead = Target.GetAutoById(5);
            Assert.Equal(newAuto.Marke, autoRead.Marke);
            Assert.Equal(newAuto.Tagestarif, autoRead.Tagestarif);
        }

        [Fact]
        public void InsertKundeTest()
        {
            KundeDto newKunde = new KundeDto
            {
                Geburtsdatum = new DateTime(1990, 01, 31),
                Nachname = "Illi",
                Vorname = "Dominique"
            };

            Target.InsertKunde(newKunde);
            KundeDto kundeRead = Target.GetKundeById(5);
            Assert.Equal(kundeRead.Geburtsdatum, newKunde.Geburtsdatum);
            Assert.Equal(kundeRead.Vorname, newKunde.Vorname);
            Assert.Equal(kundeRead.Nachname, newKunde.Nachname);
        }

        [Fact]
        public void InsertReservationTest()
        {
            ReservationDto newReservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2019, 02, 01),
                Bis = new DateTime(2019, 02, 14),
            };

            Target.InsertReservation(newReservation);
            ReservationDto reservationRead = Target.GetReservationById(5);
            Assert.Equal(reservationRead.Auto.Id, newReservation.Auto.Id);
            Assert.Equal(reservationRead.Kunde.Id, newReservation.Kunde.Id);
            Assert.Equal(reservationRead.Von, newReservation.Von);
            Assert.Equal(reservationRead.Bis, newReservation.Bis);
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
            AutoDto autoToUpdate = Target.GetAutoById(4);
            autoToUpdate.Tagestarif = 100;
            Target.UpdateAuto(autoToUpdate);

            AutoDto autoUpdated = Target.GetAutoById(4);
            Assert.NotNull(autoUpdated);
            Assert.Equal(autoToUpdate.Tagestarif, autoUpdated.Tagestarif);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            KundeDto kundeToUpdate = Target.GetKundeById(4);
            kundeToUpdate.Nachname = "Muster";
            kundeToUpdate.Vorname = "Hans";
            Target.UpdateKunde(kundeToUpdate);

            KundeDto kundeUpdated = Target.GetKundeById(4);
            Assert.NotNull(kundeUpdated);
            Assert.Equal(kundeToUpdate.Vorname, kundeUpdated.Vorname);
            Assert.Equal(kundeToUpdate.Nachname, kundeUpdated.Nachname);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            ReservationDto reservationToUpdate = Target.GetReservationById(4);
            reservationToUpdate.Auto = Target.GetAutoById(1);
            reservationToUpdate.Bis = new DateTime(2025, 11, 21);
            reservationToUpdate.Von = new DateTime(2025, 10, 20);
            Target.UpdateReservation(reservationToUpdate);

            ReservationDto reservationUpdated = Target.GetReservationById(4);
            Assert.NotNull(reservationUpdated);
            Assert.Equal(1, reservationUpdated.Auto.Id);
            Assert.Equal(reservationToUpdate.Bis, reservationUpdated.Bis);
            Assert.Equal(reservationToUpdate.Von, reservationUpdated.Von);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto auto1 = Target.GetAutoById(1);
            auto1.Tagestarif = 89;

            AutoDto auto2 = Target.GetAutoById(1);
            auto2.Tagestarif = 45;

            Target.UpdateAuto(auto1);
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() =>
                Target.UpdateAuto(auto2));
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kunde1 = Target.GetKundeById(1);
            kunde1.Nachname = "Meier";

            KundeDto kunde2 = Target.GetKundeById(1);
            kunde2.Nachname = "Müller";

            Target.UpdateKunde(kunde1);
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() =>
                Target.UpdateKunde(kunde2));
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto reservation1 = Target.GetReservationById(1);
            reservation1.Bis = new DateTime(2022, 10, 12);

            ReservationDto reservation2 = Target.GetReservationById(1);
            reservation2.Bis = new DateTime(2022, 10, 13);

            Target.UpdateReservation(reservation1);
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() =>
                Target.UpdateReservation(reservation2));
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto newReservation = new ReservationDto
            {
                ReservationsNr = 6,
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2019, 02, 01),
                Bis = new DateTime(2019, 01, 14),
            };

            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => Target.InsertReservation(newReservation));
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            ReservationDto newReservation = new ReservationDto
            {
                ReservationsNr = 7,
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20),
            };

            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.InsertReservation(newReservation));
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            ReservationDto reservationToUpdate = Target.GetReservationById(3);
            reservationToUpdate.Bis = new DateTime(2020, 01, 01);

            Assert.Throws<FaultException<InvalidDateRangeFault>>(
                () => Target.UpdateReservation(reservationToUpdate));
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            ReservationDto reservationToUpdate = Target.GetReservationById(3);
            reservationToUpdate.Auto = Target.GetAutoById(1);

            Assert.Throws<FaultException<AutoUnavailableFault>>(() =>
                Target.UpdateReservation(reservationToUpdate));
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            ReservationDto reservationToUpdate = Target.GetReservationById(3);
            reservationToUpdate.Auto = Target.GetAutoById(1);
            reservationToUpdate.Bis = new DateTime(2020, 01, 22);
            reservationToUpdate.Von = new DateTime(2020, 01, 21);
            Target.UpdateReservation(reservationToUpdate);

            ReservationDto reservationUpdated = Target.GetReservationById(3);
            Assert.Equal(reservationToUpdate.Auto.Id, reservationUpdated.Auto.Id);
            Assert.Equal(reservationToUpdate.Bis, reservationUpdated.Bis);
            Assert.Equal(reservationToUpdate.Von, reservationUpdated.Von);
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            ReservationDto reservationToUpdate = Target.GetReservationById(4);
            reservationToUpdate.Bis = new DateTime(2020, 01, 19);
            reservationToUpdate.Von = new DateTime(2020, 01, 11);
            Assert.Throws<FaultException<AutoUnavailableFault>>(() =>
                Target.UpdateReservation(reservationToUpdate));
        }

        #endregion
    }
}