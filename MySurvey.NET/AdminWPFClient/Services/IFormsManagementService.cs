// <copyright file="IFormsManagementService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Models;
using System.Collections.Generic;

namespace AdminWPFClient.Services
{
    public interface IFormsManagementService
    {
        Form CreateForm(IUserService userService);

        bool DeleteForm(Form form);

        IEnumerable<Form> GetCurrentFormsList();

        IEnumerable<Form> GetArchivedFormsList();

        bool ArchiveForm(Form form);

        Form DuplicateForm(Form original);

        bool EndForm(Form form);

        bool PublishForm(Form form);
    }
}
