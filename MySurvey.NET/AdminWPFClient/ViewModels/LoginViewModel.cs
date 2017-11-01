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
        private readonly IUserService _userService = null;
        private string _loginField = "admin";
        private RelayCommand<SecureString> _connectCmd = null;

        public LoginViewModel(IUserService userService)
        {
            this._userService = userService;
        }

        public string LoginField
        {
            get => this._loginField;

            set
            {
                if (value != this._loginField)
                    this.Set(ref this._loginField, value);
            }
        }

        public SecureString PasswordHash { get; internal set; }


        public RelayCommand<SecureString> ConnectCmd
        {
            get
            {
                return this._connectCmd ?? new RelayCommand<SecureString>(
                    securePassword =>
                    {
                        var val = this.PasswordHash;

                        // TODO securepassword always empty...
                        if (!this._userService.Authenticate(this.LoginField, securePassword))
                        {
                            MessageBox.Show("Mot de passe invalide!");
                            return;
                        }

                        var currentWindow = Application.Current.Windows[0];
                        var newWindow = new MainWindow();
                        currentWindow?.Close();
                        newWindow.Show();
                    });
            }

            set
            {
                if (value != this._connectCmd)
                    this.Set(ref this._connectCmd, value);
            }
        }

    }
}