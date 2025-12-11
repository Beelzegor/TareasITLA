using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjGestionEventosEscolares.Entidades
{
    public class E_Evento
    {
       public int IdEvento { get; set; }
       public string Nombre { get; set; }

       public string Direccion { get; set; }
       public string NombreLugar { get; set; }

        public DateTime FechaInicio { get; set; }
        public decimal Presupuesto { get; set; }
    }
}
