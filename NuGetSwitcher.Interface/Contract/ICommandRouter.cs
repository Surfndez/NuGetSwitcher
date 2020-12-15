namespace NuGetSwitcher.Interface.Contract
{
    public interface ICommandRouter
    {
        void Route(string command);
    }
}