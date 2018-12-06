using System;
using System.Collections.Generic;
using System.Linq;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        public List<Reservation> List()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Reservationen.ToList();
            }
        }

        public Reservation GetByReservationNr(int reservationNr)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Reservationen.Single(r => r.ReservationsNr == reservationNr);
                return new Reservation();
            }
        }

        public void Insert(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    if (isDateRangeValid(reservation.Von, reservation.Bis) &&
                        isAutoAvailable(reservation.Auto.Id, reservation.Von, reservation.Bis))
                    {
                    }

                    context.Entry(reservation).State = EntityState.Added;
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        public void Update(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    if (!isDateRangeValid(reservation.Von, reservation.Bis))
                    {
                        throw new InvalidDateRangeException("No valid reservation dates", reservation.Von, reservation.Bis);
                    }

                    if (isAutoAvailable(reservation.Auto.Id, reservation.Von, reservation.Bis))
                    {
                        throw new AutoUnavailableException($"The car {reservation.Auto.Marke} is not available in this date range from reservation");
                    }

                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        private bool isDateRangeValid(DateTime von, DateTime bis)
        {
            return (von.Date < bis.Date) && (bis.Date - von.Date).TotalHours >= 24;
        }

        private bool isAutoAvailable(int autoId, DateTime von, DateTime bis)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var count = (from reservation in context.Reservationen
                    where reservation.Auto.Id == autoId &&
                          (reservation.Von < von && reservation.Bis >= von) ||
                          (reservation.Von <= bis && reservation.Bis > bis)
                    select reservation).Count();
                return count == 0;
            }
        }

        public void Remove(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(reservation).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }
    }
}