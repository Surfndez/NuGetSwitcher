namespace NuGetSwitcher.Option
{
    public interface IOptionEntity
    {
        string IncludeProjectFile
        {
            get;
            set;
        }

        string IncludeLibraryFile
        {
            get;
            set;
        }

        string ExcludeProjectFile
        {
            get;
            set;
        }
    }
}