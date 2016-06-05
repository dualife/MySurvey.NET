// <copyright file="TextTabViewModel.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AdminWPFClient.ViewModels
{
    public class TextTabViewModel : ViewModelBase
    {
        private IUserService userService = null;
        private string tabTitleLabel = string.Empty;
        private string fileUploadLabel = string.Empty;
        private string textContent = string.Empty;
        private bool showUploadBar = false;
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
                this.Set(ref this.showUploadBar, value);
            }
        }

        public RelayCommand<string> LoadedCmd
        {
            get
            {
                return this.loadedCmd ?? (this.loadedCmd = new RelayCommand<string>(
                    loadedAction));
            }

            set
            {
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
                    }));
            }

            set
            {
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
                    }));
            }

            set
            {
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
                    }));
            }

            set
            {
                this.Set(ref this.loadFileCmd, value);
            }
        }

        private void loadedAction(string ucName)
        {
            // OnLoad
            switch (ucName)
            {
                case "mentionUc":
                    this.TabTitleLabel = "Mentions légales appliquées à chaque formulaire";
                    this.ShowUploadBar = true;
                    this.TextContent = this.userService.GetMentionsText();
                    break;
                case "confirmationUc":
                    this.TabTitleLabel = "Message de confirmation appliqué à chaque formulaire";
                    this.ShowUploadBar = false;
                    this.TextContent = this.userService.GetConfirmationText();
                    break;
                case "clotureUc":
                    this.TabTitleLabel = "Message affiché une fois que le formulaire est clôturé.";
                    this.ShowUploadBar = false;
                    this.TextContent = this.userService.GetClotureText();
                    break;
            }
        }
    }
}