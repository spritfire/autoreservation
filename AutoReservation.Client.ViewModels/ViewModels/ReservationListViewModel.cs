using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    class ReservationListViewModel
    {
        private List<ReservationDto> ReservationsListe { get; set; }
        private IAutoReservationService _target;

        public ReservationListViewModel(IAutoReservationService target)
        {
            _target = target;
            ReservationsListe = target.ReservationList();
        }
    }
}
