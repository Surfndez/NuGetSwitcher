﻿using NuGet.ProjectModel;

using NuGetSwitcher.Interface.Contract;

using NuGetSwitcher.Interface.Entity;
using NuGetSwitcher.Interface.Entity.Enum;
using NuGetSwitcher.Interface.Entity.Error;

using NuGetSwitcher.Option;

using System;
using System.Collections.Generic;
using System.IO;

namespace NuGetSwitcher.Core.Switch
{
    public class LibrarySwitch : ProjectSwitch
    {
        public LibrarySwitch(bool isVSIX, ReferenceType type, IOptionProvider packageOption, IProjectProvider projectHelper, IMessageProvider messageHelper) : base(isVSIX, type, packageOption, projectHelper, messageHelper)
        { }

        /// <summary>
        /// Replaces PackageReference references marked with the 
        /// Temp attribute to Reference. Transitive dependencies 
        /// will be included.
        /// </summary>
        ///
        /// <exception cref="SwitcherFileNotFoundException"/>
        /// 
        /// <exception cref="FileNotFoundException"/>
        /// 
        /// <exception cref="ArgumentException">
        public override IEnumerable<string> Switch()
        {
            void Executor(IProjectReference reference, LockFileTargetLibrary library, string absolutePath)
            {
                SwitchSysDependency(reference, library);
                SwitchPkgDependency(reference, library, absolutePath);
            }

            IterateAndExecute(ProjectProvider.GetLoadedProject(), Executor);

            return default;
        }
    }
}