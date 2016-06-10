// <copyright file="Form.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using System;

namespace AdminWPFClient.Models
{
    public class Form
    {
        public enum State
        {
            InProgress = 0,
            Validated = 1,
            Published = 2,
            Closed = 3,
            Archived = 4
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public string AuthorName { get; set; }

        public State Status { get; set; }

        public int NumberOfAnswers { get; set; }
    }
}
