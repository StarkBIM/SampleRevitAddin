// <copyright file="RvtWindow.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Core
{
    using System;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Window that will load the resources defined in RvtResources
    /// </summary>
    public class RvtWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RvtWindow"/> class
        /// </summary>
        public RvtWindow()
        {
            Application.ResourceAssembly = Assembly.GetAssembly(typeof(RvtWindow));

            var uri = new Uri("/Core/RvtResources.xaml", UriKind.Relative);

            object resourceDictionaryObject = Application.LoadComponent(uri);

            var resourceDictionary = (ResourceDictionary)resourceDictionaryObject;

            Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
