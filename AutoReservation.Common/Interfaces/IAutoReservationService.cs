using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        #region Auto
        [OperationContract]
        AutoDto GetAutoById(int autoId);
        [OperationContract]
        void InsertAuto(AutoDto auto);
        [OperationContract]
        void UpdateAuto(AutoDto auto);
        [OperationContract]
        void RemoveAuto(AutoDto auto);
        [OperationContract]
        List<AutoDto> AutoList();
        #endregion
        #region Kunde
        [OperationContract]
        KundeDto GetKundeById(int kundeId);
        [OperationContract]
        void InsertKunde(KundeDto kunde);
        [OperationContract]
        void UpdateKunde(KundeDto kunde);
        [OperationContract]
        void RemoveKunde(KundeDto kunde);
        [OperationContract]
        List<KundeDto> KundeList();
        #endregion
        #region Reservation
        [OperationContract]
        ReservationDto GetReservationById(int reservationId);
        [OperationContract]
        void InsertReservation(ReservationDto reservation);
        [OperationContract]
        void UpdateReservation(ReservationDto reservation);
        [OperationContract]
        void RemoveReservation(ReservationDto reservation);
        [OperationContract]
        List<ReservationDto> ReservationList();
        #endregion
    }
}
