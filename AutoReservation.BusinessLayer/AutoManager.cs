﻿using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        public List<Auto> List()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.ToList();
            }
        }

        public Auto GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.Single(a => a.Id == id);
            }
        }

        public void Insert(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Added;
                context.SaveChanges();
            }
        }
        
        public void Update(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}