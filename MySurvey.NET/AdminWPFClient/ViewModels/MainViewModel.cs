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

namespace AdminWPFClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserService userService;
        private string windowTitleLabel = string.Empty;
        private string accountLabel = string.Empty;
        private RelayCommand<string> tabChangedCmd = null;
        private RelayCommand helpCmd = null;

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

        public RelayCommand<string> TabChangedCmd
        {
            get
            {
                return this.tabChangedCmd
                  ?? (this.tabChangedCmd = new RelayCommand<string>(
                    this.TabChangedAction));
            }

            set
            {
                this.Set(ref this.tabChangedCmd, value);
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
                          throw new NotImplementedException();
                      }
                    ));
            }

            set
            {
                this.Set(ref this.helpCmd, value);
            }
        }

        private void TabChangedAction(string obj)
        {
            throw new NotImplementedException();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}