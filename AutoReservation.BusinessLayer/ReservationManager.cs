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
                return context.Reservationen.Include(r => r.Auto).Include(r => r.Kunde).ToList();
            }
        }

        public Reservation GetByReservationNr(int reservationNr)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Reservationen.Include(r => r.Auto).Include(r => r.Kunde)
                    .Single(r => r.ReservationsNr == reservationNr);
            }
        }

        public void Insert(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                if (!isDateRangeValid(reservation.Von, reservation.Bis))
                {
                    throw new InvalidDateRangeException("No valid reservation dates", reservation.Von,
                        reservation.Bis);
                }

                if (!isAutoAvailable(reservation.ReservationsNr, reservation.AutoId, reservation.Von, reservation.Bis))
                {
                    throw new AutoUnavailableException(
                        $"The car {reservation.Auto.Marke} is not available in this date range from reservation");
                }

                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
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
                        throw new InvalidDateRangeException("No valid reservation dates", reservation.Von,
                            reservation.Bis);
                    }

                    if (!isAutoAvailable(reservation.ReservationsNr, reservation.AutoId, reservation.Von,
                        reservation.Bis))
                    {
                        throw new AutoUnavailableException(
                            $"The car {reservation.Auto.Marke} is not available in this date range from reservation");
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

        private bool isAutoAvailable(int reservationNr, int autoId, DateTime von, DateTime bis)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var count = (from reservation in context.Reservationen
                    where reservation.AutoId == autoId &&
                          reservation.ReservationsNr != reservationNr &&
                          ((von <= reservation.Von && bis > reservation.Von) ||
                          (von >= reservation.Von && von < reservation.Bis))
                    select reservation);
                return !count.Any();
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