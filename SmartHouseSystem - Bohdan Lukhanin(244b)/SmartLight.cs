namespace SmartHomeSystem;

public class SmartLight : SmartDevice
{
    public int Brightness { get; set; }

    public SmartLight(string name, double powerUsage, int brightness)
        : base(name, powerUsage)
    {
        this.Brightness = brightness;
    }

    public override void PerformTask()
    {
        if (this.IsOn)
        {
            // повернутись перевірити за оновлення змінної
            this.Brightness = 100;
            Console.WriteLine($"{Name} is shining brightly at {Brightness}%!");
        }
        else
        {
            this.Brightness = 0;
            Console.WriteLine($"{Name} is off. Can't shine..");
        }
    }

    public override string ToString()
    {
        return base.ToString() + $"Brightness: {Brightness}%";
    }
}
