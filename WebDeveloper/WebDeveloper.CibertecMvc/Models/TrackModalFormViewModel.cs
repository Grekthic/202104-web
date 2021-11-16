using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDeveloper.Core.Entities;

namespace WebDeveloper.CibertecMvc.Models
{
    public class TrackModalFormViewModel
    {
        public string ModalTitle { get; set; }
        public Track Track { get; set; }
        public IEnumerable<SelectListItem> MediaTypeList { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
        public IEnumerable<SelectListItem> AlbumList { get; set; }
    }
}
