// <copyright file="MainViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using AdminWPFClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using AdminWPFClient.Windows;

namespace AdminWPFClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService userService;
        private string windowTitleLabel = "Tableau de bord des formulaires";
        private string accountLabel = string.Empty;
        private RelayCommand<string> tabSelectionChangedCmd = null;
        private RelayCommand helpCmd = null;
        private RelayCommand editAccountCmd = null;
        private RelayCommand logoffCmd = null;

        public MainViewModel(IUserService userService)
        {
            this.userService = userService;
            this.AccountLabel = "Bienvenue, " + userService.GetLoggedUsername();
        }

        public string WindowTitleLabel
        {
            get
            {
                return this.windowTitleLabel;
            }

            set
            {
                if (value != this.windowTitleLabel)
                    this.Set(ref this.windowTitleLabel, value);
            }
        }

        public string AccountLabel
        {
            get
            {
                return this.accountLabel;
            }

            set
            {
                if (value != this.accountLabel)
                    this.Set(ref this.accountLabel, value);
            }
        }

        public RelayCommand<string> TabSelectionChangedCmd
        {
            get
            {
                return this.tabSelectionChangedCmd
                  ?? (this.tabSelectionChangedCmd = new RelayCommand<string>(
                    this.TabChangedAction));
            }

            set
            {
                if (value != this.tabSelectionChangedCmd)
                    this.Set(ref this.tabSelectionChangedCmd, value);
            }
        }

        public RelayCommand HelpCmd
        {
            get
            {
                return this.helpCmd
                  ?? (this.helpCmd = new RelayCommand(
                      () =>
                      {
                          MessageBox.Show("Lis la doc!");
                      }));
            }

            set
            {
                if (value != this.helpCmd)
                    this.Set(ref this.helpCmd, value);
            }
        }

        public RelayCommand EditAccountCmd
        {
            get
            {
                return this.editAccountCmd
                  ?? (this.editAccountCmd = new RelayCommand(this.EditAccountAction));
            }

            set
            {
                if (value != this.editAccountCmd)
                    this.Set(ref this.editAccountCmd, value);
            }
        }



        public RelayCommand LogoffCmd
        {
            get
            {
                return this.logoffCmd
                  ?? (this.logoffCmd = new RelayCommand(this.LogoffAction));
            }

            set
            {
                if (value != this.logoffCmd)
                    this.Set(ref this.logoffCmd, value);
            }
        }

        private void TabChangedAction(string tabName)
        {
            switch (tabName)
            {
                case "acceuilTi":
                    this.WindowTitleLabel = "Tableau de bord des formulaires";
                    break;
                case "archivesTi":
                    this.WindowTitleLabel = "Archives des formulaires";
                    break;
                case "mentionsTi":
                    this.WindowTitleLabel = "Mentions";
                    break;
                case "confirmationTi":
                    this.WindowTitleLabel = "Confirmation";
                    break;
                case "clotureTi":
                    this.WindowTitleLabel = "Clôture";
                    break;
                default:
                    break;
            }
        }

        private void EditAccountAction()
        {
            var newWindow = new EditAccountWindow();
            newWindow.ShowDialog();
        }

        private void LogoffAction()
        {
            userService.Logoff();
            /// TODO: clean the user forms services and all

            var currentWindow = App.Current.Windows[0];
            var newWindow = new LoginWindow();
            currentWindow.Close();
            newWindow.Show();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}