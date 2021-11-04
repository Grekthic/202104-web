using System;
using System.Collections.Generic;
using System.Text;

namespace WebDeveloper.Core.ViewModels.Reports
{
    public class TopCancionVendida
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public int Quantity { get; set; }
    }
}
