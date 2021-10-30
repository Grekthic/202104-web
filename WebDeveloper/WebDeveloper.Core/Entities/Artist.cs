using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebDeveloper.Core.Entities
{
    [Table("Artist")]
    public class Artist
    {
        //[Column("artist_id")] -> Para especificar el nombre de la columna en BD a mapear
        public int ArtistId { get; set; } // artist_id
        public string Name { get; set; } // name
    }
}
