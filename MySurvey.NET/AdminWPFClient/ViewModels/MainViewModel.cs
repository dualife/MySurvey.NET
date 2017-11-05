// <copyright file="MainViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Services;
using AdminWPFClient.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private string _windowTitleLabel = "Tableau de bord des formulaires";
        private string _accountLabel = string.Empty;
        private RelayCommand<string> _tabSelectionChangedCmd = null;
        private RelayCommand _helpCmd = null;
        private RelayCommand _editAccountCmd = null;
        private RelayCommand _logoffCmd = null;

        public MainViewModel(IUserService userService)
        {
            this._userService = userService;
            this.AccountLabel = "Bienvenue, " + userService.GetLoggedUsername();
        }

        public string WindowTitleLabel
        {
            get => this._windowTitleLabel;

            set
            {
                if (value != this._windowTitleLabel)
                    this.Set(ref this._windowTitleLabel, value);
            }
        }

        public string AccountLabel
        {
            get => this._accountLabel;

            set
            {
                if (value != this._accountLabel)
                    this.Set(ref this._accountLabel, value);
            }
        }

        public RelayCommand<string> TabSelectionChangedCmd
        {
            get => this._tabSelectionChangedCmd
                   ?? (this._tabSelectionChangedCmd = new RelayCommand<string>(
                       this.TabChangedAction));

            set
            {
                if (value != this._tabSelectionChangedCmd)
                    this.Set(ref this._tabSelectionChangedCmd, value);
            }
        }

        public RelayCommand HelpCmd
        {
            get
            {
                return this._helpCmd
                  ?? (this._helpCmd = new RelayCommand(
                      () =>
                      {
                          MessageBox.Show("Lis la doc!");
                      }));
            }

            set
            {
                if (value != this._helpCmd)
                    this.Set(ref this._helpCmd, value);
            }
        }

        public RelayCommand EditAccountCmd
        {
            get => this._editAccountCmd
                   ?? (this._editAccountCmd = new RelayCommand(EditAccountAction));

            set
            {
                if (value != this._editAccountCmd)
                    this.Set(ref this._editAccountCmd, value);
            }
        }



        public RelayCommand LogoffCmd
        {
            get => this._logoffCmd
                   ?? (this._logoffCmd = new RelayCommand(this.LogoffAction));

            set
            {
                if (value != this._logoffCmd)
                    this.Set(ref this._logoffCmd, value);
            }
        }

        private void TabChangedAction(string tabName)
        {
            switch (tabName)
            {
                case "AcceuilTi":
                    this.WindowTitleLabel = "Tableau de bord des formulaires";
                    break;
                case "ArchivesTi":
                    this.WindowTitleLabel = "Archives des formulaires";
                    break;
                case "MentionsTi":
                    this.WindowTitleLabel = "Mentions";
                    break;
                case "ConfirmationTi":
                    this.WindowTitleLabel = "Confirmation";
                    break;
                case "EndingTi":
                    this.WindowTitleLabel = "Clôture";
                    break;
            }
        }

        private static void EditAccountAction()
        {
            var newWindow = new EditAccountWindow();
            newWindow.ShowDialog();
        }

        private void LogoffAction()
        {
            _userService.Logoff();
            // TODO: clean the user forms services and all

            var currentWindow = Application.Current.Windows[0];
            var newWindow = new LoginWindow();
            currentWindow?.Close();
            newWindow.Show();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}