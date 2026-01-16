using System;

namespace NavalVessels
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Controller controller = new Controller();

            string input;

            while ((input = Console.ReadLine()) != "Quit")
            {
                try
                {
                    string[] parts = input.Split(' ');
                    string command = parts[0];
                    string output = string.Empty;

                    switch (command)
                    {
                        case "HireCaptain":
                            output = controller.HireCaptain(parts[1]);
                            break;

                        case "ProduceVessel":
                            string name = parts[1];
                            string type = parts[2];
                            double caliber = double.Parse(parts[3]);
                            double speed = double.Parse(parts[4]);
                            output = controller.ProduceVessel(name, type, caliber, speed);
                            break;

                        case "AssignCaptain":
                            output = controller.AssignCaptain(parts[1], parts[2]);
                            break;

                        case "CaptainReport":
                            output = controller.CaptainReport(parts[1]);
                            break;

                        case "VesselReport":
                            output = controller.VesselReport(parts[1]);
                            break;

                        case "ToggleSpecialMode":
                            output = controller.ToggleSpecialMode(parts[1]);
                            break;

                        case "ServiceVessel":
                            output = controller.ServiceVessel(parts[1]);
                            break;

                        case "AttackVessels":
                            output = controller.AttackVessels(parts[1], parts[2]);
                            break;
                    }

                    Console.WriteLine(output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}