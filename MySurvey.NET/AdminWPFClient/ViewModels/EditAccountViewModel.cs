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
using System;
using System.Linq;
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class EditAccountViewModel : ViewModelBase
    {
        private readonly IUserService _userService = null;
        private string _loginField = "admin";
        private RelayCommand _saveCmd = null;
        private RelayCommand _exitCmd = null;

        public EditAccountViewModel(IUserService userService)
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

        public RelayCommand SaveCmd
        {
            get => this._saveCmd ?? new RelayCommand(this.SaveAction);

            set
            {
                if (value != this._saveCmd)
                    this.Set(ref this._saveCmd, value);
            }
        }

        public RelayCommand ExitCmd
        {
            get => this._exitCmd ?? new RelayCommand(ExitAction);

            set
            {
                if (value != this._exitCmd)
                    this.Set(ref this._exitCmd, value);
            }
        }

        private void SaveAction()
        {
            var errorMessage = string.Empty;

            if (this._userService.SetNewPassword("oldPwd", "newwPwd", ref errorMessage))
            {
                MessageBox.Show("Sauvegardé !");
            }
            else
            {
                MessageBox.Show("Modification non enregistrée !" +
                    Environment.NewLine + errorMessage);
            }

        }

        private static void ExitAction()
        {
            var currentWindow = Application.Current.Windows.OfType<EditAccountWindow>().Single();
            currentWindow.Close();
        }
    }
}