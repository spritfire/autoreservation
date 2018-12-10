using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    class AutoListViewModel
    {
        private List<AutoDto> AutoListe { get; set; }
        private IAutoReservationService _target;

        public AutoListViewModel(IAutoReservationService target)
        {
            _target = target;
            AutoListe = target.AutoList();
        }
    }
}
