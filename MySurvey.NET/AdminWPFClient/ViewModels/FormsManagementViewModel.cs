// <copyright file="FormsManagementViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Models;
using AdminWPFClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;

namespace AdminWPFClient.ViewModels
{
    public class FormsManagementViewModel : ViewModelBase
    {
        private IFormsManagementService formsService = null;
        private IUserService userService = null;
        private ObservableCollection<SelectableForm> formsList = null;
        private string context = string.Empty;
        private RelayCommand<string> loadedCmd = null;
        private RelayCommand createFormCmd = null;
        private RelayCommand archiveFormsCmd = null;
        private RelayCommand deleteFormsCmd = null;

        public FormsManagementViewModel(IUserService userService, IFormsManagementService formsService)
        {
            this.userService = userService;
            this.formsService = formsService;
            this.formsList = new ObservableCollection<SelectableForm>();
        }

        public ObservableCollection<SelectableForm> FormsList
        {
            get
            {
                return this.formsList;
            }

            set
            {
                this.Set(ref this.formsList, value);
            }
        }

        public RelayCommand<string> LoadedCmd
        {
            get
            {
                return this.loadedCmd ?? (this.loadedCmd = new RelayCommand<string>(
                    this.LoadedAction));
            }

            set
            {
                this.Set(ref this.loadedCmd, value);
            }
        }

        public RelayCommand CreateFormCmd
        {
            get
            {
                return this.createFormCmd ?? (this.createFormCmd = new RelayCommand(
                    () =>
                    {
                        var newForm = this.formsService.CreateForm(userService);
                        this.FormsList.Add(new SelectableForm()
                        {
                            IsSelected = false,
                            Form = newForm
                        });
                    }));
            }

            set
            {
                this.Set(ref this.createFormCmd, value);
            }
        }

        public RelayCommand ArchiveFormsCmd
        {
            get
            {
                return this.archiveFormsCmd ?? (this.archiveFormsCmd = new RelayCommand(
                    () =>
                    {
                        throw new NotImplementedException();
                    }));
            }

            set
            {
                this.Set(ref this.archiveFormsCmd, value);
            }
        }

        public RelayCommand DeleteFormsCmd
        {
            get
            {
                return this.deleteFormsCmd ?? (this.deleteFormsCmd = new RelayCommand(
                    () =>
                    {
                        throw new NotImplementedException();
                    }));
            }

            set
            {
                this.Set(ref this.deleteFormsCmd, value);
            }
        }

        private void LoadedAction(string uclName)
        {
            // OnLoad
            this.context = uclName;
            switch (this.context)
            {
                case "acceuilUc":
                    break;
                case "archiveUc":
                    break;
            }
        }
    }
}