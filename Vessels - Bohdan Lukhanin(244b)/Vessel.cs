using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels
{
    public abstract class Vessel
    {
        private string name;
        private ICaptain captain;

        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            this.Name = name;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.ArmorThickness = armorThickness;
            this.Targets = new List<string>();
        }

        public string Name
        {   
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Vessel name cannot be null or empty.");
                }
                name = value;
            }
        }

        public ICaptain Captain
        {
            get => captain;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Captain cannot be null.");
                }
                captain = value;
            }
        }

        public double ArmorThickness { get; set; }
        public double MainWeaponCaliber { get; protected set; }
        public double Speed { get; protected set; }
        public ICollection<string> Targets { get; }

        public void Attack(Vessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null.");
            }

            target.ArmorThickness -= this.MainWeaponCaliber;

            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }

            this.Targets.Add(target.Name);
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Armor thickness: {this.ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {this.MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {this.Speed} knots");

            string targetsOutput = this.Targets.Count == 0 ? "None" : string.Join(", ", this.Targets);
            sb.Append($" *Targets: {targetsOutput}");

            return sb.ToString();
        }
    }
}