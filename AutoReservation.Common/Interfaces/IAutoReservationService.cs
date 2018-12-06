using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
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
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
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
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void RemoveKunde(KundeDto kunde);

        [OperationContract]
        List<KundeDto> KundeList();
        #endregion

        #region Reservation
        [OperationContract]
        ReservationDto GetReservationById(int reservationId);

        [OperationContract]
        [FaultContract(typeof(AutoUnavailableFault))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        void InsertReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void RemoveReservation(ReservationDto reservation);

        [OperationContract]
        List<ReservationDto> ReservationList();
        #endregion

    }
}
