using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Service.Wcf;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Client.ViewModels
{
    public class KundeListViewModel
    {
        private List<KundeDto> KundenListe { get; set; }
        private IAutoReservationService _target;

        public KundeListViewModel(IAutoReservationService target)
        {
            _target = target;
            KundenListe = target.KundeList();
        }
    }
}
