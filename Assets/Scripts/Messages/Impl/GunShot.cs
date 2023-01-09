using Assets.Scripts.Messages;
using UnityEngine;

namespace Erntemaschine.Messages.Impl
{
    internal class GunShot : IMessage
    {
        public Transform Author { get; }

        public Vector3 Origin { get; }

        public Vector3 Direction { get; }

        public float BulletSpeed { get; }

        public float BulletDamage { get; }

        public float BulletScale { get; }

        public GunShot(Transform author, Vector3 origin, Vector3 direction, float bulletSpeed, float bulletDamage, float bulletScale)
        {
            Author = author;
            Origin = origin;
            Direction = direction;
            BulletSpeed = bulletSpeed;
            BulletDamage = bulletDamage;
            BulletScale = bulletScale;
        }
    }
}
