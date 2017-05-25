// <copyright file="IUserService.cs" company="GosselCorp" author="gossel_c">
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

        string GetMentionsText();

        bool SetMentionsText(string text);

        string GetConfirmationText();

        bool SetConfirmationText(string text);

        string GetClotureText();

        bool SetClotureText(string text);

        string GetMentionsFile();

        bool SaveMentionsFile(string filePath);

        bool DeleteMentionsFile();

        void Logoff();
    }
}
