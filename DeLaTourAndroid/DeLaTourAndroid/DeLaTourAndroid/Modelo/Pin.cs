using System;
using System.Collections.Generic;
using System.Text;

namespace DeLaTourAndroid.Modelo
{
    public class Pin
    {
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public Coords coords { get; set; }
        public int estatus { get; set; }
        public string imagen { get; set; }
    }
}
