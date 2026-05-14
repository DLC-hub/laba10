using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfigurator
{
    
    public abstract class ComputerBuilder
    {
        protected Computer computer = new Computer();

        public abstract void SetProcessor();
        public abstract void SetMemory();
        public abstract void SetGraphicsCard();
        public abstract void AddAdditionalComponents();

        
        public Computer GetComputer() => computer;
    }

    
    public class OfficeComputerBuilder : ComputerBuilder
    {
        public override void SetProcessor() => computer.Processor = "Intel Core i3";
        public override void SetMemory() => computer.Memory = 8;
        public override void SetGraphicsCard() => computer.GraphicsCard = "Интегрированная";
        public override void AddAdditionalComponents() => computer.AdditionalComponents.Add("Офисный пакет");
    }

    public class GamingComputerBuilder : ComputerBuilder
    {
        public override void SetProcessor() => computer.Processor = "Intel Core i7";
        public override void SetMemory() => computer.Memory = 32;
        public override void SetGraphicsCard() => computer.GraphicsCard = "NVIDIA RTX 3080";
        public override void AddAdditionalComponents()
        {
            computer.AdditionalComponents.Add("Игровая мышь");
            computer.AdditionalComponents.Add("Механическая клавиатура");
        }
    }

    public class HomeComputerBuilder : ComputerBuilder
    {
        public override void SetProcessor() => computer.Processor = "AMD Ryzen 5";
        public override void SetMemory() => computer.Memory = 16;
        public override void SetGraphicsCard() => computer.GraphicsCard = "AMD Radeon RX 580";
        public override void AddAdditionalComponents() => computer.AdditionalComponents.Add("Веб‑камера");
    }
}

