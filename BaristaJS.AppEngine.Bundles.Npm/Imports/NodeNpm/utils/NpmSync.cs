// -----------------------------------------------------------------------
// <copyright file="NpmSync.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Object used to synchronize client and delegate output handlers
    /// </summary>
    internal class NpmSync
    {
        /// <summary>
        /// List of active NPM requests
        /// </summary>
        private static List<NpmSync> s_runningNpms;

        /// <summary>
        /// The process object
        /// </summary>
        private readonly Process m_nodeNpm;

        /// <summary>
        /// The output text string builder
        /// </summary>
        private StringBuilder m_output;

        /// <summary>
        /// The error text string builder
        /// </summary>
        private StringBuilder m_errorOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="NpmSync" /> class.
        /// </summary>
        /// <param name="npm">Process object</param>
        private NpmSync(Process npm)
        {
            m_nodeNpm = npm;
            m_output = null;
            m_errorOutput = null;
        }

        /// <summary>
        /// Get NpmSync for Process object
        /// </summary>
        /// <param name="obj">Process object</param>
        /// <returns>matching NpmSync</returns>
        internal static NpmSync FindNpmSync(Process obj)
        {
            if (s_runningNpms == null)
            {
                return null;
            }

            return s_runningNpms.FirstOrDefault(sync => sync.m_nodeNpm == obj);
        }

        /// <summary>
        /// Create new NpmSync and associate it with this Porcess object
        /// </summary>
        /// <param name="obj">Process object</param>
        /// <returns>NpmSync to track this request</returns>
        internal static NpmSync AddNpmSync(Process obj)
        {
            if (s_runningNpms == null)
            {
                s_runningNpms = new List<NpmSync>();
            }

            var sync = new NpmSync(obj);
            s_runningNpms.Add(sync);
            return sync;
        }

        /// <summary>
        /// Remove the NpmSync object
        /// </summary>
        /// <param name="sync">sync object</param>
        internal static void RemNpmSync(NpmSync sync)
        {
            if (sync != null)
            {
                s_runningNpms.Remove(sync);
            }
        }

        /// <summary>
        /// Remove the NpmSync for the specified Process object
        /// </summary>
        /// <param name="obj">Process object</param>
        internal static void RemNpmSync(Process obj)
        {
            if (s_runningNpms == null)
            {
                return;
            }

            NpmSync sync = FindNpmSync(obj);
            if (sync != null)
            {
                s_runningNpms.Remove(sync);
            }
        }

        /// <summary>
        /// Appends line to output
        /// </summary>
        /// <param name="line">Line of text</param>
        internal void AddToOuput(string line)
        {
            if (m_output == null)
            {
                m_output = new StringBuilder();
            }

            m_output.AppendLine(line);
        }

        /// <summary>
        /// Appends line to error
        /// </summary>
        /// <param name="line">Line of text</param>
        internal void AddToError(string line)
        {
            if (m_errorOutput == null)
            {
                m_errorOutput = new StringBuilder();
            }

            m_errorOutput.AppendLine(line);
        }

        /// <summary>
        /// Retrieve output text
        /// </summary>
        /// <returns>output as string</returns>
        internal string GetOutput()
        {
            if (m_output == null)
            {
                return string.Empty;
            }

            return m_output.ToString();
        }

        /// <summary>
        /// Retrieve error text
        /// </summary>
        /// <returns>error as string</returns>
        internal string GetError()
        {
            if (m_errorOutput == null)
            {
                return string.Empty;
            }

            return m_errorOutput.ToString();
        }
    }
}
