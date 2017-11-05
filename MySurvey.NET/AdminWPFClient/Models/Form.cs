// <copyright file="Form.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;
using System.Diagnostics;
using System.Windows;

namespace AdminWPFClient.Models
{
    public class Form
    {
        public enum State
        {
            InProgress = 0,
            Validated = 1,
            Published = 2,
            Ended = 3,
            Archived = 4
        }

        public int Id { get; }

        public string Title { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public string AuthorName { get; set; }

        public State Status { get; set; }

        public int NumberOfAnswers { get; set; }

        public Uri Url { get; set; }

        public string MentionsTxt { get; set; }

        public string ConfirmationTxt { get; set; }

        public string EndingTxt { get; set; }

        public string MentionsFilePath { get; set; }

        public Form(int id)
        {
            this.Id = id;
        }

        public Form(int id, Form original)
        {
            this.Id = id;
            this.Title = original.Title + " Copy";
            this.CreationDate = DateTime.Now;
            this.ModificationDate = DateTime.Now;
            this.AuthorName = original.AuthorName;
            this.Status = original.Status;
            this.NumberOfAnswers = original.NumberOfAnswers;
            this.MentionsTxt  = original.MentionsTxt;
            this.ConfirmationTxt = original.ConfirmationTxt;
            this.EndingTxt = original.EndingTxt;
            this.MentionsFilePath = original.MentionsFilePath;
            // temporary
            this.Url = original.Url;
        }

        public void GoToUrl()
        {
            // reach form URL with default browser
            Process.Start(this.Url.AbsoluteUri);
        }

        public void CopyUrlToClipboard()
        {
            // copy to clipboard form URL
            Clipboard.SetData(DataFormats.Text, this.Url.AbsoluteUri);
        }

        public bool Archive()
        {
            this.ModificationDate = DateTime.Now;
            this.Status = State.Archived;
            return true;
        }

        public bool End()
        {
            this.ModificationDate = DateTime.Now;
            this.Status = State.Ended;
            return true;
        }

        public bool Publish()
        {
            this.ModificationDate = DateTime.Now;
            this.Status = State.Published;
            return true;
        }

        public bool Restore()
        {
            this.ModificationDate = DateTime.Now;
            this.Status = State.InProgress;
            return true;
        }
    }
}
