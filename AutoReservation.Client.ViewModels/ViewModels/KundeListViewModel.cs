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
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class KundeListViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<KundeDto> _kundenCollection;
        private List<KundeDto> _sortedKundenListe;
        private IAutoReservationService _target;
        public KundeDto _selectedKunde;

        private PropertyInfo _sortProperty;
        public PropertyInfo[] SortOptionsList { get; set; }

        public KundeListViewModel(IAutoReservationService target)
        {
            _target = target;
            _kundenCollection = new ObservableCollection<KundeDto>(_target.KundeList());
            SortOptionsList = getSortOptions();
            SortedKundenListe = SortCollection();
        }

        public ObservableCollection<KundeDto> KundenCollection
        {
            get { return _kundenCollection; }
            set
            {
                _kundenCollection = value;
                SortedKundenListe = SortCollection();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KundenListe"));
            }
        }

        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set
            {
                SetProperty(ref _selectedKunde, value);
            }
        }

        public List<KundeDto> SortedKundenListe
        {
            get { return _sortedKundenListe; }
            set
            {
                SetProperty(ref _sortedKundenListe, value);
            }
        }

        public PropertyInfo SortProperty
        {
            get { return _sortProperty; }
            set
            {
                _sortProperty = value;
                SortedKundenListe = SortCollection();
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
            KundenCollection = new ObservableCollection<KundeDto>(_target.KundeList());
        }

        private List<KundeDto> SortCollection()
        {
            if (SortProperty == null)
            {
                return KundenCollection.ToList();
            }
            else
            {
                return KundenCollection.OrderBy(kunde => SortProperty.GetValue(kunde, null)).ToList();
            }
        }

        private PropertyInfo[] getSortOptions()
        {
            PropertyInfo[] allProperties = typeof(KundeDto).GetProperties();
            PropertyInfo[] sortOptions = new PropertyInfo[allProperties.Length - 1];
            int index = 0;
            foreach (PropertyInfo p in allProperties)
            {
                if (!p.Equals(typeof(KundeDto).GetProperty("RowVersion")))
                {
                    sortOptions[index] = p;
                    index++;
                }
            }
            return sortOptions;
        }
    }
}
