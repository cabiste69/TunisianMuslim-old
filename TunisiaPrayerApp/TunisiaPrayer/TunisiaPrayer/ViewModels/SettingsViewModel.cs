﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using TunisiaPrayer.Models;

namespace TunisiaPrayer.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {

        public List<Rootobject> statePicker { get; set; }
        public List<Delegation> delegatePicker { get; set; }

        public SettingsViewModel()
        {
            //populate the elements
            statePicker= App.statesData;
           
            delegatePicker = App.statesData[App.selectedStateIndex].Delegations;

        }



        //update the delegates on state change
        public int SelectedStateIndex
        {
            get { return App.selectedStateIndex; }
            set
            {
                App.selectedStateIndex = value;
                delegatePicker = App.statesData[App.selectedStateIndex].Delegations;
                OnPropertyChanged(nameof(delegatePicker));
            }
        }

        public int SelectedDelegate
        {
            get { return App.selectedDelegate; }
            set
            {
                App.selectedDelegate = value;
            }
        }
    }
}
