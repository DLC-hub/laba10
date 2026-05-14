using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Generic;

namespace ComputerConfigurator
{
    public class Computer
    {
        // Свойства компьютера
        public string Processor { get; set; }
        public int Memory { get; set; } // в ГБ
        public string GraphicsCard { get; set; }
        public List<string> AdditionalComponents { get; set; } = new List<string>();

        // Метод для поверхностного копирования (прототип)
        public Computer Clone() => (Computer)this.MemberwiseClone();

        // Переопределяем ToString для удобного вывода
        public override string ToString()
        {
            return $"Процессор: {Processor}, Память: {Memory} ГБ, Видеокарта: {GraphicsCard}, Доп. компоненты: {string.Join(", ", AdditionalComponents)}";
        }
    }
}



