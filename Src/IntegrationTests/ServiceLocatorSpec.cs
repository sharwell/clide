﻿#region BSD License
/* 
Copyright (c) 2012, Clarius Consulting
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

namespace Clide
{
    using System;
    using System.Linq;
    using EnvDTE;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.VisualStudio.Text.Editor;

    [TestClass]
    public class ServiceLocatorSpec
    {
        [HostType("VS IDE")]
        [TestMethod]
        public void WhenGettingGlobalLocator_ThenCanGetInstanceVsServices()
        {
            Assert.IsNotNull(ServiceLocator.GlobalLocator.GetInstance<IServiceProvider>());
            Assert.IsNotNull(ServiceLocator.GlobalLocator.GetInstance<IVsShell>());
            Assert.IsNotNull(ServiceLocator.GlobalLocator.GetInstance<DTE>());
        }

        [HostType("VS IDE")]
        [TestMethod]
        public void WhenUsingGlobalLocator_ThenCanGetManyExportedVsComponents()
        {
            System.Diagnostics.Debugger.Launch();
            Assert.IsTrue(ServiceLocator.GlobalLocator.GetAllInstances<IWpfTextViewCreationListener>().Any());
        }

        [HostType("VS IDE")]
        [TestMethod]
        public void WhenGettingGlobalLocator_ThenCanGetServiceForVsServices()
        {
            Assert.IsNotNull(ServiceLocator.GlobalLocator.GetService<IServiceProvider>());
            Assert.IsNotNull(ServiceLocator.GlobalLocator.GetService<IVsShell>());
            Assert.IsNotNull(ServiceLocator.GlobalLocator.GetService<DTE>());
        }
    }
}