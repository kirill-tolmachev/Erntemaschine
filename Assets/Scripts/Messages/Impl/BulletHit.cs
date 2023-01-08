using Assets.Scripts.Messages;
using Erntemaschine.Vehicles;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class BulletHit : IMessage
    {
        public Bullet Bullet { get; }
        public Vector3 Position { get; }

        public GameObject Target { get; }

        public BulletHit(Bullet bullet, GameObject target, Vector3 position)
        {
            Bullet = bullet;
            Target = target;
            Position = position;
        }
    }
}
