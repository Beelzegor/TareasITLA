using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAPADECLASES.Entities
{

    public class Administrador : Docente
    {
        
        public string Rol {get; set;}
        public int Salario {get; set;}
    }
}