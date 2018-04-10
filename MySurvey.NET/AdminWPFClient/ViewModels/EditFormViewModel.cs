// <copyright file="EditFormViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using System.Windows;
using AdminWPFClient.Models;
using AdminWPFClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AdminWPFClient.ViewModels
{
    public class EditFormViewModel : ViewModelBase
    {
        private readonly IFormsManagementService _formsService = null;
        private readonly IUserService _userService = null;
        private Form _currentForm = null;
        private RelayCommand _helpCmd = null;

        public EditFormViewModel(IUserService userService, IFormsManagementService formsService)
        {
            this._userService = userService;
            this._formsService = formsService;
        }

        public RelayCommand HelpCmd
        {
            get => this._helpCmd ?? (this._helpCmd = new RelayCommand(
                       this.HelpAction));

            set
            {
                if (value != this._helpCmd)
                    this.Set(ref this._helpCmd, value);
            }
        }

        private void HelpAction()
        {
            MessageBox.Show("Help!");
        }
    }
}