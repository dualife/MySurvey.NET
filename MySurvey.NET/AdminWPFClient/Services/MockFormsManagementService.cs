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
        private readonly List<Form> _forms = null;

        public MockFormsManagementService()
        {
            this._forms = new List<Form>();
        }

        public Form CreateForm(IUserService userService)
        {
            var newForm = new Form(this.GetNewId())
            {
                AuthorName = userService.GetLoggedUsername(),
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                NumberOfAnswers = 10,
                Status = Form.State.InProgress,
                Title = "newEmptyForm",
                Url = new Uri("http://i2.kym-cdn.com/entries/icons/facebook/000/001/030/dickbutt.jpg"),
                MentionsTxt = userService.GetDefaultMentionsText(),
                ConfirmationTxt = userService.GetDefaultConfirmationText(),
                EndingTxt = userService.GetDefaultEndingText(),
                MentionsFilePath = userService.GetDefaultMentionsFile()
            };

            this._forms.Add(newForm);
            return newForm;
        }

        public bool DeleteForm(Form form)
        {
            return this._forms.Remove(form);
        }

        public IEnumerable<Form> GetCurrentFormsList(string author)
        {
            return this._forms.Where(element => element.AuthorName == author && element.Status != Form.State.Archived);
        }

        public IEnumerable<Form> GetArchivedFormsList(string author)
        {
            return this._forms.Where(element => element.AuthorName == author && element.Status == Form.State.Archived);
        }

        private int GetNewId()
        {
            return this._forms.Count + 1;
        }

        public bool ArchiveForm(Form form)
        {
            return form.Archive();
        }

        public Form DuplicateForm(Form original)
        {
            var newForm = new Form(this.GetNewId(), original);
            this._forms.Add(newForm);
            return newForm;
        }

        public bool EndForm(Form form)
        {
            return form.End();
        }

        public bool PublishForm(Form form)
        {
            return form.Publish();
        }

        public bool RestoreForm(Form form)
        {
            return form.Restore();
        }

        public bool IsUserHaveAnyForm(string author)
        {
            return this._forms.Any(element => element.AuthorName == author);
        }

        public void OverwriteMentionsText(string author, string newText)
        {
            var concernedItems = this._forms.Where(i => i.AuthorName == author && i.Status != Form.State.Archived);
            foreach (var form in concernedItems)
            {
                form.MentionsTxt = newText;
            }
        }

        public void OverwriteConfirmationText(string author, string newText)
        {
            var concernedItems = this._forms.Where(i => i.AuthorName == author && i.Status != Form.State.Archived);
            foreach (var form in concernedItems)
            {
                form.ConfirmationTxt = newText;
            }
        }

        public void OverwriteEndingText(string author, string newText)
        {
            var concernedItems = this._forms.Where(i => i.AuthorName == author && i.Status != Form.State.Archived);
            foreach (var form in concernedItems)
            {
                form.EndingTxt = newText;
            }
        }
    }
}
