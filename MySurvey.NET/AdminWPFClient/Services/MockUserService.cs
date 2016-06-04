﻿// <copyright file="MockUserService.cs" company="GosselCorp" author="gossel_c">
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

        public bool Authenticate(string userName, SecureString securePassword)
        {
            this.currentLoggedUserName = userName;
            return true;
        }

        public string GetLoggedUsername()
        {
            if (string.IsNullOrWhiteSpace(this.currentLoggedUserName))
                throw new InvalidOperationException("not logged yet");
            return this.currentLoggedUserName;
        }
    }
}
