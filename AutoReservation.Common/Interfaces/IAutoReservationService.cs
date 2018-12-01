using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        #region Auto
        AutoDto GetAutoById(int autoId);
        void InsertAuto(AutoDto auto);
        void UpdateAuto(AutoDto auto);
        void RemoveAuto(AutoDto auto);
        List<AutoDto> AutoList();
        #endregion
        #region Kunde
        KundeDto GetKundeById(int kundeId);
        void InsertKunde(KundeDto kunde);
        void UpdateKunde(KundeDto kunde);
        void RemoveKunde(KundeDto kunde);
        List<KundeDto> KundeList();
        #endregion
        #region Reservation
        ReservationDto GetReservationById(int reservationId);
        void InsertReservation(ReservationDto reservation);
        void UpdateReservation(ReservationDto reservation);
        void RemoveReservation(ReservationDto reservation);
        List<ReservationDto> ReservationList();
        #endregion
    }
}
