using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaForm
{
    internal class Pizza
    {
        int pazon;
        string pizzaNev;
        int ar;
        public Pizza(string line)
        {
            string[] sor = line.Split(';');
            pazon = int.Parse(sor[0]);
            pizzaNev = sor[1].Trim().Replace("\"", string.Empty);
            Ar = int.Parse(sor[2]);
        }

        public string PizzaNev { get => pizzaNev; set => pizzaNev = value; }
        public int Ar { get => ar; set => ar = value; }
        public int Pazon { get => pazon; set => pazon = value; }
        public override string ToString()
        {
            return $"{pizzaNev} ára: {ar}";
        }
    }
}
