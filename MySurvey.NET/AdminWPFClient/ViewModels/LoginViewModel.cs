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
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IUserService userService = null;
        private string loginField = "admin";
        private RelayCommand<SecureString> connectCmd = null;

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
                        /// TODO securepassword always empty...
                        if (!this.userService.Authenticate(this.LoginField, securePassword))
                        {
                            MessageBox.Show("Mot de passe invalide!");
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