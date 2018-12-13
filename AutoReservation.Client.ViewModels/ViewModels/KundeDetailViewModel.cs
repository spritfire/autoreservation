using AutoReservation.Client.ViewModels.Navigation;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class KundeDetailViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigationService _navService;

        public RelayCommand SaveKundeCommand { get; set; }
        public RelayCommand RemoveKundeCommand { get; set; }

        private DateTime _geburtsdatum;
        private int _id;
        private string _nachname;
        private byte[] _rowVersion;
        private string _vorname;
        
        public KundeListViewModel Klvm { get; set; }

        private IAutoReservationService _target;

        public KundeDetailViewModel(INavigationService navService, IAutoReservationService target, KundeListViewModel klvm)
        {
            _navService = navService;
            _target = target;
            Klvm = klvm;
            SaveKundeCommand = new RelayCommand(SaveKunde);
            RemoveKundeCommand = new RelayCommand(RemoveKunde);
        }

        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set { SetProperty(ref _geburtsdatum, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Nachname
        {
            get { return _nachname; }
            set { SetProperty(ref _nachname, value); }
        }

        public byte[] RowVersion
        {
            get { return _rowVersion; }
            set { SetProperty(ref _rowVersion, value); }
        }

        public string Vorname
        {
            get { return _vorname; }
            set { SetProperty(ref _vorname, value); }
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

        public void SaveKunde()
        {
            if (_rowVersion == null)
            {
                _target.InsertKunde(new KundeDto
                {
                    Geburtsdatum = _geburtsdatum,
                    Nachname = _nachname,
                    Vorname = _vorname
                });
            } else
            {
                try
                {
                    _target.UpdateKunde(new KundeDto
                    {
                        Geburtsdatum = _geburtsdatum,
                        Nachname = _nachname,
                        Id = _id,
                        RowVersion = _rowVersion,
                        Vorname = _vorname
                    });
                }
                catch (FaultException<OptimisticConcurrencyFault> e)
                {
                    //Handle Fault
                }
            }
            _navService.CloseWindow();
            Klvm.RefreshList();
        }

        public void RemoveKunde()
        {
            try
            {
                _target.RemoveKunde(new KundeDto
                {
                    Geburtsdatum = _geburtsdatum,
                    Nachname = _nachname,
                    Id = _id,
                    RowVersion = _rowVersion,
                    Vorname = _vorname
                });
            }
            catch (FaultException<OptimisticConcurrencyFault> e)
            {
                //Handle Fault
            }
            Klvm.RefreshList();
        }
    }
}
