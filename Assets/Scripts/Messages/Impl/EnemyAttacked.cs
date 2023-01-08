using Assets.Scripts.Messages;
using Erntemaschine.Enemies;
using Erntemaschine.Vehicles;

namespace Erntemaschine.Messages.Impl
{
    internal class EnemyAttacked : IMessage
    {
        public Enemy Enemy { get; }

        public Part Part { get; }

        public EnemyAttacked(Enemy enemy, Part part)
        {
            Enemy = enemy;
            Part = part;
        }
    }
}