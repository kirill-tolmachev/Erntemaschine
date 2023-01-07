using UnityEngine;

namespace Erntemaschine.Enemies

{
    internal class Enemy : MonoBehaviour
    {
        public Collider2D Collider => _collider;

        [SerializeField] private Collider2D _collider;

        public bool IsSeen => true;
    }
}
