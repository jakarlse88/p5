using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface IMappingService<TSource, TDestination>
    {
        Task<Listing> Map(TSource source);
        Task<Listing> Map(TSource source, TDestination destination);
    }
}