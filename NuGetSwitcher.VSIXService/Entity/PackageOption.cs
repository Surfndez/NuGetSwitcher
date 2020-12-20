using Microsoft.VisualStudio.Shell;

using NuGetSwitcher.Option;

using System.ComponentModel;

namespace NuGetSwitcher.VSIXService.Entity
{
    public class PackageOption : DialogPage, IOptionEntity
    {
        /// <summary>
        /// For internal use only.
        /// </summary>
        [Category("Main"), DisplayName("Include Project File"), Description("Path to a file containing enumerations of directories used to include projects")]
        public string IncludeProjectFile
        {
            get;
            set;
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [Category("Main"), DisplayName("Include Library File"), Description("Path to a file containing enumerations of directories used to include libraries")]
        public string IncludeLibraryFile
        {
            get;
            set;
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [Category("Main"), DisplayName("Exclude File"), Description("Path to a file containing enumerations of directories used to exclude projects and libraries")]
        public string ExcludeProjectFile
        {
            get;
            set;
        }
    }
}