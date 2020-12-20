using Microsoft.Build.Evaluation;

using Microsoft.VisualStudio.Shell;

using NuGetSwitcher.Interface.Contract;
using NuGetSwitcher.Interface.Entity;

using NuGetSwitcher.VSIXService.Entity;

using System.Collections.Generic;
using System.Linq;

namespace NuGetSwitcher.VSIXService
{
    public class ProjectProvider : IProjectProvider
    {
        protected EnvDTE.DTE DTE
        {
            get;
            set;
        }

        public ProjectProvider()
        {
            DTE = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
        }

        /// <summary>
        /// Absolute path to the solution file.
        /// </summary>
        public string Solution
        {
            get => DTE.Solution.FullName;
        }

        /// <summary>
        /// Returns solution projects
        /// that are also loaded into 
        /// the GPC.
        /// </summary>
        public virtual IEnumerable<IProjectReference> GetLoadedProject()
        {
            List<IProjectReference> projects = new
            List<IProjectReference>(30);

            foreach (EnvDTE.Project dteProject in DTE.Solution.Projects)
            {
                // To filter miscellaneous files.
                if (string.IsNullOrWhiteSpace(dteProject.FileName))
                {
                    continue;
                }

                projects.Add(new ProjectReference(dteProject, GetLoadedProject(dteProject.FullName)));
            }

            return projects;
        }

        /// <summary>
        /// Returns projects from GPC.
        /// </summary>
        public virtual IEnumerable<Project> GetLoadedProject(IEnumerable<string> projects)
        {
            return projects.Select(p => GetLoadedProject(p));
        }

        /// <summary>
        /// Returns projects from GPC.
        /// </summary>
        public virtual Project GetLoadedProject(string project)
        {
            return ProjectCollection.GlobalProjectCollection.LoadProject(project);
        }
    }
}