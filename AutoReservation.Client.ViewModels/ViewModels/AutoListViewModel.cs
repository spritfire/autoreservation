using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AutoReservation.Client.ViewModels.ViewModels
{
    public class AutoListViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AutoDto> _autosCollection { get; set; }
        private List<AutoDto> _sortedAutosListe;
        private IAutoReservationService _target;
        public AutoDto _selectedAuto;

        private PropertyInfo _sortProperty;
        public PropertyInfo[] SortOptionsList { get; set; }

        public AutoListViewModel(IAutoReservationService target)
        {
            _target = target;
            _autosCollection = new ObservableCollection<AutoDto>(target.AutoList());
            SortOptionsList = getSortOptions();
            SortedAutosListe = SortCollection();
        }

        public ObservableCollection<AutoDto> AutosCollection
        {
            get { return _autosCollection; }
            set
            {
                _autosCollection = value;
                SortedAutosListe = SortCollection();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AutosListe"));
            }
        }

        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set
            {
                SetProperty(ref _selectedAuto, value);
            }
        }

        public List<AutoDto> SortedAutosListe
        {
            get { return _sortedAutosListe; }
            set
            {
                SetProperty(ref _sortedAutosListe, value);
            }
        }

        public PropertyInfo SortProperty
        {
            get { return _sortProperty; }
            set
            {
                _sortProperty = value;
                SortedAutosListe = SortCollection();
                
            }
        }

        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if(Equals(field, value))
            {
                return false;
            }
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }

        public void RefreshList()
        {
            AutosCollection = new ObservableCollection<AutoDto>(_target.AutoList());
        }

        private List<AutoDto> SortCollection()
        {
            if (SortProperty == null)
            {
                return AutosCollection.ToList();
            } else
            {
                return AutosCollection.OrderBy(auto => SortProperty.GetValue(auto, null)).ToList();
            }
        }

        private PropertyInfo[] getSortOptions()
        {
            PropertyInfo[] allProperties = typeof(AutoDto).GetProperties();
            PropertyInfo[] sortOptions = new PropertyInfo[allProperties.Length - 1];
            int index = 0;
            foreach (PropertyInfo p in allProperties)
            {
                if (!p.Equals(typeof(AutoDto).GetProperty("RowVersion")))
                {
                    sortOptions[index] = p;
                    index++;
                }
            }
            return sortOptions;
        }
    }
}
