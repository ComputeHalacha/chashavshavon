﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ChashInstall
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            this.InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            this.Context.LogMessage(">>>> ngenCA: install");
            this.ngenCA(stateSaver, "install");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            this.Context.LogMessage(">>>> ngenCA: commit");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            this.Context.LogMessage(">>>> ngenCA: uninstall");
            this.ngenCA(savedState, "uninstall");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            this.Context.LogMessage(">>>> ngenCA: uninstall");
            this.ngenCA(savedState, "uninstall");
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
        }

        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Chashavshavon.exe");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        private void ngenCA(System.Collections.IDictionary savedState, string ngenCommand)
        {
            string[] argsArray;

            if (string.Compare(ngenCommand, "install", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string args = this.Context.Parameters["Args"];
                if (string.IsNullOrEmpty(args))
                {
                    throw new System.Configuration.Install.InstallException("No arguments specified");
                }

                char[] separators = { ';' };
                argsArray = args.Split(separators);
                savedState.Add("NgenCAArgs", argsArray); //It is Ok to 'ngen uninstall' assemblies which were not installed

            }
            else
            {
                argsArray = (string[])savedState["NgenCAArgs"];
            }

            // Gets the path to the Framework directory.

            string fxPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();

            for (int i = 0; i < argsArray.Length; ++i)
            {
                string arg = argsArray[i];
                // Quotes the argument, in case it has a space in it.

                arg = "\"" + arg + "\"";

                string command = ngenCommand + " " + arg;

                var si = new ProcessStartInfo(Path.Combine(fxPath, "ngen.exe"), command)
                {
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process p;

                try
                {
                    this.Context.LogMessage(">>>>" + Path.Combine(fxPath, "ngen.exe ") + command);
                    p = Process.Start(si);
                    p.WaitForExit();
                }
                catch (Exception ex)
                {
                    throw new System.Configuration.Install.InstallException("Failed to ngen " + arg, ex);
                }
            }
        }
    }
}
