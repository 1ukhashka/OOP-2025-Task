using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels
{
    public class Captain : ICaptain
    {
        private string fullName;
        private int combatExperience;
        private readonly List<Vessel> vessels;

        public Captain(string fullName)
        {
            this.FullName = fullName;
            this.combatExperience = 0; 
            this.vessels = new List<Vessel>();
        }

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Captain full name cannot be null or empty string.");
                }
                fullName = value;
            }
        }

        public int CombatExperience
        {
            get => combatExperience;
            private set => combatExperience = value;
        }

        public ICollection<Vessel> Vessels => vessels;

        public void AddVessel(Vessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException("Null vessel cannot be added to the captain.");
            }
            this.vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            this.CombatExperience += 10;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.Append($"{this.FullName} has {this.CombatExperience} combat experience and commands {this.vessels.Count} vessels.");

            if (this.vessels.Count > 0)
            {
                sb.AppendLine();
                
                for (int i = 0; i < this.vessels.Count; i++)
                {
                    sb.Append(this.vessels[i].ToString());
                    
                    if (i < this.vessels.Count - 1)
                    {
                        sb.AppendLine();
                    }
                }
            }

            return sb.ToString();
        }
    }
}