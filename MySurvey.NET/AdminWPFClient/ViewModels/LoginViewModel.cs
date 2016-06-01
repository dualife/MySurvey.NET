// <copyright file="LoginViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Services;
using AdminWPFClient.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Security;

namespace AdminWPFClient.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private IUserService userService = null;
        private string loginField = "admin";
        private RelayCommand<SecureString> connectCmd = null;

        /// <summary>
        /// Initializes a new instance of the LoginViewModel class.
        /// </summary>
        public LoginViewModel(IUserService userService)
        {
            this.userService = userService;
        }

        public string LoginField
        {
            get
            {
                return this.loginField;
            }

            set
            {
                this.Set(ref this.loginField, value);
            }
        }

        public RelayCommand<SecureString> ConnectCmd
        {
            get
            {
                return this.connectCmd ?? new RelayCommand<SecureString>(
                    securePassword =>
                    {
                        /// TODO: authentification and move to MainWindow
                        if (!this.userService.Authenticate(this.LoginField, securePassword))
                        {
                            return;
                        }

                        var currentWindow = App.Current.Windows[0];
                        var newWindow = new MainWindow();
                        currentWindow.Close();
                        newWindow.Show();
                    });
            }

            set
            {
                this.Set(ref this.connectCmd, value);
            }
        }
    }
}