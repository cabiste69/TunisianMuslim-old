﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net;
using Xamarin.Forms.PlatformConfiguration;
using System.IO;
using TunisiaPrayer.Services;
using System.ComponentModel;

namespace TunisiaPrayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatesPage : ContentPage
    {
        public bool UpdateAvailable { get; set; } = false;
        public string IsDownloading { get; set; }
        IPathService pathService;
        IPackageInstaller packageInstaller;
        private string fileUri;
        public UpdatesPage()
        {
            InitializeComponent();
            BindingContext = this;
            pathService = DependencyService.Get<IPathService>();
        }

        private async void RefreshButton_Clicked(object sender, EventArgs e)
        {
            var client = new GitHubClient(new ProductHeaderValue("tunisiaPrayerApp"));
            //yes that's the repo's id, it's public
            var releases = await client.Repository.Release.GetAll(451233074);

            for (int i = 0; i < releases.Count; i++)
            {
                rel.Text = releases[i].Name;
            }

            //releases[0].TagName != VersionTracking.CurrentVersion
            if (true)
            {
                UpdateAvailable = true;
                OnPropertyChanged(nameof(UpdateAvailable));
                //versionDiff.Text = $"current is {AppInfo.VersionString}, new is {releases[0].TagName}";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await permissionDenied())
            {
                return;
            }

            rel.Text += "\n" + pathService.PublicExternalFolder;
            IsDownloading = "downloading";
            OnPropertyChanged(nameof(IsDownloading));
            Uri url = new Uri("https://github.com/cabiste69/TunisiaPrayer/releases/download/1.0.0/TunisiaPrayer-1.0.apk");

            GetFileUri();

            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFileAsync(url, fileUri);
            IsDownloading = "completed downloading";
            OnPropertyChanged(nameof(IsDownloading));
            myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(InstallUpdate);
        }

        private void InstallUpdate(object sender, AsyncCompletedEventArgs e)
        {

            packageInstaller.OnCreate(fileUri);
        }

        private async Task<bool> permissionDenied()
        {
            PermissionStatus permissionWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            PermissionStatus permissionRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (permissionWrite != PermissionStatus.Granted)
            {
                permissionWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            if (permissionRead != PermissionStatus.Granted)
            {
                permissionRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            return permissionWrite != PermissionStatus.Granted && permissionRead != PermissionStatus.Granted;

        }
        private void GetFileUri()
        {
            fileUri = pathService.PublicExternalFolder + "/test.apk";
        }

    }
}