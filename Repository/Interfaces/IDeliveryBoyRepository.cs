using Entities.Domain_Models;
using System.Threading.Tasks;


namespace Repository.Interfaces
{
    public interface IDeliveryBoyRepository
    {
        Task CreateDeliveryBoyAsync(DeliveryBoy deliveryBoy);
    }
}
