using System;
namespace q5id.guardian.Models
{
    public class ColorData
    {
        public string Name { get; set; }
        public string HexColor { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
