// -----------------------------------------------------------------------
// <copyright file="NpmClient.cs" company="Microsoft Open Technologies, Inc.">
// Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0.
//
// THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT
// LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR
// A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache License, Version 2.0 for specific language governing
// permissions and limitations under the License.
// </copyright>
// -----------------------------------------------------------------------

namespace BaristaJS.Appengine.Bundles.Npm.NodeNpm
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Security.Permissions;

    /// <summary>
    /// The class that invokes NPM commands and returns the results text
    /// </summary>
    public class NpmClient : INpmClient
    {
        /// <summary>
        /// The default installation path for node
        /// </summary>
        private const string DefInstallPath = @"%ProgramFiles%\nodejs\";

        /// <summary>
        /// The default relative path to NPM
        /// </summary>
        private const string DefNpmRelativePath = @"node_modules\npm\bin\npm-cli.js";

        /// <summary>
        /// The executable file name for node
        /// </summary>
        private const string NodeExe = "node.exe";

        /// <summary>
        /// Error message
        /// </summary>
        private const string StartFailed = "Failed: process create - executable may already be running: ";

        /// <summary>
        /// Error message
        /// </summary>
        private const string NodeExeNotExistFormat = "The node.exe file is not found at {0}. Please check installation path.";

        /// <summary>
        /// Error message
        /// </summary>
        private const string NpmJSNotExistFormat = "The npm-cli.js file is not found at {0}. Please check installation.";

        /// <summary>
        /// Error message
        /// </summary>
        private const string StartWin32Exception = "Fatal: node process create failed. Verify inner exception message for details";

        /// <summary>
        /// Error message
        /// </summary>
        private const string WaitTimeout = "Timeout: waiting for process completion";

        /// <summary>
        /// Error message
        /// </summary>
        private const string WaitWin32Exception = "Fatal: process wait failed due to Win32 error";

        /// <summary>
        /// Error message
        /// </summary>
        private const string WaitSystemException = "Fatal: process wait failed due to system error";

        /// <summary>
        /// The output from most recent execute
        /// </summary>
        private string m_lastExecuteOutput;

        /// <summary>
        /// The error output from most recent execute
        /// </summary>
        private string m_lastExecuteErrorText;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmClient" /> class.
        /// </summary>
        public NpmClient()
        {
            Initialize(Environment.CurrentDirectory);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmClient" /> class.
        /// Accepts the current project directory.
        /// </summary>
        /// <param name="wd">project directory</param>
        public NpmClient(string wd)
        {
            Initialize(wd);
        }

        /// <summary>
        /// Gets or sets installation path for node
        /// </summary>
        public string InstallPath 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets relative path to NPM from node installation
        /// </summary>
        public string NpmRelativePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets project directory used for some NPM commands
        /// </summary>
        public string WorkingDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets URL of remote registry. Only set if not using default NPM.
        /// </summary>
        public Uri Registry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets HTTP Proxy URL
        /// </summary>
        public string HttpProxy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets HTTPS Proxy URL
        /// </summary>
        public string HttpsProxy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets timeout to use for NPM commands
        /// </summary>
        public int Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the output text from the last NPM command
        /// </summary>
        public string LastExecuteOutput
        {
            get
            {
                return m_lastExecuteOutput;
            }
        }

        /// <summary>
        /// Gets the error text from the last NPM command
        /// </summary>
        public string LastExecuteErrorText
        {
            get
            {
                return m_lastExecuteErrorText;
            }
        }

        /// <summary>
        /// Execute NPM command and save output
        /// </summary>
        /// <param name="cmd">command name</param>
        /// <param name="args">remainder of npm command line</param>
        /// <returns>exit code. 0 is success</returns>
        /// <remarks>LastExecuteOutput and LastExecuteError will be set</remarks>
        [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
        public int Execute(string cmd, string args)
        {
            m_lastExecuteErrorText = string.Empty;
            m_lastExecuteOutput = string.Empty;

            if (string.IsNullOrWhiteSpace(cmd))
            {
                throw new ArgumentNullException("cmd");
            }

            string nodepath = Path.Combine(InstallPath, NodeExe);
            if (!File.Exists(nodepath))
            {
                string error = string.Format(CultureInfo.InvariantCulture, NodeExeNotExistFormat, nodepath);
                throw new NpmException(error);
            }

            string npmcliPath = Path.Combine(InstallPath, NpmRelativePath);
            if (!File.Exists(npmcliPath))
            {
                string error = string.Format(CultureInfo.InvariantCulture, NpmJSNotExistFormat, npmcliPath);
                throw new NpmException(error);
            }

            // npm-cli.js full path in quotes with space trailer for command line
            string npmcli = "\"" + npmcliPath + "\" ";

            using (var nodeNpm = new Process())
            {
                nodeNpm.StartInfo.RedirectStandardError = true;
                nodeNpm.StartInfo.RedirectStandardOutput = true;
                string config = string.Empty;
                if (Registry != null)
                {
                    config = config + " --registry " + Registry;
                }

                if (!string.IsNullOrWhiteSpace(HttpProxy))
                {
                    config = config + " --proxy " + HttpProxy;
                }

                if (!string.IsNullOrWhiteSpace(HttpsProxy))
                {
                    config = config + " --https-proxy " + HttpsProxy;
                }

                if (string.IsNullOrWhiteSpace(args))
                {
                    nodeNpm.StartInfo.Arguments = npmcli + cmd + config;
                }
                else
                {
                    nodeNpm.StartInfo.Arguments = npmcli + cmd + config + " " + args;
                }

                nodeNpm.StartInfo.FileName = nodepath;
                nodeNpm.StartInfo.UseShellExecute = false;
                nodeNpm.StartInfo.WorkingDirectory = WorkingDirectory;
                nodeNpm.StartInfo.CreateNoWindow = true;

                // force colorization off, this will prevent escape characters from showing up
                // in the output we need to parse
                nodeNpm.StartInfo.EnvironmentVariables.Add("npm_config_color", "false");

                // It is not safe to read output and error synchronously
                var sync = NpmSync.AddNpmSync(nodeNpm);
                nodeNpm.OutputDataReceived += StandardOutputHandler;
                nodeNpm.ErrorDataReceived += ErrorOutputHandler;

                try
                {
                    bool started = nodeNpm.Start();
                    if (!started)
                    {
                        // node may already be running
                        throw new NpmException(StartFailed + nodeNpm.StartInfo.FileName);
                    }

                    nodeNpm.BeginOutputReadLine();
                    nodeNpm.BeginErrorReadLine();
                }
                catch (Win32Exception ex)
                {
                    NpmSync.RemNpmSync(sync);
                    throw new NpmException(StartWin32Exception, ex);
                }

                try
                {
                    bool exited;
                    if (Timeout == 0)
                    {
                        nodeNpm.WaitForExit();
// ReSharper disable once RedundantAssignment
                        exited = true;
                    }
                    else
                    {
                        exited = nodeNpm.WaitForExit(Timeout);
                        if (!exited)
                        {
                            NpmSync.RemNpmSync(sync);
                            nodeNpm.Kill();
                            throw new NpmException(WaitTimeout);
                        }

                        // need extra wait to ensure output flushed
                        nodeNpm.WaitForExit();
                    }
                }
                catch (Win32Exception ex)
                {
                    NpmSync.RemNpmSync(sync);
                    throw new NpmException(WaitWin32Exception, ex);
                }
                catch (SystemException ex)
                {
                    NpmSync.RemNpmSync(sync);
                    throw new NpmException(WaitSystemException, ex);
                }

                m_lastExecuteOutput = sync.GetOutput();
                m_lastExecuteErrorText = sync.GetError();
                NpmSync.RemNpmSync(sync);

                return nodeNpm.ExitCode;
            }
        }

        /// <summary>
        /// Delegate to receive stdout text lines
        /// </summary>
        /// <param name="sendingProcess">The sender</param>
        /// <param name="outLine">The event</param>
        private static void ErrorOutputHandler(
                                                object sendingProcess, 
                                                DataReceivedEventArgs outLine)
        {
            Process procObj = (Process)sendingProcess;
            if (procObj != null)
            {
                NpmSync sync = NpmSync.FindNpmSync(procObj);
                if (sync != null)
                {
                    sync.AddToError(outLine.Data);
                }
            }
        }

        /// <summary>
        /// Delegate to receive stderr text lines
        /// </summary>
        /// <param name="sendingProcess">The sender</param>
        /// <param name="outLine">The event</param>
        private static void StandardOutputHandler(
                                                object sendingProcess,
                                                DataReceivedEventArgs outLine)
        {
            Process procObj = (Process)sendingProcess;
            if (procObj != null)
            {
                NpmSync sync = NpmSync.FindNpmSync(procObj);
                if (sync != null)
                {
                    sync.AddToOuput(outLine.Data);
                }
            }
        }

        /// <summary>
        /// Initializes NpmClient.
        /// Accepts the current project directory.
        /// </summary>
        /// <param name="wd">project directory</param>
        private void Initialize(string wd)
        {
            WorkingDirectory = wd;
            Timeout = 0;
            InstallPath = Environment.ExpandEnvironmentVariables(DefInstallPath);
            NpmRelativePath = DefNpmRelativePath;
            Registry = null;
        }
    }
}
