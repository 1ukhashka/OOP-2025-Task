using System;

namespace SmartHomeSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // ініціалізуємо нашого контролера
            HomeController home = new HomeController();

            // 1 створення приладів
            var lamp = new SmartLight("Kitchen Lamp", 15, 100);
            var ac = new AirConditioner("Samsung AC 3000", 2000, 19.1);
            var coffeeMachine = new SmartCoffeeMachine("Barista 17 Max Pro", 1450);

            // 2 додавання в систему
            home.AddDevice(lamp);
            home.AddDevice(ac);
            home.AddDevice(coffeeMachine);

            // 3 базове управління якраз через поліморфізм
            Console.WriteLine("\nWaking up..");
            home.ToggleDevice("Kitchen Lamp");
            home.ToggleDevice("Samsung AC 3000");
            home.ToggleDevice("Barista 17 Max Pro");  
            
            home.ActivateTask("Kitchen Lamp"); // світить)

            // 4 робота з дженериками, з унікальними функціями
            
            // налаштуєм кондиціонер
            // говоримо так - дай мені прилад 'Samsung AC 3000', але я хочу, щоб це був саме AirConditioner"
            // бо коли ми дістаєм "з коробки", він в нас просто як девайс, а ми хочемо от наші конкретні функції,
            // в цьому випадку - зекономити гроші коли рахунок прийде за ЕЕ
            var myAC = home.GetDevice<AirConditioner>("Samsung AC");
            if (myAC != null)
            {
                myAC.ToggleEcoMode(); // вмикаємо економний режим
                myAC.PerformTask();
            }

            // процес кави, тут теж ми беремо саме КАВОМАШИНУ, а не залізяку якусь
            var barista = home.GetDevice<SmartCoffeeMachine>("Barista 17 Max Pro");
            if (barista != null)
            {
                // enum доволі зручненький
                barista.MakeCoffee(CoffeeType.Espresso);
                barista.MakeCoffee(CoffeeType.Latte);
                
                barista.Refill(); // поповнюємось
            }

            // 5 фінальний звітт
            home.PrintReport();
        }
    }
}