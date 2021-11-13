using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.ViewModels;

namespace WebDeveloper.Core.Services
{
    public class TrackService : ITrackService
    {
        private readonly IChinookContext _chinookContext;
        public TrackService(IChinookContext chinookContext)
        {
            _chinookContext = chinookContext;
        }
        public async Task<IEnumerable<TrackViewModel>> ObtenerListaTracks()
        {
            return await _chinookContext.Tracks
                            .Select(t => new TrackViewModel
                            {
                                TrackId = t.TrackId,
                                Name = t.Name,
                                AlbumName = t.Album.Title,
                                ArtistName = t.Album.Artist.Name,
                            })
                            .ToListAsync();
        }
    }
}
