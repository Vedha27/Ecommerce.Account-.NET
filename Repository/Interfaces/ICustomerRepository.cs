using Entities.Domain_Models;
using System.Threading.Tasks;


namespace Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomerAsync(Customer customer);
    }
}
