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
    public class AutoDetailViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private INavigationService _navService;

        public RelayCommand InsertAutoCommand { get; set; }
        public RelayCommand UpdateAutoCommand { get; set; }
        public RelayCommand RemoveAutoCommand { get; set; }

        private int _basistarif;
        private int _id;
        private string _marke;
        private byte[] _rowVersion;
        private int _tagestarif;
        private AutoKlasse _autoKlasse;

        private IAutoReservationService _target;

        public AutoDetailViewModel(INavigationService navService, IAutoReservationService target)
        {
            _navService = navService;
            _target = target;
            InsertAutoCommand = new RelayCommand(InsertAuto);
            UpdateAutoCommand = new RelayCommand(UpdateAuto);
            RemoveAutoCommand = new RelayCommand(RemoveAuto);
        }

        public int Basistarif
        {
            get { return _basistarif; }
            set { SetProperty(ref _basistarif, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Marke
        {
            get { return _marke; }
            set { SetProperty(ref _marke, value); }
        }

        public byte[] RowVersion
        {
            get { return _rowVersion; }
            set { SetProperty(ref _rowVersion, value); }
        }

        public int Tagestarif
        {
            get { return _tagestarif; }
            set { SetProperty(ref _tagestarif, value); }
        }

        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set { SetProperty(ref _autoKlasse, value); }
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

        public void InsertAuto()
        {
            _target.InsertAuto(new AutoDto
            {
                Basistarif = _basistarif,
                Marke = _marke,
                Tagestarif = _tagestarif,
                AutoKlasse = _autoKlasse
            });
        }

        public void UpdateAuto()
        {
            try
            {
                _target.InsertAuto(new AutoDto
                {
                    Basistarif = _basistarif,
                    Id = _id,
                    Marke = _marke,
                    RowVersion = _rowVersion,
                    Tagestarif = _tagestarif,
                    AutoKlasse = _autoKlasse
                });
            }
            catch (FaultException<OptimisticConcurrencyFault> e)
            {
                //Handle Fault
            }
        }

        public void RemoveAuto()
        {
            try
            {
                _target.InsertAuto(new AutoDto
                {
                    Basistarif = _basistarif,
                    Id = _id,
                    Marke = _marke,
                    RowVersion = _rowVersion,
                    Tagestarif = _tagestarif,
                    AutoKlasse = _autoKlasse
                });
            }
            catch (FaultException<OptimisticConcurrencyFault> e)
            {
                //Handle Fault
            }
        }
    }
}
