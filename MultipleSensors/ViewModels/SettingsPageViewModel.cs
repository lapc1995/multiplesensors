using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AxLEConnector.Helpers;
using AxLEConnector.Services;
using MultipleSensors.Models;

namespace MultipleSensors.ViewModels
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        private int _selectedIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<FrequencyModel> Frequencies { get; private set; }

        public int SelectedIndex
        {
            get 
            {
                return _selectedIndex;
            }

            set
            {
                if(_selectedIndex != value)
                {
                    _selectedIndex = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedIndex"));
                }
            }
        }

        public SettingsPageViewModel()
        {
            Frequencies = new List<FrequencyModel>();
            SetFrequencies();
        }

        private void SetFrequencies()
        {
            var values = Enum.GetValues(typeof(StreamFrequency));
            for(int i = 0; i < values.Length; i++)
            {
                StreamFrequency sf = (StreamFrequency)values.GetValue(i);
                Frequencies.Add(new FrequencyModel(sf));
                if (sf == Devices.Instance.StreamFrequency)
                    _selectedIndex = i;
            }
        }
    }
}
