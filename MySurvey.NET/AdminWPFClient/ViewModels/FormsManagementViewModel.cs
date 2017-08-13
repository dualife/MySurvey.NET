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
using System.Linq;
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class FormsManagementViewModel : ViewModelBase
    {
        private IFormsManagementService formsService = null;
        private IUserService userService = null;
        private ObservableCollection<SelectableForm> formsList = null;
        private string context = string.Empty;
        private RelayCommand<string> loadedCmd = null;
        private RelayCommand<Form> goToFormUrlCmd = null;
        private RelayCommand<Form> copyFormUrlCmd = null;
        private RelayCommand<Form> editFormCmd = null;
        private RelayCommand<Form> deployFormCmd = null;
        private RelayCommand<Form> endFormCmd = null;
        private RelayCommand<Form> duplicateFormCmd = null;
        private RelayCommand<Form> archiveFormCmd = null;
        private RelayCommand<Form> deleteFormCmd = null;
        private RelayCommand createFormCmd = null;
        private RelayCommand archiveFormsCmd = null;
        private RelayCommand deleteFormsCmd = null;
        private RelayCommand updateFormsSelectedCmd = null;

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
                if (value != this.formsList)
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
                if (value != this.loadedCmd)
                    this.Set(ref this.loadedCmd, value);
            }
        }

        public RelayCommand<Form> GoToFormUrlCmd
        {
            get
            {
                return this.goToFormUrlCmd ?? (this.goToFormUrlCmd = new RelayCommand<Form>(
                    this.GoToFormUrlAction));
            }

            set
            {
                if (value != this.goToFormUrlCmd)
                    this.Set(ref this.goToFormUrlCmd, value);
            }
        }

        public RelayCommand<Form> CopyFormUrlCmd
        {
            get
            {
                return this.copyFormUrlCmd ?? (this.copyFormUrlCmd = new RelayCommand<Form>(
                    this.CopyFormUrlAction));
            }

            set
            {
                if (value != this.copyFormUrlCmd)
                    this.Set(ref this.copyFormUrlCmd, value);
            }
        }

        public RelayCommand<Form> EditFormCmd
        {
            get
            {
                return this.editFormCmd ?? (this.editFormCmd = new RelayCommand<Form>(
                    this.EditFormAction));
            }

            set
            {
                if (value != this.editFormCmd)
                    this.Set(ref this.editFormCmd, value);
            }
        }

        public RelayCommand<Form> DeployFormCmd
        {
            get
            {
                return this.deployFormCmd ?? (this.deployFormCmd = new RelayCommand<Form>(
                    this.DeployFormAction));
            }

            set
            {
                if (value != this.deployFormCmd)
                    this.Set(ref this.deployFormCmd, value);
            }
        }

        public RelayCommand<Form> EndFormCmd
        {
            get
            {
                return this.endFormCmd ?? (this.endFormCmd = new RelayCommand<Form>(
                    this.EndFormAction));
            }

            set
            {
                if (value != this.endFormCmd)
                    this.Set(ref this.endFormCmd, value);
            }
        }

        public RelayCommand<Form> DuplicateFormCmd
        {
            get
            {
                return this.duplicateFormCmd ?? (this.duplicateFormCmd = new RelayCommand<Form>(
                    this.DuplicateFormAction));
            }

            set
            {
                if (value != this.duplicateFormCmd)
                    this.Set(ref this.duplicateFormCmd, value);
            }
        }

        public RelayCommand<Form> ArchiveFormCmd
        {
            get
            {
                return this.archiveFormCmd ?? (this.archiveFormCmd = new RelayCommand<Form>(
                    this.ArchiveFormAction));
            }

            set
            {
                if (value != this.archiveFormCmd)
                    this.Set(ref this.archiveFormCmd, value);
            }
        }


        public RelayCommand<Form> DeleteFormCmd
        {
            get
            {
                return this.deleteFormCmd ?? (this.deleteFormCmd = new RelayCommand<Form>(
                    this.DeleteFormAction));
            }

            set
            {
                if (value != this.deleteFormCmd)
                    this.Set(ref this.deleteFormCmd, value);
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
                if (value != this.createFormCmd)
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
                    },
                    () =>
                    {
                        return this.formsList.Any(item => item.IsSelected);
                    }));
            }

            set
            {
                if (value != this.archiveFormsCmd)
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
                    },
                    () =>
                    {
                        return this.formsList.Any(item => item.IsSelected);
                    }));
            }

            set
            {
                if (value != this.deleteFormsCmd)
                    this.Set(ref this.deleteFormsCmd, value);
            }
        }

        public RelayCommand UpdateFormsSelectedCmd
        {
            get
            {
                return this.updateFormsSelectedCmd ?? (this.updateFormsSelectedCmd = new RelayCommand(
                    () =>
                    {
                        this.RaisePropertyChanged(() => this.IsAllFormsSelected);
                    }));
            }

            set
            {
                if (value != this.updateFormsSelectedCmd)
                    this.Set(ref this.updateFormsSelectedCmd, value);
            }
        }

        public bool IsAllFormsSelected
        {
            get
            {
                if (this.formsList.Count == 0)
                {
                    return false;
                }

                return this.formsList.All(form => form.IsSelected);
            }

            set
            {
                this.formsList.ToList().ForEach(form => form.IsSelected = value);
                this.RaisePropertyChanged();
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

        private void GoToFormUrlAction(Form form)
        {
            form.GoToUrl();
        }

        private void CopyFormUrlAction(Form form)
        {
            form.CopyUrlToClipboard();

            MessageBox.Show("Copié dans le presse-papier");
        }

        private void EditFormAction(Form obj)
        {
            MessageBox.Show("EditFormAction");
        }

        private void DeployFormAction(Form obj)
        {
            MessageBox.Show("DeployFormAction");
        }

        private void EndFormAction(Form obj)
        {
            MessageBox.Show("EndFormAction");
        }

        private void DuplicateFormAction(Form obj)
        {
            MessageBox.Show("DuplicateFormAction");
        }

        private void ArchiveFormAction(Form obj)
        {
            MessageBox.Show("ArchiveFormAction");
        }

        private void DeleteFormAction(Form obj)
        {
            MessageBox.Show("DeleteFormAction");
        }
    }
}