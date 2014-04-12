// -----------------------------------------------------------------------
// <copyright file="NpmInstalledPackage.cs" company="Microsoft Open Technologies, Inc.">
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

    /// <summary>
    /// NpmPackage plus dependencies
    /// </summary>
    public class NpmInstalledPackage : INpmInstalledPackage, IEquatable<INpmInstalledPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmInstalledPackage" /> class.
        /// </summary>
// ReSharper disable once EmptyConstructor
        public NpmInstalledPackage()
        {
        }

        /// <summary>
        /// Gets or sets name of Npm object
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets version of Npm object if known
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the '/' delimited parents for this installation
        /// </summary>
        public string DependentPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is missing
        /// </summary>
        public bool IsMissing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package is outdated
        /// </summary>
        public bool IsOutdated
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the package has dependencies
        /// </summary>
        public bool HasDependencies
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="package">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmInstalledPackage package)
        {
            if (package == null)
            {
                return false;
            }

            if (Name != package.Name)
            {
                return false;
            }

            if (Version != package.Version)
            {
                return false;
            }

            if (DependentPath != package.DependentPath)
            {
                return false;
            }

            if (IsMissing != package.IsMissing)
            {
                return false;
            }

            if (IsOutdated != package.IsOutdated)
            {
                return false;
            }

            if (HasDependencies != package.HasDependencies)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Test if another object matches this one
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as INpmInstalledPackage);
        }

        /// <summary>
        /// Calculate hash code
        /// </summary>
        /// <returns>hash value for object</returns>
        public override int GetHashCode()
        {
            var hash = 0;
            if (Name != null)
            {
                hash = hash ^ Name.GetHashCode();
            }

            if (Version != null)
            {
                hash = hash ^ Version.GetHashCode();
            }

            if (DependentPath != null)
            {
                hash = hash ^ DependentPath.GetHashCode();
            }

            return hash;
        }
    }
}
