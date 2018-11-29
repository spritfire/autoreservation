using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public List<Kunde> List()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.ToList();
            }
        }

        public Kunde GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.Single(k => k.Id == id);
            }
        }

        public void Insert(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Added;
                context.SaveChanges();
            }
        }
        
        public void Update(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}