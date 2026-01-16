using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHomeSystem
{
    public class HomeController
    {
        // списочек наших девайсів
        private readonly List<SmartDevice> devices;

        public HomeController()
        {
            this.devices = new List<SmartDevice>();
        }

        // додавання пристрою
        public void AddDevice(SmartDevice device)
        {
            // підсвічувало помилку але ніби нема
            if (device == null) return;

            // перевірка на вже існуючу назву
            if (devices.Any(d => d.Name == device.Name))
            {
                Console.WriteLine($"[Error]: Device '{device.Name}' already exists!");
                return;
            }

            devices.Add(device);
            Console.WriteLine($"[systen]: Added '{device.Name}'.");
        }

        // типу універсальний перемикач (працює для всіх девайсів)
        public void ToggleDevice(string name)
        {
            var device = devices.FirstOrDefault(d => d.Name == name);
            if (device != null)
            {
                device.TogglePower();
                Console.WriteLine($"[system]: '{device.Name}' is now {(device.IsOn ? "ON" : "OFF")},");
            }
            else
            {
                Console.WriteLine($"[Error]: Device '{name}' not found");
            }
        }

        // абстрактна наша дія
        public void ActivateTask(string name)
        {
            var device = devices.FirstOrDefault(d => d.Name == name);
            if (device != null)
            {
                // наша сила поліморфізму, викличеться метод певного класу
                device.PerformTask(); 
            }
        }

        // пункт 4* - дженерик метод, задовго возився але працює ніби ок
        public T GetDevice<T>(string name) where T : SmartDevice
        {
            // шукаєм девайс за іменем
            var device = devices.FirstOrDefault(d => d.Name == name);
            
            return device as T; 
        }

        // підраховуємо енергіюю
        public double GetTotalPower()
        {
            return devices.Where(d => d.IsOn).Sum(d => d.PowerUsage);
        }

        // звіт по споживанням
        public void PrintReport()
        {
            Console.WriteLine("\n---  HOME STATUS ---");
            if (devices.Count == 0) Console.WriteLine("System empty.");

            foreach (var d in devices)
            {
                Console.WriteLine(d.ToString()); // працює без помилок але перевіряв разів 10
                Console.WriteLine("-");
            }
            Console.WriteLine($"Total Load: {GetTotalPower()} W");
            Console.WriteLine("----------=--------\n");
        }
    }
}