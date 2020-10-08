using System;
using System.Collections.Generic;

namespace PruebaWebMaster000.Models
{
    public partial class Horarios
    {
        public Horarios()
        {
            Restaurantes = new HashSet<Restaurantes>();
        }

        public int IdHorarios { get; set; }
        public string HorariosAtencion { get; set; }

        public virtual ICollection<Restaurantes> Restaurantes { get; set; }
    }
}
