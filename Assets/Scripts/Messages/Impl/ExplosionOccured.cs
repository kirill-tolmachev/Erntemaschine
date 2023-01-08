using Assets.Scripts.Messages;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class ExplosionOccured : IMessage
    {
        public Vector3 Position { get; }

        public ExplosionOccured(Vector3 position)
        {
            Position = position;
        }
    }
}
