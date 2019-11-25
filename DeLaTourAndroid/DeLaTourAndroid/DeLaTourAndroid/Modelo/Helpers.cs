using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace DeLaTourAndroid.Modelo
{
    internal class Helpers
    {
        public static string apiUrl = "http://192.168.137.1:3700/api/";
    }

    public class CustomCircle
    {
        public Position Position { get; set; }
        public int Radius { get; set; }
    }

    public class CustomMap : Map
    {
        public CustomCircle Circle { get; set; }
    }
}
