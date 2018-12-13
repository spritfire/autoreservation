using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Service.Wcf;
using AutoReservation.Common.Interfaces;
using System.ComponentModel;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class KundeListViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<KundeDto> _kundenListe;
        private IAutoReservationService _target;
        public KundeDto _selectedKunde;

        public KundeListViewModel(IAutoReservationService target)
        {
            _target = target;
            _kundenListe = new ObservableCollection<KundeDto>(_target.KundeList());
        }

        public ObservableCollection<KundeDto> KundenListe
        {
            get { return _kundenListe; }
            set
            {
                _kundenListe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KundenListe"));
            }
        }

        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set
            {
                _selectedKunde = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedKunde"));
            }
        }

        public void RefreshList()
        {
            KundenListe = new ObservableCollection<KundeDto>(_target.KundeList());
        }
    }
}
