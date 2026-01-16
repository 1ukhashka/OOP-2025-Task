using System.Collections.Generic;

namespace NavalVessels
{
    public interface ICaptain
    {
        string FullName { get; }
        int CombatExperience { get; }
        ICollection<Vessel> Vessels { get; }

        void AddVessel(Vessel vessel);
        void IncreaseCombatExperience();
        string Report();
    }
}