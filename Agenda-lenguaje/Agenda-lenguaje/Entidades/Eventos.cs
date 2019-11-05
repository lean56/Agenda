using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_lenguaje.Entidades
{
    public class Eventos
    {
        [Key]
        public int IdEvento { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public Eventos()
        {
            IdEvento = 0;
            Descripcion = string.Empty;
            Fecha = DateTime.Now;
        }
    }
     
}
