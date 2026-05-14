using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfigurator.Fabrika
{
    public enum ComputerType
    {
        Office,
        Gaming,
        Home
    }

    public class ComputerFactory
    {
        public static ComputerBuilder CreateBuilder(ComputerType type)
        {
            return type switch
            {
                ComputerType.Office => new OfficeComputerBuilder(),
                ComputerType.Gaming => new GamingComputerBuilder(),
                ComputerType.Home => new HomeComputerBuilder(),
                _ => throw new ArgumentException("Неизвестный типок компьютера, меняй")
            };
        }
    }
}