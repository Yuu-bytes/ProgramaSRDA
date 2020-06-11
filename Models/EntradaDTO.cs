using System;

namespace ProgramaSRDAMVC.Models
{
    public class EntradaDTO
    {
        public Guid Id {get; set;}
        public DateTime Data { get; set; }
        public string Hora { get; set; }
        public string Comentario { get; set; }

    }
}
