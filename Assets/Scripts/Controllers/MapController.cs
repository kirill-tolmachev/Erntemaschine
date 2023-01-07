using System.Collections.Generic;
using Erntemaschine.Vehicles;

namespace Erntemaschine.Controllers
{
    public class MapController
    {
        private HashSet<Part> _spawnedParts = new HashSet<Part>();

        public IReadOnlyCollection<Part> SpawnedParts => _spawnedParts;

        public void AddSpawnedPart(Part part)
        {
            _spawnedParts.Add(part);
        }

        public void RemoveSpawnedPart(Part part)
        {
            _spawnedParts.Remove(part);
        }
    }
}
