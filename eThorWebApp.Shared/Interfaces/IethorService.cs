using eThorWebApp.Shared.Models;
using System.Threading.Tasks;

namespace eThorWebApp.Shared.Interfaces
{
    public interface IethorService
    {
        Task<eThorTestEntity[]> GetAll();
        Task<eThorTestEntity> Get(int Id);
        Task Add(eThorTestEntity entity);
        Task Update(eThorTestEntity entity);
        Task Delete(int Id);
    }
}
