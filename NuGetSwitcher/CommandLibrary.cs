using NuGetSwitcher.Interface.Contract;

using NuGetSwitcher.VSIXMenu;

using System;
using System.ComponentModel.Design;

namespace NuGetSwitcher
{
    internal sealed class CommandLibrary : GlobalCommand
    {
        public CommandLibrary(ICommandRouter commandRouter, IMessageProvider messageProvider) : base(commandRouter, messageProvider)
        { }

        /// <summary>
        /// Callback for <see cref="MenuCommand"/>.
        /// </summary>
        public override void Callback(object sender, EventArgs eventArgs)
        {
            try
            {
                CommandRouter.Route(Name);
            }
            catch (Exception exception)
            {
                MessageProvider.AddMessage(exception);
            }
        }
    }
}