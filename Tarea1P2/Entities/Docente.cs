using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAPADECLASES.Entities
{
    public class Docente : Empleado
    {
        string Especialidad {get; set;}
        public int HorasDeClase {get; set; }
        public int Salario {get; set; }
    }
}