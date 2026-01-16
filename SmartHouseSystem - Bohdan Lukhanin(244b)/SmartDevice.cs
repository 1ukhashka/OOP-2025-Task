using System;
using System.Collections.Generic;
using System.Text;


namespace SmartHomeSystem
{
    public abstract class SmartDevice
    {
        private string name;

        protected SmartDevice(string name, double powerUsage)
        {
            this.Name = name;
            this.PowerUsage = powerUsage;
            this.IsOn = false;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace.");
                }
                name = value; 
            }
        }

        // повернутись до цього, помилка в контрольорі?
        public double PowerUsage { get; set; }

        public bool IsOn { get; private set; }

        public void TogglePower()
        {
            this.IsOn = !this.IsOn;
        }

        // задаємо що умовно кожний пристрій матиме свою фічу, отако от
        public abstract void PerformTask();

        public override string ToString()
        {
            var ts = new StringBuilder();
            ts.AppendLine($"Device Type: {this.GetType().Name}"); // додав тип пристрою перевірити чи працює*
            ts.AppendLine($"Name: {this.Name}");
            ts.AppendLine($"Power: {this.PowerUsage}W");
            ts.AppendLine($"Status: {(this.IsOn ? "ON" : "OFF")}");
            return ts.ToString(); // перевірити з стрінгбілдер і без, поки працює 13.01*
        }
    }}