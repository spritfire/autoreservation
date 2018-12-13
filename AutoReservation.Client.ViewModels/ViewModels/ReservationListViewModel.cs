using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class ReservationListViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ReservationDto> _reservationenListe;
        private IAutoReservationService _target;

        public ReservationListViewModel(IAutoReservationService target)
        {
            _target = target;
            _reservationenListe = new ObservableCollection<ReservationDto>(target.ReservationList());
        }

        public ObservableCollection<ReservationDto> ReservationenListe
        {
            get { return _reservationenListe; }
            set
            {
                _reservationenListe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ReservationenListe"));
            }
        }

        public void RefreshList()
        {
            ReservationenListe = new ObservableCollection<ReservationDto>(_target.ReservationList());
        }
    }
}
