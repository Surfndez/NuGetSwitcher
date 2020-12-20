using NuGetSwitcher.Interface.Contract;
using NuGetSwitcher.Interface.Entity.Enum;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace NuGetSwitcher.Core.Abstract
{
    public abstract class AbstractOption : IOptionProvider
    {
        private struct FileOption
        {
            public string[] Include
            {
                get;
                set;
            }

            public string[] Exclude
            {
                get;
                set;
            }

            public string Pattern
            {
                get;
                set;
            }

            public FileOption(string includeFile, string excludeFile, string pattern)
            {
                if (!File.Exists(includeFile))
                {
                    throw new FileNotFoundException($"Configuration file: { includeFile } not specified or not found");
                }
                else
                {
                    Include = File.ReadAllLines(includeFile);
                }

                if (!File.Exists(excludeFile))
                {
                    Exclude = new string[0];
                }
                else
                {
                    Exclude = File.ReadAllLines(excludeFile);
                }

                Pattern = pattern;
            }
        }

        /// <summary>
        /// Returns the structure containing an array of
        /// directories to search for and directories to
        /// exclude from searches.
        /// </summary>
        /// 
        /// <exception cref="FileNotFoundException"/>
        /// 
        /// <exception cref="ArgumentException">
        private FileOption GetFileOption(ReferenceType type)
        {
            FileOption option;

            switch (type)
            {
                case ReferenceType.ProjectReference:
                    option = new FileOption(IncludeProjectFile, ExcludeProjectFile, "*.csproj");
                    break;
                case ReferenceType.Reference:
                    option = new FileOption(IncludeLibraryFile, ExcludeProjectFile, "*.dll");
                    break;
                default: throw new ArgumentException($"Reference type not supported: { type }");
            }

            return option;
        }

        /// <summary>
        /// Returns true if a directory containing the file
        /// is found. Level at which the file is located is 
        /// not taken into account.
        /// </summary>
        private bool Contains(string[] excludeDirectories, string absolutePath)
        {
            const int NOT_FOUND = -1;

            return excludeDirectories.Any(d => absolutePath.IndexOf(d) != NOT_FOUND);
        }

        /// <summary>
        /// Returns a dictionary where file names are 
        /// used as keys, and absolute file paths are 
        /// values.
        /// </summary>
        /// 
        /// <exception cref="FileNotFoundException"/>
        /// 
        /// <exception cref="ArgumentException">
        public virtual ReadOnlyDictionary<string, string> GetIncludeItems(ReferenceType type)
        {
            FileOption option = GetFileOption(type);

            Dictionary<string, string> output = new
            Dictionary<string, string>(30);

            foreach (string directory in option.Include)
            {
                if (!Directory.Exists(directory))
                {
                    MessageHelper.AddMessage($"Directory: { directory } does not exist and will be skipped", MessageCategory.WG);

                    continue;
                }

                foreach (string absolutePath in Directory.GetFiles(directory, option.Pattern, SearchOption.AllDirectories).Where(i => !Contains(option.Exclude, i)))
                {
                    string key = Path.GetFileNameWithoutExtension(absolutePath);

                    if (!output.ContainsKey(key))
                    {
                        output.Add(key, absolutePath);
                    }
                    else
                    {
                        MessageHelper.AddMessage($"Project: { absolutePath } is a duplicate and will be skipped", MessageCategory.WG);
                    }
                }
            }

            return new ReadOnlyDictionary<string, string>(output);
        }
    }
}