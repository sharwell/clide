﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VSSDK.Tools.VsIdeTesting;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.ComponentModelHost;
using System.ComponentModel.Composition.Hosting;
using Clide;

[TestClass]
public abstract class VsHostedSpec
{
    public TestContext TestContext { get; set; }

    protected static EnvDTE.DTE Dte
    {
        get { return VsIdeTestHostContext.Dte; }
    }

    protected static IServiceProvider ServiceProvider
    {
        get { return VsIdeTestHostContext.ServiceProvider; }
    }

    protected static CompositionContainer Container
    {
        get { return container.Value; }
    }

    private static Lazy<CompositionContainer> container = new Lazy<CompositionContainer>(() => BuildContainer());

    private static CompositionContainer BuildContainer()
    {
        var componentModel = ServiceProvider.GetService<SComponentModel, IComponentModel>();
        var catalog = new AssemblyCatalog(typeof(IDevEnv).Assembly);

        return new CompositionContainer(catalog, componentModel.DefaultExportProvider);
    }

    [TestInitialize]
    public virtual void TestInitialize()
    {
        if (Dte != null)
        {
            Dte.SuppressUI = false;
            Dte.MainWindow.Visible = true;
            Dte.MainWindow.WindowState = EnvDTE.vsWindowState.vsWindowStateMaximize;
        }
    }

    [TestCleanup]
    public virtual void TestCleanup()
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "None")]
    protected void OpenSolution(string solutionFile)
    {
        VsHostedSpec.DoActionWithWaitAndRetry(
            () => Dte.Solution.Open(solutionFile),
            2000,
            3,
            () => !Dte.Solution.IsOpen);
    }

    protected string GetFullPath(string relativePath)
    {
        return Path.Combine(TestContext.TestDeploymentDir, relativePath);
    }

    protected static void DoActionWithWait(Action action, int millisecondsToWait)
    {
        DoActionWithWaitAndRetry(action, millisecondsToWait, 1, () => true);
    }

    protected static void DoActionWithWaitAndRetry(Action action, int millisecondsToWait, int numberOfRetries, Func<bool> retryCondition)
    {
        int retry = 0;

        do
        {
            action();
            if (retryCondition())
            {
                Thread.Sleep(millisecondsToWait);
                Application.DoEvents();
                retry++;
            }
        }
        while (retryCondition() && retry < numberOfRetries);
    }
}