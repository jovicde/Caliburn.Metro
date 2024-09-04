﻿using Caliburn.Micro;
using System.Dynamic;
using System.Windows;

namespace Caliburn.Metro.Autofac.Sample
{
    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Caliburn Metro Autofac Sample";

        private readonly IWindowManager _windowManager;
        public AppViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public async void OpenWindow()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;

            await _windowManager.ShowWindowAsync(new AppViewModel(_windowManager), null, settings);
        }

        public void OpenSettings()
        {
            IsSettingsFlyoutOpen = true;
        }

        private bool _isSettingsFlyoutOpen;

        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                _isSettingsFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
            }
        }

    }
}