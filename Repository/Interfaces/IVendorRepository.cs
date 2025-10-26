using Entities.Domain_Models;
using System.Threading.Tasks;


namespace Repository.Interfaces
{
    public interface IVendorRepository
    {
        Task CreateVendorAsync(Vendor vendor);
    }
}
