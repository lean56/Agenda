using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_lenguaje.Entidades
{
    public class Contexto : DbContext
    {
        public DbSet<Agenda> agenda { get; set; }

        public DbSet<Eventos> evento { get; set; }

        public Contexto() : base("Constr") { }
    }
}
