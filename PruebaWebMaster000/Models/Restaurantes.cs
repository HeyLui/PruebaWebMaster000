using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaWebMaster000.Models
{
    public partial class Restaurantes
    {
        public Restaurantes()
        {
            CalificacionNavigation = new HashSet<Calificacion>();
        }

        public int IdRestaurante { get; set; }
        public int? IdHorarios { get; set; }
        public string InformacionGeneral { get; set; }
        public byte[] Logo { get; set; }

        [NotMapped]
        public IFormFile[] ImagenLogo { get; set; }
        public byte[] ImagenItemDestacado { get; set; }

        [NotMapped]
        public IFormFile[] ImagenDestacada { get; set; }
        public int? Calificacion { get; set; }

        public virtual Horarios IdHorariosNavigation { get; set; }
        public virtual ICollection<Calificacion> CalificacionNavigation { get; set; }
    }
}
