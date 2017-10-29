// <copyright file="FormsManagementViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Models;
using AdminWPFClient.Services;
using AdminWPFClient.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class FormsManagementViewModel : ViewModelBase
    {
        private IFormsManagementService formsService = null;
        private IUserService userService = null;
        private ItemsChangeObservableCollection<SelectableForm> formsList = null;
        private string context = string.Empty;
        private RelayCommand<string> loadedCmd = null;
        private RelayCommand<SelectableForm> goToFormUrlCmd = null;
        private RelayCommand<SelectableForm> copyFormUrlCmd = null;
        private RelayCommand<SelectableForm> editFormCmd = null;
        private RelayCommand<SelectableForm> deployFormCmd = null;
        private RelayCommand<SelectableForm> endFormCmd = null;
        private RelayCommand<SelectableForm> duplicateFormCmd = null;
        private RelayCommand<SelectableForm> archiveFormCmd = null;
        private RelayCommand<SelectableForm> deleteFormCmd = null;
        private RelayCommand createFormCmd = null;
        private RelayCommand archiveFormsCmd = null;
        private RelayCommand deleteFormsCmd = null;
        private RelayCommand updateFormsSelectedCmd = null;

        public FormsManagementViewModel(IUserService userService, IFormsManagementService formsService)
        {
            this.userService = userService;
            this.formsService = formsService;
            this.formsList = new ItemsChangeObservableCollection<SelectableForm>();
            this.formsList.CollectionChanged += FormsList_CollectionChanged;
        }

        public ItemsChangeObservableCollection<SelectableForm> FormsList
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

        public RelayCommand<SelectableForm> GoToFormUrlCmd
        {
            get
            {
                return this.goToFormUrlCmd ?? (this.goToFormUrlCmd = new RelayCommand<SelectableForm>(
                    this.GoToFormUrlAction));
            }

            set
            {
                if (value != this.goToFormUrlCmd)
                    this.Set(ref this.goToFormUrlCmd, value);
            }
        }

        public RelayCommand<SelectableForm> CopyFormUrlCmd
        {
            get
            {
                return this.copyFormUrlCmd ?? (this.copyFormUrlCmd = new RelayCommand<SelectableForm>(
                    this.CopyFormUrlAction));
            }

            set
            {
                if (value != this.copyFormUrlCmd)
                    this.Set(ref this.copyFormUrlCmd, value);
            }
        }

        public RelayCommand<SelectableForm> EditFormCmd
        {
            get
            {
                return this.editFormCmd ?? (this.editFormCmd = new RelayCommand<SelectableForm>(
                    this.EditFormAction));
            }

            set
            {
                if (value != this.editFormCmd)
                    this.Set(ref this.editFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> DeployFormCmd
        {
            get
            {
                return this.deployFormCmd ?? (this.deployFormCmd = new RelayCommand<SelectableForm>(
                    this.DeployFormAction));
            }

            set
            {
                if (value != this.deployFormCmd)
                    this.Set(ref this.deployFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> EndFormCmd
        {
            get
            {
                return this.endFormCmd ?? (this.endFormCmd = new RelayCommand<SelectableForm>(
                    this.EndFormAction));
            }

            set
            {
                if (value != this.endFormCmd)
                    this.Set(ref this.endFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> DuplicateFormCmd
        {
            get
            {
                return this.duplicateFormCmd ?? (this.duplicateFormCmd = new RelayCommand<SelectableForm>(
                    this.DuplicateFormAction));
            }

            set
            {
                if (value != this.duplicateFormCmd)
                    this.Set(ref this.duplicateFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> ArchiveFormCmd
        {
            get
            {
                return this.archiveFormCmd ?? (this.archiveFormCmd = new RelayCommand<SelectableForm>(
                    this.ArchiveFormAction));
            }

            set
            {
                if (value != this.archiveFormCmd)
                    this.Set(ref this.archiveFormCmd, value);
            }
        }


        public RelayCommand<SelectableForm> DeleteFormCmd
        {
            get
            {
                return this.deleteFormCmd ?? (this.deleteFormCmd = new RelayCommand<SelectableForm>(
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
                        for (int i = this.formsList.Count - 1; i >= 0; i--)
                        {
                            if (this.formsList[i].IsSelected)
                            {
                                this.DeleteFormAction(this.formsList[i]);
                            }
                        }
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

        private void FormsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.archiveFormsCmd != null)
                this.archiveFormsCmd.RaiseCanExecuteChanged();
            if (this.deleteFormsCmd != null)
                this.deleteFormsCmd.RaiseCanExecuteChanged();
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

        private void GoToFormUrlAction(SelectableForm selectedForm)
        {
            selectedForm.Form.GoToUrl();
        }

        private void CopyFormUrlAction(SelectableForm selectedForm)
        {
            selectedForm.Form.CopyUrlToClipboard();

            MessageBox.Show("Copié dans le presse-papier");
        }

        private void EditFormAction(SelectableForm selectedForm)
        {
            MessageBox.Show("EditFormAction");
        }

        private void DeployFormAction(SelectableForm selectedForm)
        {
            MessageBox.Show("DeployFormAction");
        }

        private void EndFormAction(SelectableForm selectedForm)
        {
            MessageBox.Show("EndFormAction");
        }

        private void DuplicateFormAction(SelectableForm selectedForm)
        {
            MessageBox.Show("DuplicateFormAction");
        }

        private void ArchiveFormAction(SelectableForm selectedForm)
        {
            MessageBox.Show("ArchiveFormAction");
        }

        private void DeleteFormAction(SelectableForm selectedForm)
        {
            this.FormsList.Remove(selectedForm);
            this.formsService.DeleteForm(selectedForm.Form);
        }
    }
}