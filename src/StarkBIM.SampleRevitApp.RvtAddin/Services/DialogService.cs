// <copyright file="DialogService.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Services
{
    using System;

    using StarkBIM.SampleRevitApp.Model.Core;
    using StarkBIM.SampleRevitApp.Model.Services;

    public class DialogService : IDialogService
    {
        /// <inheritdoc />
        public void NotifyException(Exception exception, object o, LogLevel critical)
        {
            throw new NotImplementedException();
        }
    }
}
