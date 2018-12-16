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
        public List<ReservationDto> SortedReservationenListe { get; private set; }
        private IAutoReservationService _target;
        public ReservationDto _selectedReservation;

        public ReservationListViewModel(IAutoReservationService target)
        {
            _target = target;
            ReservationenListe = new ObservableCollection<ReservationDto>(target.ReservationList());
        }

        public ObservableCollection<ReservationDto> ReservationenListe
        {
            get { return _reservationenListe; }
            set
            {
                _reservationenListe = value;
                SortedReservationenListe = SortAscending(_reservationenListe);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ReservationenListe"));
            }
        }

        public ReservationDto SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedReservation"));
            }
        }

        public void RefreshList()
        {
            ReservationenListe = new ObservableCollection<ReservationDto>(_target.ReservationList());
        }

        private List<ReservationDto> SortAscending(ObservableCollection<ReservationDto> list)
        {
            return list.OrderBy(r => r.Von).ToList();
        }
    }
}
