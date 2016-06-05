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

namespace AdminWPFClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService userService;
        private string windowTitleLabel = "Tableau de bord des formulaires";
        private string accountLabel = string.Empty;
        private RelayCommand helpCmd = null;
        private RelayCommand<string> tabSelectionChangedCmd = null;

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
                this.Set(ref this.accountLabel, value);
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
                this.Set(ref this.helpCmd, value);
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
                this.Set(ref this.tabSelectionChangedCmd, value);
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

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}