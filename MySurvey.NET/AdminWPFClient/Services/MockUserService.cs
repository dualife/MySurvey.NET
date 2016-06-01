// <copyright file="MockUserService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AdminWPFClient.Services
{
    public class MockUserService : IUserService
    {
        private string currentLoggedUserName;

        public bool Authenticate(string userName, SecureString securePassword)
        {
            this.currentLoggedUserName = userName;
            return true;
        }
    }
}
