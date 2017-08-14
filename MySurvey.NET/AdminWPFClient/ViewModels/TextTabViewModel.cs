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
        private IUserService userService = null;
        private string context = string.Empty;
        private string tabTitleLabel = string.Empty;
        private string fileUploadLabel = string.Empty;
        private string textContent = string.Empty;
        private bool showUploadBar = false;
        private string filePath = string.Empty;
        private RelayCommand<string> loadedCmd = null;
        private RelayCommand saveCmd = null;
        private RelayCommand deleteFileCmd = null;
        private RelayCommand loadFileCmd = null;

        public TextTabViewModel(IUserService userService)
        {
            this.userService = userService;
        }

        public string TabTitleLabel
        {
            get
            {
                return this.tabTitleLabel;
            }

            set
            {
                if (value != this.tabTitleLabel)
                    this.Set(ref this.tabTitleLabel, value);
            }
        }

        public string FileUploadLabel
        {
            get
            {
                return this.fileUploadLabel;
            }

            set
            {
                if (value != this.fileUploadLabel)
                    this.Set(ref this.fileUploadLabel, value);
            }
        }

        public string TextContent
        {
            get
            {
                return this.textContent;
            }

            set
            {
                if (value != this.textContent)
                    this.Set(ref this.textContent, value);
            }
        }

        public bool ShowUploadBar
        {
            get
            {
                return this.showUploadBar;
            }

            set
            {
                if (value != this.showUploadBar)
                    this.Set(ref this.showUploadBar, value);
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

        public RelayCommand SaveCmd
        {
            get
            {
                return this.saveCmd ?? (this.saveCmd = new RelayCommand(
                    () =>
                    {
                        // saveBtn
                        switch (this.context)
                        {
                            case "mentionUc":
                                this.userService.SetMentionsText(this.TextContent);
                                break;
                            case "confirmationUc":
                                this.userService.SetConfirmationText(this.TextContent);
                                break;
                            case "clotureUc":
                                this.userService.SetClotureText(this.TextContent);
                                break;
                        }

                        if (!string.IsNullOrWhiteSpace(this.FilePath))
                        {
                            this.userService.SaveMentionsFile(this.FilePath);
                            this.FileUploadLabel = "Fichier chargé : " + Path.GetFileName(this.FilePath);
                            this.DeleteFileCmd.RaiseCanExecuteChanged();
                        }

                        MessageBox.Show("Sauvegardé!");
                    }));
            }

            set
            {
                if (value != this.saveCmd)
                    this.Set(ref this.saveCmd, value);
            }
        }

        public RelayCommand DeleteFileCmd
        {
            get
            {
                return this.deleteFileCmd ?? (this.deleteFileCmd = new RelayCommand(
                    () =>
                    {
                        // deleteBtn
                        this.userService.DeleteMentionsFile();
                        this.FileUploadLabel = "Fichié chargé : Aucun.";
                        this.FilePath = string.Empty;
                        this.DeleteFileCmd.RaiseCanExecuteChanged();
                    },
                    () =>
                    {
                        // CanExecute
                        if (string.IsNullOrWhiteSpace(this.userService.GetMentionsFile()))
                        {
                            return false;
                        }

                        return true;
                    }));
            }

            set
            {
                if (value != this.deleteFileCmd)
                    this.Set(ref this.deleteFileCmd, value);
            }
        }

        public RelayCommand LoadFileCmd
        {
            get
            {
                return this.loadFileCmd ?? (this.loadFileCmd = new RelayCommand(
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
                            var justFilename = Path.GetFileName(this.FilePath);
                            this.FileUploadLabel = "Fichier à charger : " + justFilename;
                        }
                    }));
            }

            set
            {
                if (value != this.loadFileCmd)
                    this.Set(ref this.loadFileCmd, value);
            }
        }

        private string FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                if (value != this.filePath)
                    this.Set(ref this.filePath, value);
            }
        }

        private void LoadedAction(string uclName)
        {
            // OnLoad
            this.context = uclName;
            this.ShowUploadBar = false;
            switch (this.context)
            {
                case "mentionUc":
                    this.TabTitleLabel = "Mentions légales appliquées à chaque formulaire";
                    this.ShowUploadBar = true;
                    this.TextContent = this.userService.GetMentionsText();
                    this.FilePath = this.userService.GetMentionsFile();
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
                case "confirmationUc":
                    this.TabTitleLabel = "Message de confirmation appliqué à chaque formulaire";
                    this.TextContent = this.userService.GetConfirmationText();
                    break;
                case "clotureUc":
                    this.TabTitleLabel = "Message affiché une fois que le formulaire est clôturé.";
                    this.TextContent = this.userService.GetClotureText();
                    break;
            }
        }
    }
}