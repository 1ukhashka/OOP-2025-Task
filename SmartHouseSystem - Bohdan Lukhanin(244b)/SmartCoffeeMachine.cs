using System;

namespace SmartHomeSystem
{
    public enum CoffeeType
    {
        Espresso,
        Americano,
        Latte
    }

    public class SmartCoffeeMachine : SmartDevice
    {
        public int WaterLevel { get; private set; } // мл
        public int BeanLevel { get; private set; }  // г
        public int MilkLevel { get; private set; }  // м

        // максимальні обсяги
        private const int MaxWater = 1000;
        private const int MaxBeans = 500;
        private const int MaxMilk = 500;

        public SmartCoffeeMachine(string name, double powerUsage) 
            : base(name, powerUsage)
        {
            // при покупці машину маємо повною
            WaterLevel = MaxWater;
            BeanLevel = MaxBeans;
            MilkLevel = MaxMilk;
        }

        // меетод поповнення ресурсів
        public void Refill()
        {
            WaterLevel = MaxWater;
            BeanLevel = MaxBeans;
            MilkLevel = MaxMilk;
            Console.WriteLine($"{Name}: all ingredients refilled to max");
        }

        // логіка приготування
        public void MakeCoffee(CoffeeType type)
        {
            if (!IsOn)
            {
                Console.WriteLine($"{Name} is off... turn it on first!!");
                return;
                
            }
            

            // початкові... потреби для рецепту
            int waterNeeded = 0;
            int beansNeeded = 0;
            int milkNeeded = 0;

            switch (type)
            {
                case CoffeeType.Espresso:
                    waterNeeded = 30; beansNeeded = 20; milkNeeded = 0;
                    break;
                
                case CoffeeType.Americano:
                    waterNeeded = 150; beansNeeded = 20; milkNeeded = 0;
                    break;
                case CoffeeType.Latte:
                    waterNeeded = 50; beansNeeded = 20; milkNeeded = 150;
                    break;
            }

            // Пперевіряємо наявність інгредієнтів
            if (WaterLevel < waterNeeded || BeanLevel < beansNeeded || MilkLevel < milkNeeded)
            {
                Console.WriteLine($"{Name}: not enough ingredients for {type}! Please refill");
                Console.WriteLine($" debug: Water: {WaterLevel}, beans: {BeanLevel}, Milk: {MilkLevel}");
                return;
            }

            // забираємо продукти і на приготування
            WaterLevel -= waterNeeded;
            BeanLevel -= beansNeeded;
            MilkLevel -= milkNeeded;
            
            
            Console.WriteLine($"{Name}: brewing delicious {type}.. Done!");
        }

        // той самий абстрактний метод (робить першу дефолтну каву)
        public override void PerformTask()
        {
            MakeCoffee(CoffeeType.Espresso);
            
        }

        
        
        public override string ToString()
        {
            return base.ToString() + $"[Status] Water: {WaterLevel}ml, beans: {BeanLevel}g, Milk: {MilkLevel}ml";
            
        }
        
        
    }
}