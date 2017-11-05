// <copyright file="MockUserService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Extensions;
using System;
using System.IO;
using System.Security;

namespace AdminWPFClient.Services
{
    public class MockUserService : IUserService
    {
        private string _currentLoggedUserName = string.Empty;
        private string _mentionsText = "Default mention text.";
        private string _confirmationText = "Default confirmation text.";
        private string _endingText = "Default ending text.";
        private string _mentionsFilePath = string.Empty;

        public bool Authenticate(string userName, SecureString securePassword)
        {
            // unsecure use of SecureString
            var pwd = securePassword.GetUnsecureString();

            // mock authentification allowing no password or same than username
            if (string.IsNullOrEmpty(pwd) || pwd == userName)
            {
                this._currentLoggedUserName = userName;
                return true;
            }

            return false;
        }

        public string GetLoggedUsername()
        {
            if (string.IsNullOrWhiteSpace(this._currentLoggedUserName))
            {
                throw new InvalidOperationException("not logged yet");
            }

            return this._currentLoggedUserName;
        }

        public string GetDefaultMentionsText()
        {
            return this._mentionsText;
        }

        public bool SetDefaultMentionsText(string text)
        {
            this._mentionsText = text;
            return true;
        }

        public string GetDefaultConfirmationText()
        {
            return this._confirmationText;
        }

        public bool SetDefaultConfirmationText(string text)
        {
            this._confirmationText = text;
            return true;
        }

        public string GetDefaultEndingText()
        {
            return this._endingText;
        }

        public bool SetDefaultEndingText(string text)
        {
            this._endingText = text;
            return true;
        }

        public string GetDefaultMentionsFile()
        {
            return this._mentionsFilePath;
        }

        public bool SaveDefaultMentionsFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            this._mentionsFilePath = filePath;
            return true;
        }

        public bool DeleteDefaultMentionsFile()
        {
            this._mentionsFilePath = string.Empty;
            return true;
        }

        public void Logoff()
        {
            this._currentLoggedUserName = string.Empty;
            this._mentionsText = string.Empty;
            this._confirmationText = string.Empty;
            this._endingText = string.Empty;
            this._mentionsFilePath = string.Empty;
        }

        public bool SetNewPassword(string oldPassword, string newPassword, ref string errorMessage)
        {
            errorMessage = "Succès!";

            return true;
        }
    }
}
