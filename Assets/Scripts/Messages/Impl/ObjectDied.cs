using Assets.Scripts.Messages;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class ObjectDied : IMessage
    {
        public GameObject Object { get; }

        public ObjectDied(GameObject o)
        {
            Object = o;
        }
    }
}
