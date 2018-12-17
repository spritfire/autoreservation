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
using System.Windows;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class AutoDetailViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigationService _navService;

        public RelayCommand SaveAutoCommand { get; set; }
        public RelayCommand RemoveAutoCommand { get; set; }

        private int _basistarif;
        private int _id;
        private string _marke;
        private byte[] _rowVersion;
        private int _tagestarif;
        private AutoKlasse _autoKlasse;
        private string _autoKlasseAsString;

        public AutoListViewModel CurrentAutoListViewModel { get; set; }
        public List<string> AutoKlassen { get; set; }

        private IAutoReservationService _target;

        public AutoDetailViewModel(INavigationService navService, IAutoReservationService target, AutoListViewModel currentAutoListViewModel)
        {
            _navService = navService;
            _target = target;
            CurrentAutoListViewModel = currentAutoListViewModel;
            SaveAutoCommand = new RelayCommand(SaveAuto);
            RemoveAutoCommand = new RelayCommand(RemoveAuto);
            AutoKlassen = Enum.GetValues(typeof(AutoKlasse)).Cast<AutoKlasse>().Select(v => v.ToString()).ToList();
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
            set
            {
                _autoKlasseAsString = value.ToString();
                SetProperty(ref _autoKlasse, value);
            }
        }

        public string AutoKlasseAsString
        {
            get { return _autoKlasseAsString; }
            set
            {
                _autoKlasseAsString = value;
                AutoKlasse = (AutoKlasse)Enum.Parse(typeof(AutoKlasse), value);
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

        private void SaveAuto()
        {
            if (_rowVersion == null)
            {
                InsertAuto();
            } else
            {
                UpdateAuto();
            }
        }

        private void InsertAuto()
        {
            _target.InsertAuto(new AutoDto
            {
                Basistarif = _basistarif,
                Marke = _marke,
                Tagestarif = _tagestarif,
                AutoKlasse = _autoKlasse
            });
            onClose();
        }

        private void UpdateAuto()
        {
            try
            {
                _target.UpdateAuto(new AutoDto
                {
                    Basistarif = _basistarif,
                    Id = _id,
                    Marke = _marke,
                    RowVersion = _rowVersion,
                    Tagestarif = _tagestarif,
                    AutoKlasse = _autoKlasse
                });
            }
            catch (FaultException<OptimisticConcurrencyFault>)
            {
                MessageBox.Show("Oups, somebody got there first. The car has already been altered or deleted. Try again!");
            }
            onClose();
        }

        private void RemoveAuto()
        {
            if (_rowVersion != null && UserIsSure())
            {
                try
                {
                    _target.RemoveAuto(new AutoDto
                    {
                        Basistarif = _basistarif,
                        Id = _id,
                        Marke = _marke,
                        RowVersion = _rowVersion,
                        Tagestarif = _tagestarif,
                        AutoKlasse = _autoKlasse
                    });
                }
                catch (FaultException<OptimisticConcurrencyFault>)
                {
                    MessageBox.Show("Oups, somebody got there first. The car has already been altered or deleted. Try again!");
                }
                onClose();
            }
        }

        private void onClose()
        {
            _navService.CloseWindow();
            CurrentAutoListViewModel.RefreshList();
        }

        private bool UserIsSure()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to permenantly delete this car?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return (result == MessageBoxResult.Yes) ? true : false;
        }
    }
}
