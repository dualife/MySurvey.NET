// <copyright file="MockUserService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Extensions;
using System;
using System.Security;

namespace AdminWPFClient.Services
{
    public class MockUserService : IUserService
    {
        private string _currentLoggedUserName = string.Empty;
        private string _mentionsText = string.Empty;
        private string _confirmationText = string.Empty;
        private string _clotureText = string.Empty;
        private string _mentionsFile = string.Empty;

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

        public string GetMentionsText()
        {
            return this._mentionsText;
        }

        public bool SetMentionsText(string text)
        {
            this._mentionsText = text;
            return true;
        }

        public string GetConfirmationText()
        {
            return this._confirmationText;
        }

        public bool SetConfirmationText(string text)
        {
            this._confirmationText = text;
            return true;
        }

        public string GetClotureText()
        {
            return this._clotureText;
        }

        public bool SetClotureText(string text)
        {
            this._clotureText = text;
            return true;
        }

        public string GetMentionsFile()
        {
            return this._mentionsFile;
        }

        public bool SaveMentionsFile(string filePath)
        {
            this._mentionsFile = filePath;
            return true;
        }

        public bool DeleteMentionsFile()
        {
            this._mentionsFile = string.Empty;
            return true;
        }

        public void Logoff()
        {
            _currentLoggedUserName = string.Empty;
            _mentionsText = string.Empty;
            _confirmationText = string.Empty;
            _clotureText = string.Empty;
            _mentionsFile = string.Empty;
        }

        public bool SetNewPassword(string oldPassword, string newPassword, ref string errorMessage)
        {
            errorMessage = "Succès!";

            return true;
        }
    }
}
