using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System.ComponentModel;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class AutoListViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AutoDto> _autosListe { get; set; }
        private IAutoReservationService _target;

        public AutoListViewModel(IAutoReservationService target)
        {
            _target = target;
            _autosListe = new ObservableCollection<AutoDto>(target.AutoList());
        }

        public ObservableCollection<AutoDto> AutosListe
        {
            get { return _autosListe; }
            set
            {
                _autosListe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AutosListe"));
            }
        }

        public void RefreshList()
        {
            AutosListe = new ObservableCollection<AutoDto>(_target.AutoList());
        }
    }
}
