// <copyright file="MockUserService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using System.Security;

namespace AdminWPFClient.Services
{
    public class MockUserService : IUserService
    {
        private string currentLoggedUserName = string.Empty;
        private string mentionsText = string.Empty;
        private string confirmationText = string.Empty;
        private string clotureText = string.Empty;
        private string mentionsFile = string.Empty;

        public bool Authenticate(string userName, SecureString securePassword)
        {
            this.currentLoggedUserName = userName;
            return true;
        }

        public string GetLoggedUsername()
        {
            if (string.IsNullOrWhiteSpace(this.currentLoggedUserName))
            {
                throw new InvalidOperationException("not logged yet");
            }

            return this.currentLoggedUserName;
        }

        public string GetMentionsText()
        {
            return this.mentionsText;
        }

        public bool SetMentionsText(string text)
        {
            this.mentionsText = text;
            return true;
        }

        public string GetConfirmationText()
        {
            return this.confirmationText;
        }

        public bool SetConfirmationText(string text)
        {
            this.confirmationText = text;
            return true;
        }

        public string GetClotureText()
        {
            return this.clotureText;
        }

        public bool SetClotureText(string text)
        {
            this.clotureText = text;
            return true;
        }

        public string GetMentionsFile()
        {
            return this.mentionsFile;
        }

        public bool SaveMentionsFile(string filePath)
        {
            this.mentionsFile = filePath;
            return true;
        }

        public bool DeleteMentionsFile()
        {
            this.mentionsFile = string.Empty;
            return true;
        }
    }
}
