using Entities.Domain_Models;
using System.Threading.Tasks;


namespace Repository.Interfaces
{
    public interface IAdminRepository
    {
        Task CreateAdminAsync(Admin admin);
    }
}
