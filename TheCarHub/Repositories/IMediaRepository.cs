using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IMediaRepository
    {
        void Add(Media media);
    }
}