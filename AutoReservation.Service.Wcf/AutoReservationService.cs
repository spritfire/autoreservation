using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService
    {
        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

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
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            new KundeManager()
                .Update(kundeEntity);
        }

        public void RemoveKunde(KundeDto kunde)
        {
            WriteActualMethod();
            Kunde kundeEntity = kunde.ConvertToEntity();
            new KundeManager()
                .Remove(kundeEntity);
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
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            new AutoManager()
                .Update(autoEntity);
        }

        public void RemoveAuto(AutoDto auto)
        {
            WriteActualMethod();
            Auto autoEntity = auto.ConvertToEntity();
            new AutoManager()
                .Remove(autoEntity);
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

        public ReservationDto GetReservationById(int reservationId)
        {
            WriteActualMethod();
            return new ReservationManager()
                .GetById(reservationId)
                .ConvertToDto();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            Reservation reservationEntity = reservation.ConvertToEntity();
            new ReservationManager()
                .Insert(reservationEntity);
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            Reservation reservationEntity = reservation.ConvertToEntity();
            new ReservationManager()
                .Update(reservationEntity);
        }

        public void RemoveReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            Reservation reservationEntity = reservation.ConvertToEntity();
            new ReservationManager()
                .Remove(reservationEntity);
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
    }
}