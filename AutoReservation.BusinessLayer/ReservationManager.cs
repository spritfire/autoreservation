using System.Collections.Generic;
using System.Linq;
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

        public Reservation GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                //return context.Reservationen.Single(r => r.Id == id);
                return new Reservation();
            }
        }

        public void Insert(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }
        }
        
        public void Update(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}