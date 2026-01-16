using System.Collections.Generic;

namespace NavalVessels
{
    public interface IController
    {
        string HireCaptain(string fullName);

        string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed);

        string AssignCaptain(string selectedCaptainName, string selectedVesselName);

        string CaptainReport(string captainFullName);

        string VesselReport(string vesselName);

        string ToggleSpecialMode(string vesselName);

        string ServiceVessel(string vesselName);

        string AttackVessels(string attackingVesselName, string defendingVesselName);
    }
}