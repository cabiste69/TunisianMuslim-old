﻿using System;
using System.Collections.Generic;
using TunisiaPrayer.ViewModels;
using TunisiaPrayer.Views;
using Xamarin.Forms;

namespace TunisiaPrayer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PrayerTimePage), typeof(PrayerTimePage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(ChapterPage), typeof(ChapterPage));
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
