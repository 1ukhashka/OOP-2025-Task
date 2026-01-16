using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels
{
    public class Controller : IController
    {
        private readonly VesselRepository vessels;
        private readonly List<ICaptain> captains;

        public Controller()
        {
            this.vessels = new VesselRepository();
            this.captains = new List<ICaptain>();
        }

        public string HireCaptain(string fullName)
        {
            ICaptain existingCaptain = this.captains.FirstOrDefault(c => c.FullName == fullName);

            if (existingCaptain != null)
            {
                return $"Captain {fullName} is already hired.";
            }

            Captain newCaptain = new Captain(fullName);
            this.captains.Add(newCaptain);

            return $"Captain {fullName} is hired.";
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            Vessel existingVessel = this.vessels.FindByName(name);

            if (existingVessel != null)
            {
                return $"{vesselType} vessel {name} is already manufactured.";
            }

            Vessel vessel;
            if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return "Invalid vessel type.";
            }

            this.vessels.Add(vessel);
            return $"{vesselType} {name} is manufactured with the main weapon caliber of {mainWeaponCaliber} inches and a maximum speed of {speed} knots.";
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = this.captains.FirstOrDefault(c => c.FullName == selectedCaptainName);

            if (captain == null)
            {
                return $"Captain {selectedCaptainName} could not be found.";
            }

            Vessel vessel = this.vessels.FindByName(selectedVesselName);

            if (vessel == null)
            {
                return $"Vessel {selectedVesselName} could not be found.";
            }

            if (vessel.Captain != null)
            {
                return $"Vessel {selectedVesselName} is already occupied.";
            }

            vessel.Captain = captain;
            captain.AddVessel(vessel);

            return $"Captain {selectedCaptainName} command vessel {selectedVesselName}.";
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = this.captains.FirstOrDefault(c => c.FullName == captainFullName);

            if (captain == null)
            {
                return string.Empty;
            }

            return captain.Report();
        }

        public string VesselReport(string vesselName)
        {
            Vessel vessel = this.vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            return vessel.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            Vessel vessel = this.vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            if (vessel is Battleship battleship)
            {
                battleship.ToggleSonarMode();
                return $"Battleship {vesselName} toggled sonar mode.";
            }
            else if (vessel is Submarine submarine)
            {
                submarine.ToggleSubmergeMode();
                return $"Submarine {vesselName} toggled submerge mode.";
            }

            return string.Empty;
        }

        public string ServiceVessel(string vesselName)
        {
            Vessel vessel = this.vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            vessel.RepairVessel();
            return $"Vessel {vesselName} was repaired.";
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            Vessel attacker = this.vessels.FindByName(attackingVesselName);
            Vessel defender = this.vessels.FindByName(defendingVesselName);

            if (attacker == null)
            {
                return $"Vessel {attackingVesselName} could not be found.";
            }

            if (defender == null)
            {
                return $"Vessel {defendingVesselName} could not be found.";
            }

            if (attacker.ArmorThickness == 0)
            {
                return $"Unarmored vessel {attackingVesselName} cannot attack or be attacked.";
            }

            if (defender.ArmorThickness == 0)
            {
                return $"Unarmored vessel {defendingVesselName} cannot attack or be attacked.";
            }

            attacker.Attack(defender);
            attacker.Captain.IncreaseCombatExperience();
            defender.Captain.IncreaseCombatExperience();

            return $"Vessel {defendingVesselName} was attacked by vessel {attackingVesselName} - current armor thickness: {defender.ArmorThickness}.";
        }
    }
}