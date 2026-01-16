using System;

namespace SmartHomeSystem
{
    public class AirConditioner : SmartDevice
    {
        public double Temperature { get; set; }
        public bool EcoMode { get; private set; }

        public AirConditioner(string name, double powerUsage, double temperature)
            : base(name, powerUsage)
        {
            this.Temperature = temperature;
            this.EcoMode = false;
        }

        public void ToggleEcoMode()
        {
            EcoMode = !EcoMode;
            if (EcoMode)
            {
                PowerUsage /= 2; 
                Console.WriteLine($"{Name}: Eco Mode ON (Power: {PowerUsage}W)");
            }
            else
            {
                PowerUsage *= 2;
                Console.WriteLine($"{Name}: Eco Mode OFF (Power: {PowerUsage}W)");
            }
        }

        public override void PerformTask()
        {
            if (IsOn)
                Console.WriteLine($"{Name} is cooling room to {Temperature}°C.");
            else
                Console.WriteLine($"{Name} is off.");
        }

        public override string ToString()
        {
            return base.ToString() + $"Temperature: {Temperature}°C, Eco: {EcoMode}";
        }
    }
}