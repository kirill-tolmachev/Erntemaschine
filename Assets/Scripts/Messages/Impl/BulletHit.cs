using Assets.Scripts.Messages;
using Erntemaschine.Vehicles;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class BulletHit : IMessage
    {
        public Bullet Bullet { get; }
        public Vector3 Position { get; }

        public BulletHit(Bullet bullet, Vector3 position)
        {
            Bullet = bullet;
            Position = position;
        }
    }
}
