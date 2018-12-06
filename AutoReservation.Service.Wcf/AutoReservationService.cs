using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        #region Kunde
        public KundeDto GetKundeById(int kundeId)
        {
            WriteActualMethod();
            return new KundeManager()
                .GetById(kundeId)
                .ConvertToDto();
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            new KundeManager()
                .Insert(kundeEntity);
        }

        public void UpdateKunde(KundeDto kunde)
        {
            try
            {
                WriteActualMethod();
                Kunde kundeEntity = kunde.ConvertToEntity();
                new KundeManager()
                    .Update(kundeEntity);
            }
            catch (OptimisticConcurrencyException<Kunde> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault {
                        Operation = "Update",
                        Description = "Kunde was not updated, someone else beat you to it!"
                    }
                );
            }
        }

        public void RemoveKunde(KundeDto kunde)
        {
            try
            {
                WriteActualMethod();
                Kunde kundeEntity = kunde.ConvertToEntity();
                new KundeManager()
                    .Remove(kundeEntity);
            }
            catch (OptimisticConcurrencyException<Kunde> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault {
                        Operation = "Remove",
                        Description = "Kunde was not removed, someone else edited the Kunde or it has already been removed!"
                    }
                );
            }
        }

        public List<KundeDto> KundeList()
        {
            WriteActualMethod();
            List<KundeDto> kundeDtos = new List<KundeDto>();
            List<Kunde> kundeEntities = new KundeManager().List();
            foreach (Kunde k in kundeEntities)
            {
                kundeDtos.Add(k.ConvertToDto());
            }
            return kundeDtos;
        }
        #endregion
        #region Auto
        public AutoDto GetAutoById(int autoId)
        {
            WriteActualMethod();
            return new AutoManager()
                .GetById(autoId)
                .ConvertToDto();
        }

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            new AutoManager()
                .Insert(autoEntity);
        }

        public void UpdateAuto(AutoDto auto)
        {
            try
            {
                WriteActualMethod();
                Auto autoEntity = auto.ConvertToEntity();
                new AutoManager()
                    .Update(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault {
                        Operation = "Update",
                        Description = "Auto was not updated, someone else beat you to it!"
                    }
                );
            }
        }

        public void RemoveAuto(AutoDto auto)
        {
            try
            {
                WriteActualMethod();
                Auto autoEntity = auto.ConvertToEntity();
                new AutoManager()
                    .Remove(autoEntity);
            }
            catch (OptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Remove",
                        Description = "Auto was not removed, someone else edited the Auto or it has already been removed!"
                    }
                );
            }
        }

        public List<AutoDto> AutoList()
        {
            WriteActualMethod();
            List<AutoDto> autoDtos = new List<AutoDto>();
            List<Auto> autoEntities = new AutoManager().List();
            foreach (Auto a in autoEntities)
            {
                autoDtos.Add(a.ConvertToDto());
            }
            return autoDtos;
        }
        #endregion
        #region Reservation
        public ReservationDto GetReservationById(int reservationId)
        {
            WriteActualMethod();
            return new ReservationManager()
                .GetByReservationNr(reservationId)
                .ConvertToDto();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                Reservation reservationEntity = reservation.ConvertToEntity();
                new ReservationManager()
                    .Insert(reservationEntity);
            }
            catch (AutoUnavailableException e)
            {
                throw new FaultException<AutoUnavailableFault>(
                    new AutoUnavailableFault
                    {
                        Operation = "Insert",
                        Description = "Auto is not available. The Reservation was not created!"
                    }
                );
            }
            catch (InvalidDateRangeException e)
            {
                throw new FaultException<InvalidDateRangeFault>(
                    new InvalidDateRangeFault
                    {
                        Operation = "Insert",
                        Description = "The end date must be at least 24h after the start date. The Reservation was not created!"
                    }
                );
            }
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                Reservation reservationEntity = reservation.ConvertToEntity();
                new ReservationManager()
                    .Update(reservationEntity);
            }
            catch (AutoUnavailableException e)
            {
                throw new FaultException<AutoUnavailableFault>(
                    new AutoUnavailableFault
                    {
                        Operation = "Update",
                        Description = "The Auto you specified is not available. The Reservation was not updated!"
                    }
                );
            }
            catch (InvalidDateRangeException e)
            {
                throw new FaultException<InvalidDateRangeFault>(
                    new InvalidDateRangeFault
                    {
                        Operation = "Update",
                        Description = "The end date must be at least 24h after the start date. The Reservation was not updated!"
                    }
                );
            }
            catch (OptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Update",
                        Description = "Reservation was not updated, someone else beat you to it!"
                    }
                );
            }
        }

        public void RemoveReservation(ReservationDto reservation)
        {
            try
            {
                WriteActualMethod();
                Reservation reservationEntity = reservation.ConvertToEntity();
                new ReservationManager()
                    .Remove(reservationEntity);
            }
            catch (OptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<OptimisticConcurrencyFault>(
                    new OptimisticConcurrencyFault
                    {
                        Operation = "Remove",
                        Description = "Reservation was not removed, someone else edited the Reservation or it has already been removed!"
                    }
                );
            }
        }

        public List<ReservationDto> ReservationList()
        {
            WriteActualMethod();
            List<ReservationDto> reservationDtos = new List<ReservationDto>();
            List<Reservation> reservations = new ReservationManager().List();
            foreach (Reservation r in reservations)
            {
                reservationDtos.Add(r.ConvertToDto());
            }
            return reservationDtos;
        }
        #endregion
    }
}