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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class ReservationDetailViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigationService _navService;

        public RelayCommand SaveReservationCommand { get; set; }
        public RelayCommand RemoveReservationCommand { get; set; }

        private DateTime _bis;
        private int _reservationsNr;
        private byte[] _rowVersion;
        private DateTime _von;
        private AutoDto _auto;
        private KundeDto _kunde;

        private string _autoAsString;
        private string _kundeAsString;

        public ReservationListViewModel CurrentReservationListViewModel { get; set; }
        public List<string> Autos { get; set; }
        public List<string> Kunden { get; set; }

        private IAutoReservationService _target;

        public ReservationDetailViewModel(INavigationService navService, IAutoReservationService target, ReservationListViewModel currentReservationListViewModel)
        {
            _navService = navService;
            _target = target;
            CurrentReservationListViewModel = currentReservationListViewModel;
            SaveReservationCommand = new RelayCommand(SaveReservation);
            RemoveReservationCommand = new RelayCommand(RemoveReservation);
            Autos = getList(_target.AutoList());
            Kunden = getList(_target.KundeList());
        }

        public DateTime Bis
        {
            get { return _bis; }
            set { SetProperty(ref _bis, value); }
        }

        public int ReservationNr
        {
            get { return _reservationsNr; }
            set { SetProperty(ref _reservationsNr, value); }
        }

        public byte[] RowVersion
        {
            get { return _rowVersion; }
            set { SetProperty(ref _rowVersion, value); }
        }

        public DateTime Von
        {
            get { return _von; }
            set { SetProperty(ref _von, value); }
        }

        public AutoDto Auto
        {
            get { return _auto; }
            set
            {
                _autoAsString = value.ToString();
                SetProperty(ref _auto, value);
            }
        }

        public KundeDto Kunde
        {
            get { return _kunde; }
            set
            {
                _kundeAsString = value.ToString();
                SetProperty(ref _kunde, value);
            }
        }

        public string AutoAsString
        {
            get { return _autoAsString; }
            set
            {
                _autoAsString = value;
                int id = extractId(value);
                Auto = _target.GetAutoById(id);
            }
        }

        public string KundeAsString
        {
            get { return _kundeAsString; }
            set
            {
                _kundeAsString = value;
                int id = extractId(value);
                Kunde = _target.GetKundeById(id);
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

        private void SaveReservation()
        {
            if (_rowVersion == null)
            {
                InsertReservation();
            } else
            {
                UpdateReservation();
            }
        }

        private void InsertReservation()
        {
            try
            {
                _target.InsertReservation(new ReservationDto
                {
                    Bis = _bis,
                    Von = _von,
                    Auto = _auto,
                    Kunde = _kunde
                });
                onClose();
            }
            catch (FaultException<AutoUnavailableFault>)
            {
                MessageBox.Show("This car is unavailable during the specified time frame. Please choose a different car.");
            }
            catch (FaultException<InvalidDateRangeFault>)
            {
                MessageBox.Show("Please enter a valid date range. Minimum reservation length must be one day.");
            }
        }

        private void UpdateReservation()
        {
            try
            {
                _target.UpdateReservation(new ReservationDto
                {
                    Bis = _bis,
                    ReservationsNr = _reservationsNr,
                    RowVersion = _rowVersion,
                    Von = _von,
                    Auto = _auto,
                    Kunde = _kunde
                });
                onClose();
            }
            catch (FaultException<AutoUnavailableFault>)
            {
                MessageBox.Show("This car is unavailable during the specified time frame. Please choose a different car.");
            }
            catch (FaultException<InvalidDateRangeFault>)
            {
                MessageBox.Show("Please enter a valid date range. Minimum reservation length must be one day.");
            }
            catch (FaultException<OptimisticConcurrencyFault>)
            {
                MessageBox.Show("Oups, somebody got there first. The customer has already been altered or deleted. Try again!");
                onClose();
            }
        }

        private void RemoveReservation()
        {
            if (_rowVersion != null && UserIsSure())
            {
                try
                {
                    _target.RemoveReservation(new ReservationDto
                    {
                        Bis = _bis,
                        ReservationsNr = _reservationsNr,
                        RowVersion = _rowVersion,
                        Von = _von,
                        Auto = _auto,
                        Kunde = _kunde
                    });
                }
                catch (FaultException<OptimisticConcurrencyFault>)
                {
                    MessageBox.Show("Oups, somebody got there first. The reservation has already been altered or deleted. Try again!");
                }
                onClose();
            }
        }

        private void onClose()
        {
            _navService.CloseWindow();
            CurrentReservationListViewModel.RefreshList();
        }

        private List<string> getList<T>(List<T> dtoList)
        {
            List<string> stringList = new List<string>();
            foreach (T item in dtoList)
            {
                stringList.Add(item.ToString());
            }
            return stringList;
        }

        private int extractId(string s)
        {
            return Int32.Parse(Regex.Match(s, @"\d+").Value);
        }

        private bool UserIsSure()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to permenantly delete this reservation?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return (result == MessageBoxResult.Yes) ? true : false;
        }
    }
}
