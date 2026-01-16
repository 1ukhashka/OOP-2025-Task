using System.Collections.Generic;
using System.Linq;

namespace NavalVessels
{
    public class VesselRepository
    {
        private readonly List<Vessel> vessels;

        public VesselRepository()
        {
            this.vessels = new List<Vessel>();
        }

        public IReadOnlyCollection<Vessel> Models => this.vessels.AsReadOnly();

        public void Add(Vessel vessel)
        {
            this.vessels.Add(vessel);
        }

        public bool Remove(Vessel vessel)
        {
            return this.vessels.Remove(vessel);
        }

        public Vessel FindByName(string name)
        {
            return this.vessels.FirstOrDefault(v => v.Name == name);
        }
    }
}