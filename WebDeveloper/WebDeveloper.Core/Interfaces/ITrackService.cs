using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebDeveloper.Core.ViewModels;

namespace WebDeveloper.Core.Interfaces
{
    public interface ITrackService
    {
        Task<IEnumerable<TrackViewModel>> ObtenerListaTracks();
    }
}
