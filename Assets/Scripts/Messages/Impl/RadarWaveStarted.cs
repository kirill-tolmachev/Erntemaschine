using Assets.Scripts.Messages;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class RadarWaveStarted : IMessage
    {
        public Vector3 Origin { get; }

        public Vector3 Direction { get; }

        public RadarWaveStarted(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
