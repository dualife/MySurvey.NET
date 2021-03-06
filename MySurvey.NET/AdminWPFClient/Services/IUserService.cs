﻿// <copyright file="IUserService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System.Security;

namespace AdminWPFClient.Services
{
    public interface IUserService
    {
        bool Authenticate(string userName, SecureString securePassword);

        string GetLoggedUsername();

        string GetDefaultMentionsText();

        bool SetDefaultMentionsText(string text);

        string GetDefaultConfirmationText();

        bool SetDefaultConfirmationText(string text);

        string GetDefaultEndingText();

        bool SetDefaultEndingText(string text);

        string GetDefaultMentionsFile();

        bool SaveDefaultMentionsFile(string filePath);

        bool DeleteDefaultMentionsFile();

        void Logoff();

        bool SetNewPassword(string oldPassword, string newPassword, ref string errorMessage);
    }
}
