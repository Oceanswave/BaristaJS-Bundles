// -----------------------------------------------------------------------
// <copyright file="NpmSerialize_v2.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Collections;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
// ReSharper disable once InconsistentNaming
    public class NpmSerialize_v2 : NpmSerialize, INpmSerialize
    {
        /// <summary>
        /// convert npm install output to INpmInstalledPackage enumeration
        /// </summary>
        /// <param name="output">text output</param>
        /// <returns>enumerable INpmInstalledPackage properties</returns>
        public new IEnumerable<INpmInstalledPackage> FromInstall(string output)
        {
            string wrapOutput;

            // sometimes the output has some text lines pre-pended, look for the first
            // json delimiter
            var firstDelimiter = output.IndexOfAny(new[] { '[', '{' });
            if (firstDelimiter > 0)
            {
                output = output.Substring(firstDelimiter);
            }

            if (output[0] == '[')
            {
                // returns array without being wrapped in object.
                // wrap this in an object to allow json parser to work
                wrapOutput = "{ INSTALLROOT: " + output + " }";
            }
            else
            {
                wrapOutput = output;
            }

            var installed = new List<INpmInstalledPackage>();
            Dictionary<string, object> listObj;

            try
            {
                listObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(wrapOutput);
            }
            catch (InvalidOperationException ex)
            {
                throw new NpmException(ParseErrorList, ex);
            }
            catch (ArgumentException ex)
            {
                throw new NpmException(ParseErrorList, ex);
            }

            try
            {
                if (listObj != null && listObj.Count > 0)
                {
                    object root;
                    listObj.TryGetValue("INSTALLROOT", out root);
                    var rootList = root as ArrayList;
                    if (rootList != null)
                    {
                        foreach (var topInstall in rootList)
                        {
                            var topDict = topInstall as Dictionary<string, object>;
                            if (topDict != null)
                            {
                                object name;
                                topDict.TryGetValue("name", out name);
                                InstalledPackageFromDictionary(installed, name as string, string.Empty, topDict);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NpmException(ConvertErrorList, ex);
            }

            return installed;
        }
    }
}
