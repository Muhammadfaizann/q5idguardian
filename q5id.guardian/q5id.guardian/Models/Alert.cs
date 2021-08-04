using System;
using Xamarin.Forms.Maps;

namespace q5id.guardian.Models
{
    public class Alert
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public Position Position { get; set; }
    }
}
