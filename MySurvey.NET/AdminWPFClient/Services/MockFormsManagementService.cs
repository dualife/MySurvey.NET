// <copyright file="MockFormsManagementService.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using AdminWPFClient.Models;

namespace AdminWPFClient.Services
{
    public class MockFormsManagementService : IFormsManagementService
    {
        private List<Form> forms = null;

        public MockFormsManagementService()
        {
            this.forms = new List<Form>();
        }

        public Form CreateForm(IUserService userService)
        {
            var newForm = new Form()
            {
                Id = this.GetNewId(),
                AuthorName = userService.GetLoggedUsername(),
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                NumberOfAnswers = 10,
                Status = Form.State.InProgress,
                Title = "newEmptyForm",
                Url= new Uri("http://i2.kym-cdn.com/entries/icons/facebook/000/001/030/dickbutt.jpg")
            };

            this.forms.Add(newForm);
            return newForm;
        }

        public bool DeleteForm(Form form)
        {
            return this.forms.Remove(form);
        }

        public IEnumerable<Form> GetCurrentFormsList()
        {
            return this.forms.Where(element => element.Status != Form.State.Archived && element.Status != Form.State.Closed);
        }

        public IEnumerable<Form> GetArchivedFormsList()
        {
            return this.forms.Where(element => element.Status == Form.State.Archived);
        }

        private int GetNewId()
        {
            return this.forms.Count + 1;
        }
    }
}
