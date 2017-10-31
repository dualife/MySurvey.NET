// <copyright file="SelectableForm.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using GalaSoft.MvvmLight;

namespace AdminWPFClient.Models
{
    public class SelectableForm : ObservableObject
    {
        private bool isSelected;
        private Form form;

        public SelectableForm(Form item)
        {
            this.form = item;
            this.isSelected = false;
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.Set(ref this.isSelected, value);
            }
        }

        public Form Form
        {
            get
            {
                return this.form;
            }

            set
            {
                this.Set(ref this.form, value);
            }
        }
    }
}
