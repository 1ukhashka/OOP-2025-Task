using System;

namespace NavalVessels
{
    public class Submarine : Vessel
    {
        private const double InitialArmor = 200;
        private bool submergeMode;

        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, InitialArmor)
        {
            this.SubmergeMode = false;
        }

        public bool SubmergeMode
        {
            get => submergeMode;
            private set => submergeMode = value;
        }

        public void ToggleSubmergeMode()
        {
            this.SubmergeMode = !this.SubmergeMode;

            if (this.SubmergeMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
            else
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
        }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < InitialArmor)
            {
                this.ArmorThickness = InitialArmor;
            }
        }

        public override string ToString()
        {
            string baseInfo = base.ToString();
            string status = this.SubmergeMode ? "ON" : "OFF";
            return baseInfo + Environment.NewLine + $" *Submerge mode: {status}";
        }
    }
}