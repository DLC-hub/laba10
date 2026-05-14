using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfigurator
{
    public sealed class PrototypeRegistry
    {
        private static readonly Lazy<PrototypeRegistry> _instance =
            new Lazy<PrototypeRegistry>(() => new PrototypeRegistry());

        private Dictionary<string, Computer> _prototypes = new Dictionary<string, Computer>();

        
        private PrototypeRegistry()
        {
            InitializePrototypes();
        }

        public static PrototypeRegistry Instance => _instance.Value;

        private void InitializePrototypes()
        {
            _prototypes["office"] = new Computer
            {
                Processor = "Intel Core i33",
                Memory = 8,
                GraphicsCard = "Интегральная",
                AdditionalComponents = { "Офисный пакет" }
            };
            _prototypes["gaming"] = new Computer
            {
                Processor = "Intel Core i2077",
                Memory = 32,
                GraphicsCard = "NVIDIA RTX 3080",
                AdditionalComponents = { "Игровая мышь", "Механическая клавиатура" }
            };

            _prototypes["home"] = new Computer
            {
                Processor = "AMD Ryzen 50",
                Memory = 16,
                GraphicsCard = "AMD Radeon RX 580",
                AdditionalComponents = { "Вебка" }
            };
        }

        public Computer GetPrototype(string key)
        {
            lock (_prototypes)
            {
                return _prototypes.ContainsKey(key) ? _prototypes[key] : null;
            }
        }

        public void AddPrototype(string key, Computer prototype)
        {
            lock(_prototypes)
            {
                _prototypes[key] = prototype;
            }
        }
    }
}
