using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace CaseyAdventure
{
    class WeaponFunctions
    {
        public static String[] getSymbols(){
            String[] symbols = { "Bar","Seven","Cherries"};
            return symbols;
        }
        public static String[] randomizeSymbols(String[] symbols)
        {
            int symbolLength = symbols.Length;
            Random rnd = new Random();
            for (int i = 0; i < symbolLength; i++){
                symbols[rnd.Next(0, symbolLength)] = symbols[rnd.Next(0, symbolLength)];
            }
            return symbols;
        }
        public static int SpinSlotMachine()
        {
            String[] column1 = randomizeSymbols(getSymbols());
            String[] column2 = randomizeSymbols(getSymbols());
            String[] column3 = randomizeSymbols(getSymbols());

            if (String.Equals(column1[0],column2[0]) && String.Equals(column2[0], column3[0]))
            {
                return column1[0] switch
                {
                    "Seven" => 20,
                    "Bar" => 5,
                    "Cherries" => 2,
                    _ => 0,
                };
            }
            return 0;
        }
    }
}