// <copyright file="TextTabViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace AdminWPFClient.ViewModels
{
    public class TextTabViewModel : ViewModelBase
    {
        private readonly IUserService _userService = null;
        private readonly IFormsManagementService _formsService;
        private string _context = string.Empty;
        private string _tabTitleLabel = string.Empty;
        private string _fileUploadLabel = string.Empty;
        private string _textContent = string.Empty;
        private bool _showUploadBar = false;
        private string _filePath = string.Empty;
        private RelayCommand<string> _loadedCmd = null;
        private RelayCommand _saveCmd = null;
        private RelayCommand _deleteFileCmd = null;
        private RelayCommand _loadFileCmd = null;

        public TextTabViewModel(IUserService userService, IFormsManagementService formsService)
        {
            this._userService = userService;
            this._formsService = formsService;
        }

        public string TabTitleLabel
        {
            get => this._tabTitleLabel;

            set
            {
                if (value != this._tabTitleLabel)
                    this.Set(ref this._tabTitleLabel, value);
            }
        }

        public string FileUploadLabel
        {
            get => this._fileUploadLabel;

            set
            {
                if (value != this._fileUploadLabel)
                    this.Set(ref this._fileUploadLabel, value);
            }
        }

        public string TextContent
        {
            get => this._textContent;

            set
            {
                if (value != this._textContent)
                    this.Set(ref this._textContent, value);
            }
        }

        public bool ShowUploadBar
        {
            get => this._showUploadBar;

            set
            {
                if (value != this._showUploadBar)
                    this.Set(ref this._showUploadBar, value);
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

        public RelayCommand SaveCmd
        {
            get
            {
                return this._saveCmd ?? (this._saveCmd = new RelayCommand(
                    () =>
                    {
                        // saveBtn
                        var overwrite = false;
                        if (this._formsService.IsUserHaveAnyForm(this._userService.GetLoggedUsername()))
                        {
                            var res = MessageBox.Show("Overwrite existing forms text?", "Overwrite?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                            switch (res)
                            {
                                case MessageBoxResult.Yes:
                                    overwrite = true;
                                    break;
                                case MessageBoxResult.No:
                                    overwrite = false;
                                    break;
                                default:
                                    return;
                            }
                        }

                        switch (this._context)
                        {
                            case "MentionUc":
                                this._userService.SetDefaultMentionsText(this.TextContent);
                                if (overwrite)
                                    this._formsService.OverwriteMentionsText(this._userService.GetLoggedUsername(), this.TextContent);
                                break;
                            case "ConfirmationUc":
                                this._userService.SetDefaultConfirmationText(this.TextContent);
                                if (overwrite)
                                    this._formsService.OverwriteConfirmationText(this._userService.GetLoggedUsername(), this.TextContent);
                                break;
                            case "EndingUc":
                                this._userService.SetDefaultEndingText(this.TextContent);
                                if (overwrite)
                                    this._formsService.OverwriteEndingText(this._userService.GetLoggedUsername(), this.TextContent);
                                break;
                        }

                        MessageBox.Show("Sauvegardé!");
                    }));
            }

            set
            {
                if (value != this._saveCmd)
                    this.Set(ref this._saveCmd, value);
            }
        }

        public RelayCommand DeleteFileCmd
        {
            get
            {
                return this._deleteFileCmd ?? (this._deleteFileCmd = new RelayCommand(
                    () =>
                    {
                        // deleteBtn
                        this._userService.DeleteDefaultMentionsFile();
                        this.FileUploadLabel = "Fichié chargé : Aucun.";
                        this.FilePath = string.Empty;
                        this.DeleteFileCmd.RaiseCanExecuteChanged();
                    },
                    () => !string.IsNullOrWhiteSpace(this._userService.GetDefaultMentionsFile())
                    ));
            }

            set
            {
                if (value != this._deleteFileCmd)
                    this.Set(ref this._deleteFileCmd, value);
            }
        }

        public RelayCommand LoadFileCmd
        {
            get
            {
                return this._loadFileCmd ?? (this._loadFileCmd = new RelayCommand(
                    () =>
                    {
                        // loadBtn
                        var openFileDialog = new OpenFileDialog()
                        {
                            ValidateNames = true,
                            Filter = "PDF files (*.pdf)|*.pdf",
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            Multiselect = false
                        };

                        if (openFileDialog.ShowDialog() == true)
                        {
                            this.FilePath = openFileDialog.FileName;
                            this._userService.SaveDefaultMentionsFile(this.FilePath);
                            this.FileUploadLabel = "Fichier chargé : " + Path.GetFileName(this.FilePath);
                            this.DeleteFileCmd.RaiseCanExecuteChanged();
                        }

                    }));
            }

            set
            {
                if (value != this._loadFileCmd)
                    this.Set(ref this._loadFileCmd, value);
            }
        }

        private string FilePath
        {
            get => this._filePath;

            set
            {
                if (value != this._filePath)
                    this.Set(ref this._filePath, value);
            }
        }

        private void LoadedAction(string uclName)
        {
            // OnLoad
            this._context = uclName;
            this.ShowUploadBar = false;
            switch (this._context)
            {
                case "MentionUc":
                    this.TabTitleLabel = "Mentions légales appliquées à chaque formulaire";
                    this.ShowUploadBar = true;
                    this.TextContent = this._userService.GetDefaultMentionsText();
                    this.FilePath = this._userService.GetDefaultMentionsFile();
                    if (string.IsNullOrWhiteSpace(this.FilePath))
                    {
                        this.FileUploadLabel = "Fichier chargé : Aucun.";
                    }
                    else
                    {
                        this.FileUploadLabel = "Fichié chargé : " + Path.GetFileName(this.FilePath);
                        this.DeleteFileCmd.RaiseCanExecuteChanged();
                    }

                    break;
                case "ConfirmationUc":
                    this.TabTitleLabel = "Message de confirmation appliqué à chaque formulaire";
                    this.TextContent = this._userService.GetDefaultConfirmationText();
                    break;
                case "EndingUc":
                    this.TabTitleLabel = "Message affiché une fois que le formulaire est clôturé.";
                    this.TextContent = this._userService.GetDefaultEndingText();
                    break;
            }
        }
    }
}