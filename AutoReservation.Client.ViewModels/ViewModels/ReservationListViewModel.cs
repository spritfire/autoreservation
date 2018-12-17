using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class ReservationListViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ReservationDto> _reservationenListe;
        public List<ReservationDto> _sortedReservationenListe;
        private IAutoReservationService _target;
        public ReservationDto _selectedReservation;
        public bool _filtered;

        public ReservationListViewModel(IAutoReservationService target)
        {
            _target = target;
            _filtered = true;
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
                SetProperty(ref _selectedReservation, value);
            }
        }

        public List<ReservationDto> SortedReservationenListe
        {
            get { return _sortedReservationenListe; }
            set { SetProperty(ref _sortedReservationenListe, value); }
        }

        public bool Filtered
        {
            get { return _filtered; }
            set
            {
                _filtered = value;
                SortedReservationenListe = SortAscending(_reservationenListe);
            }
        }

        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }

        public void RefreshList()
        {
            ReservationenListe = new ObservableCollection<ReservationDto>(_target.ReservationList());
        }

        private List<ReservationDto> SortAscending(ObservableCollection<ReservationDto> coll)
        {
            List<ReservationDto> sortedList = coll.OrderBy(r => r.Von).ToList();
            if (Filtered)
            {
                sortedList.RemoveAll(r => r.Bis < DateTime.Now);
            }
            return sortedList;
        }
    }
}
