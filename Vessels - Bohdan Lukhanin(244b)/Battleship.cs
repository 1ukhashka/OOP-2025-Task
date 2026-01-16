using System;

namespace NavalVessels
{
    public class Battleship : Vessel
    {
        private const double InitialArmor = 300;
        private bool sonarMode;

        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, InitialArmor)
        {
            this.SonarMode = false;
        }

        public bool SonarMode
        {
            get => sonarMode;
            private set => sonarMode = value;
        }

        public void ToggleSonarMode()
        {
            this.SonarMode = !this.SonarMode;

            if (this.SonarMode)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
            }
            else
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
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
            string status = this.SonarMode ? "ON" : "OFF";
            return baseInfo + Environment.NewLine + $" *Sonar mode: {status}";
        }
    }
}