using Assets.Scripts.Messages;
using Erntemaschine.Vehicles.Modules;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class ModuleConstructionAttempt : IMessage
    {
        public ModuleItem Item { get; }
        public Vector3 Position { get; }
        public float Rotation { get; }

        public ModuleConstructionAttempt(ModuleItem item, Vector3 position, float rotation)
        {
            Item = item;
            Position = position;
            Rotation = rotation;
        }
    }
}
