﻿// <copyright file="ViewModelLocator.cs" company="GosselCorp" author="gossel_c">
// Copyright © 2016 Cyril GOSSELIN gossel_c@outlook.fr
// This work is free. You can redistribute it and/or modify it under the
// terms of the Do What The Fuck You Want To Public License, Version 2,
// as published by Sam Hocevar. See http://www.wtfpl.net/ for more details.
// </copyright>

using AdminWPFClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AdminWPFClient.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            // DataServices
            SimpleIoc.Default.Register<IUserService, MockUserService>();
            SimpleIoc.Default.Register<IFormsManagementService, MockFormsManagementService>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AcceuilFormsViewModel>();
            SimpleIoc.Default.Register<ArchiveFormsViewModel>();
            SimpleIoc.Default.Register<TextTabViewModel>();
            SimpleIoc.Default.Register<EditAccountViewModel>();
            SimpleIoc.Default.Register<EditFormViewModel>();

        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();

        public AcceuilFormsViewModel AcceuilForms => ServiceLocator.Current.GetInstance<AcceuilFormsViewModel>();

        public ArchiveFormsViewModel ArchiveForms => ServiceLocator.Current.GetInstance<ArchiveFormsViewModel>();

        public TextTabViewModel TextTab => ServiceLocator.Current.GetInstance<TextTabViewModel>();

        public EditAccountViewModel EditAccount => ServiceLocator.Current.GetInstance<EditAccountViewModel>();

        public EditFormViewModel EditForm => ServiceLocator.Current.GetInstance<EditFormViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}