using Assets.Scripts.Messages;
using Erntemaschine.Vehicles.Modules;

namespace Erntemaschine.Messages.Impl
{
    internal class ModuleButtonSelected : IMessage
    {
        public ModuleItem Item { get; }

        public ModuleButtonSelected(ModuleItem item)
        {
            Item = item;
        }
    }
}
