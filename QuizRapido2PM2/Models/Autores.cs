using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace QuizRapido2PM2.Models
{
    [Table("Autores")]
    public class Autores
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNac { get; set; }

        [Unique]
        public string Telefono { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Nacionalidad { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;

    }
}
