using System;
using System.Collections.Generic;
using q5id.guardian.Models;

namespace q5id.guardian.Utils
{
    public static class Constansts
    {
        public static int SEC_TIMEOUT_LOAD_LOCATION = 10;
        public static int MILES_DEFAULT_MAP_ZOOM_DISTANCT = 10;

        public static string HAIR_COLORS_SETTING_KEY = "HairColor";
        public static string EYE_COLORS_SETTING_KEY = "EyeColor";

        public static List<ColorData> HAIR_COLORS = new List<ColorData>()
        {
            new ColorData()
            {
                Name = "Brown",
                HexColor = "#a52a2a"
            },
            new ColorData()
            {
                Name = "Black",
                HexColor = "#000000"
            },
            new ColorData()
            {
                Name = "White",
                HexColor = "#FFFFFF"
            },
            new ColorData()
            {
                Name = "Sandy",
                HexColor = "#ffdd99"
            },
            new ColorData()
            {
                Name = "Gray or partially gray",
                HexColor = "#808080"
            },
            new ColorData()
            {
                Name = "Red/Auburn",
                HexColor = "#ff0000"
            },new ColorData()
            {
                Name = "Blonde/Strawberry",
                HexColor = "#800000"
            },
            new ColorData()
            {
                Name = "Blue",
                HexColor = "#0000ff"
            },
            new ColorData()
            {
                Name = "Green",
                HexColor = "#008000"
            },
            new ColorData()
            {
                Name = "Orange",
                HexColor = "#ffa500"
            },
            new ColorData()
            {
                Name = "Pink",
                HexColor = "#ffc0cb"
            },
            new ColorData()
            {
                Name = "Purple",
                HexColor = "#800080"
            },
            new ColorData()
            {
                Name = "Bald (partially or completely)",
                HexColor = "#92969D"
            }
        };

        public static List<ColorData> EYE_COLORS = new List<ColorData>()
        {
            new ColorData()
            {
                Name = "Brown",
                HexColor = "#a52a2a"
            },
            new ColorData()
            {
                Name = "Blue",
                HexColor = "#0000ff"
            },
            new ColorData()
            {
                Name = "Black",
                HexColor = "#000000"
            },
            new ColorData()
            {
                Name = "Gray",
                HexColor = "#f5f5f0"
            },
            new ColorData()
            {
                Name = "Green",
                HexColor = "#33cc33"
            },
            new ColorData()
            {
                Name = "Hazel",
                HexColor = "#eee8aa"
            },new ColorData()
            {
                Name = "Maroon",
                HexColor = "#800000"
            },
            new ColorData()
            {
                Name = "Pink",
                HexColor = "#ff8095"
            },
            new ColorData()
            {
                Name = "Multicolored",
                HexColor = "#FFFFFF"
            },
        };
    }
}
