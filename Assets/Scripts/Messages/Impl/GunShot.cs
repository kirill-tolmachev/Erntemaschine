using Assets.Scripts.Messages;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class GunShot : IMessage
    {
        public Vector3 Origin { get; }

        public Vector3 Direction { get; }

        public float BulletSpeed { get; }

        public float BulletDamage { get; }

        public GunShot(Vector3 origin, Vector3 direction, float bulletSpeed, float bulletDamage)
        {
            Origin = origin;
            Direction = direction;
            BulletSpeed = bulletSpeed;
            BulletDamage = bulletDamage;
        }
    }
}
