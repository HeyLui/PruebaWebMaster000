using System;
using System.Collections.Generic;

namespace PruebaWebMaster000.Models
{
    public partial class Calificacion
    {
        public int IdVotos { get; set; }
        public int? IdRestaurante { get; set; }
        public int? Calificacion1 { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }

        public virtual Restaurantes IdRestauranteNavigation { get; set; }
    }
}
