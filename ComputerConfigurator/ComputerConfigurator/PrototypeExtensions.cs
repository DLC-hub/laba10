using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfigurator
{
    
    public static class PrototypeExtensions
    {
        public static Computer DeepClone(this Computer original)
        {
            var clone = new Computer
            {
                Processor = original.Processor,
                Memory = original.Memory,
                GraphicsCard = original.GraphicsCard
            };
           
            clone.AdditionalComponents = new List<string>(original.AdditionalComponents);
            return clone;
        }
    }
}
