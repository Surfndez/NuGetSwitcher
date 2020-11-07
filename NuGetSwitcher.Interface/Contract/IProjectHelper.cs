using Microsoft.Build.Evaluation;

using NuGetSwitcher.Interface.Entity;
using NuGetSwitcher.Interface.Entity.Error;

using System.Collections.Generic;

namespace NuGetSwitcher.Interface.Contract
{
    public interface IProjectHelper
    {
        /// <summary>
        /// Returns solution projects
        /// that are also loaded into 
        /// the GPC.
        /// </summary>
        IEnumerable<IProjectReference> GetLoadedProject();

        /// <summary>
        /// Returns projects from GPC.
        /// </summary>
        IEnumerable<Project> GetLoadedProject(IEnumerable<string> projects);

        /// <summary>
        /// Returns projects from GPC.
        /// </summary>
        Project GetLoadedProject(string project);

        /// <summary>
        /// Unloads projects from GPC.
        /// </summary>
        /// 
        /// <exception cref="SwitcherInvalidOperationException"/>
        void UnloadProject();

        /// <summary>
        /// Unloads projects from GPC.
        /// </summary>
        /// 
        /// <exception cref="SwitcherInvalidOperationException"/>
        void UnloadProject(Project project);

        /// <summary>
        /// Unloads projects from GPC.
        /// </summary>
        /// 
        /// <exception cref="SwitcherInvalidOperationException"/>
        void UnloadProject(string project);
    }
}