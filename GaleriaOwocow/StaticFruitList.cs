using System.Collections.Generic;

namespace GaleriaOwocow
{
    public class StaticFruitList
    {
        public static List<FruitModel> FruitList { get; set; } = GetFruits();

        public static List<FruitModel> GetFruits()
        {
            return new List<FruitModel>
            {
                new() {Id = 1, Name = "Jabłko", Color = "Czerwony"},
                new() {Id = 1, Name = "Banan", Color = "Żółty"},
            };
        }
    }
}
