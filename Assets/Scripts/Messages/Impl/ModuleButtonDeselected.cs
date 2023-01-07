using Assets.Scripts.Messages;
using Erntemaschine.Vehicles.Modules;

namespace Erntemaschine.Messages.Impl
{
    internal class ModuleButtonDeselected : IMessage
    {
        public ModuleItem Item { get; }

        public ModuleButtonDeselected(ModuleItem item)
        {
            Item = item;
        }
    }
}