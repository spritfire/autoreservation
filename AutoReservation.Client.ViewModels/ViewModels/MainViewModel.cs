using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    class MainViewModel
    {
        private IAutoReservationService _target;
        private KundeListViewModel klvm;
        private AutoListViewModel alvm;
        private ReservationListViewModel rlvm;

        public MainViewModel(IAutoReservationService target)
        {
            _target = target;
            klvm = new KundeListViewModel(target);
            alvm = new AutoListViewModel(target);
            rlvm = new ReservationListViewModel(target);

        }
    }
}
