﻿// -----------------------------------------------------------------------
// <copyright file="NpmSearchResultPackage.cs" company="Microsoft Open Technologies, Inc.">
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
    using System.Collections.Generic;

    /// <summary>
    /// NpmPackage plus properties from search result
    /// </summary>
    public class NpmSearchResultPackage : INpmSearchResultPackage, IEquatable<INpmSearchResultPackage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpmSearchResultPackage" /> class.
        /// </summary>
        /// <param name="name">name of package</param>
        /// <param name="version">version of package</param>
        /// <param name="description">description of package</param>
        /// <param name="author">author of package</param>
        /// <param name="date">date of package</param>
        /// <param name="keywords">keywords for package</param>
        public NpmSearchResultPackage(
                                      string name,
                                      string version,
                                      string description,
                                      string author,
                                      DateTime date,
                                      IEnumerable<string> keywords)
        {
            Name = name;
            Version = version;
            Description = description;
            Author = author;
            Keywords = keywords;
            LatestDate = date;
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
        /// Gets or sets the version of Npm object if known
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author of project
        /// </summary>
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the keywords
        /// </summary>
        public IEnumerable<string> Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date of last publish
        /// </summary>
        public DateTime LatestDate
        {
            get;
            set;
        }

        /// <summary>
        /// Test if another package matches this one
        /// </summary>
        /// <param name="other">NpmPackage to compare</param>
        /// <returns>true if match, false if not matched</returns>
        public bool Equals(INpmSearchResultPackage other)
        {
            if (other == null)
            {
                return false;
            }

            if (Name != other.Name)
            {
                return false;
            }

            if (Description != other.Description)
            {
                return false;
            }

            if (Author != other.Author)
            {
                return false;
            }

            if (!NpmPackage.IsSameStringEnumeration(Keywords, other.Keywords))
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
            return Equals(obj as INpmSearchResultPackage);
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

            if (Description != null)
            {
                hash = hash ^ Description.GetHashCode();
            }

            if (Author != null)
            {
                hash = hash ^ Author.GetHashCode();
            }

            return hash;
        }
    }
}
