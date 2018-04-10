// <copyright file="AcceuilFormsViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Models;
using AdminWPFClient.Services;
using AdminWPFClient.Utils;
using AdminWPFClient.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class AcceuilFormsViewModel : ViewModelBase
    {
        private readonly IFormsManagementService _formsService = null;
        private readonly IUserService _userService = null;
        private ItemsChangeObservableCollection<SelectableForm> _formsList = null;
        private string _context = string.Empty;
        private RelayCommand<string> _loadedCmd = null;
        private RelayCommand<SelectableForm> _goToFormUrlCmd = null;
        private RelayCommand<SelectableForm> _copyFormUrlCmd = null;
        private RelayCommand<SelectableForm> _editFormCmd = null;
        private RelayCommand<SelectableForm> _publishFormCmd = null;
        private RelayCommand<SelectableForm> _endFormCmd = null;
        private RelayCommand<SelectableForm> _duplicateFormCmd = null;
        private RelayCommand<SelectableForm> _archiveFormCmd = null;
        private RelayCommand<SelectableForm> _deleteFormCmd = null;
        private RelayCommand _createFormCmd = null;
        private RelayCommand _archiveFormsCmd = null;
        private RelayCommand _deleteFormsCmd = null;
        private RelayCommand _updateFormsSelectedCmd = null;

        public AcceuilFormsViewModel(IUserService userService, IFormsManagementService formsService)
        {
            this._userService = userService;
            this._formsService = formsService;
        }

        public ItemsChangeObservableCollection<SelectableForm> FormsList
        {
            get => this._formsList;

            set
            {
                if (value != this._formsList)
                {
                    if (this._formsList != null)
                        this._formsList.CollectionChanged -= FormsList_CollectionChanged;
                    value.CollectionChanged += FormsList_CollectionChanged;
                    this.Set(ref this._formsList, value);
                }
            }
        }

        public RelayCommand<string> LoadedCmd
        {
            get => this._loadedCmd ?? (this._loadedCmd = new RelayCommand<string>(
                       this.LoadedAction));

            set
            {
                if (value != this._loadedCmd)
                    this.Set(ref this._loadedCmd, value);
            }
        }

        public RelayCommand<SelectableForm> GoToFormUrlCmd
        {
            get => this._goToFormUrlCmd ?? (this._goToFormUrlCmd = new RelayCommand<SelectableForm>(
                       GoToFormUrlAction));

            set
            {
                if (value != this._goToFormUrlCmd)
                    this.Set(ref this._goToFormUrlCmd, value);
            }
        }

        public RelayCommand<SelectableForm> CopyFormUrlCmd
        {
            get => this._copyFormUrlCmd ?? (this._copyFormUrlCmd = new RelayCommand<SelectableForm>(
                       CopyFormUrlAction));

            set
            {
                if (value != this._copyFormUrlCmd)
                    this.Set(ref this._copyFormUrlCmd, value);
            }
        }

        public RelayCommand<SelectableForm> EditFormCmd
        {
            get => this._editFormCmd ?? (this._editFormCmd = new RelayCommand<SelectableForm>(
                       this.EditFormAction));

            set
            {
                if (value != this._editFormCmd)
                    this.Set(ref this._editFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> PublishFormCmd
        {
            get => this._publishFormCmd ?? (this._publishFormCmd = new RelayCommand<SelectableForm>(
                       this.PublishFormAction));

            set
            {
                if (value != this._publishFormCmd)
                    this.Set(ref this._publishFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> EndFormCmd
        {
            get => this._endFormCmd ?? (this._endFormCmd = new RelayCommand<SelectableForm>(
                       this.EndFormAction));

            set
            {
                if (value != this._endFormCmd)
                    this.Set(ref this._endFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> DuplicateFormCmd
        {
            get => this._duplicateFormCmd ?? (this._duplicateFormCmd = new RelayCommand<SelectableForm>(
                       this.DuplicateFormAction));

            set
            {
                if (value != this._duplicateFormCmd)
                    this.Set(ref this._duplicateFormCmd, value);
            }
        }

        public RelayCommand<SelectableForm> ArchiveFormCmd
        {
            get => this._archiveFormCmd ?? (this._archiveFormCmd = new RelayCommand<SelectableForm>(
                       this.ArchiveFormAction));

            set
            {
                if (value != this._archiveFormCmd)
                    this.Set(ref this._archiveFormCmd, value);
            }
        }


        public RelayCommand<SelectableForm> DeleteFormCmd
        {
            get => this._deleteFormCmd ?? (this._deleteFormCmd = new RelayCommand<SelectableForm>(
                       this.DeleteFormAction));

            set
            {
                if (value != this._deleteFormCmd)
                    this.Set(ref this._deleteFormCmd, value);
            }
        }

        public RelayCommand CreateFormCmd
        {
            get => this._createFormCmd ?? (this._createFormCmd = new RelayCommand(
                       () =>
                       {
                           var newForm = this._formsService.CreateForm(_userService);
                           this.FormsList?.Add(new SelectableForm(newForm));
                       }));

            set
            {
                if (value != this._createFormCmd)
                    this.Set(ref this._createFormCmd, value);
            }
        }

        public RelayCommand ArchiveFormsCmd
        {
            get
            {
                return this._archiveFormsCmd ?? (this._archiveFormsCmd = new RelayCommand(
                    () =>
                    {
                        for (var i = this.FormsList.Count - 1; i >= 0; i--)
                        {
                            if (this.FormsList[i].IsSelected)
                            {
                                this.ArchiveFormAction(this.FormsList[i]);
                            }
                        }
                    },
                    () =>
                    {
                        return this.FormsList?.Any(item => item.IsSelected) ?? false;
                    }));
            }

            set
            {
                if (value != this._archiveFormsCmd)
                    this.Set(ref this._archiveFormsCmd, value);
            }
        }

        public RelayCommand DeleteFormsCmd
        {
            get
            {
                return this._deleteFormsCmd ?? (this._deleteFormsCmd = new RelayCommand(
                    () =>
                    {
                        for (var i = this.FormsList.Count - 1; i >= 0; i--)
                        {
                            if (this.FormsList[i].IsSelected)
                            {
                                this.DeleteFormAction(this.FormsList[i]);
                            }
                        }
                    },
                    () =>
                    {
                        return this.FormsList?.Any(item => item.IsSelected) ?? false;
                    }));
            }

            set
            {
                if (value != this._deleteFormsCmd)
                    this.Set(ref this._deleteFormsCmd, value);
            }
        }

        public RelayCommand UpdateFormsSelectedCmd
        {
            get
            {
                return this._updateFormsSelectedCmd ?? (this._updateFormsSelectedCmd = new RelayCommand(
                    () =>
                    {
                        this.RaisePropertyChanged(() => this.IsAllFormsSelected);
                    }));
            }

            set
            {
                if (value != this._updateFormsSelectedCmd)
                    this.Set(ref this._updateFormsSelectedCmd, value);
            }
        }

        public bool IsAllFormsSelected
        {
            get
            {
                if (this.FormsList?.Count == 0) return false;
                return this.FormsList?.All(form => form.IsSelected) ?? false;
            }

            set
            {
                this.FormsList?.ToList().ForEach(form => form.IsSelected = value);
                this.RaisePropertyChanged(() => this.FormsList);
            }
        }

        private void FormsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this._archiveFormsCmd?.RaiseCanExecuteChanged();
            this._deleteFormsCmd?.RaiseCanExecuteChanged();
            this.RaisePropertyChanged(() => this.IsAllFormsSelected);
        }

        private async void LoadedAction(string uclName)
        {
            // OnLoad
            //this._context = uclName;
            //switch (this._context)
            //{
            //    case "AcceuilUc":
            //        this.FormsList = new ItemsChangeObservableCollection<SelectableForm>(
            //            this._formsService.GetCurrentFormsList().Select(item => new SelectableForm(item)));
            //        break;
            //    case "ArchiveUc":
            //        this.FormsList = new ItemsChangeObservableCollection<SelectableForm>(
            //            this._formsService.GetArchivedFormsList().Select(item => new SelectableForm(item)));
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
            var allForms = await this._formsService.GetCurrentFormsListAsync(this._userService.GetLoggedUsername());
            this.FormsList = new ItemsChangeObservableCollection<SelectableForm>(
                allForms.Select(item => new SelectableForm(item)));
            this.FormsList_CollectionChanged(null, null);
        }

        private static void GoToFormUrlAction(SelectableForm selectedForm)
        {
            selectedForm.Form.GoToUrl();
        }

        private static void CopyFormUrlAction(SelectableForm selectedForm)
        {
            selectedForm.Form.CopyUrlToClipboard();

            MessageBox.Show("Copié dans le presse-papier");
        }

        private void EditFormAction(SelectableForm selectedForm)
        {
            var newWindow = new EditFormWindow();
            newWindow.Show();
        }

        private void PublishFormAction(SelectableForm selectedForm)
        {
            this._formsService.PublishForm(selectedForm.Form);
            selectedForm.RaisePropertyChanged();
        }

        private void EndFormAction(SelectableForm selectedForm)
        {
            this._formsService.EndForm(selectedForm.Form);
            selectedForm.RaisePropertyChanged();
        }

        private void DuplicateFormAction(SelectableForm selectedForm)
        {
            var newForm = this._formsService.DuplicateForm(selectedForm.Form);
            this.FormsList?.Add(new SelectableForm(newForm));
        }

        private void ArchiveFormAction(SelectableForm selectedForm)
        {
            this._formsService.ArchiveForm(selectedForm.Form);
            this.FormsList?.Remove(selectedForm);
        }

        private void DeleteFormAction(SelectableForm selectedForm)
        {
            this._formsService.DeleteForm(selectedForm.Form);
            this.FormsList?.Remove(selectedForm);
        }
    }
}