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
using System;
using System.Linq;

namespace AdminWPFClient.ViewModels
{
    public class EditAccountViewModel : ViewModelBase
    {
        private IUserService userService = null;
        private string loginField = "admin";
        private RelayCommand saveCmd = null;
        private RelayCommand exitCmd = null;

        public EditAccountViewModel(IUserService userService)
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

        public RelayCommand SaveCmd
        {
            get
            {
                return this.saveCmd ?? new RelayCommand(this.SaveAction);
            }

            set
            {
                this.Set(ref this.saveCmd, value);
            }
        }

        public RelayCommand ExitCmd
        {
            get
            {
                return this.exitCmd ?? new RelayCommand(this.ExitAction);
            }

            set
            {
                this.Set(ref this.exitCmd, value);
            }
        }

        private void SaveAction()
        {
            string errorMessage = string.Empty;

            if (this.userService.SetNewPassword("oldPwd", "newwPwd", ref errorMessage))
            {
                MessageBox.Show("Sauvegardé !");
            }
            else
            {
                MessageBox.Show("Modification non enregistrée !" +
                    Environment.NewLine + errorMessage);
            }

        }

        private void ExitAction()
        {
            EditAccountWindow currentWindow = App.Current.Windows.OfType<EditAccountWindow>().Single();
            currentWindow.Close();
        }
    }
}