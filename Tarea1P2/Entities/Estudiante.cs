using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAPADECLASES.Entities
{
    public class Estudiante : MiembroDelaComunidad
    {
        public string Matricula {get; set;}
        public string Carrera {get; set;}
        public string Telefono {get; set;}
    }
}