using RMall_BE.Models.Shops;
using RMall_BE.Models.User;

namespace RMall_BE.Interfaces.ShopInterfaces
{
    public interface IContractRepository
    {
        ICollection<Contract> GetAllContract();
        Contract GetContractById(int id);
        ICollection<Contract> GetContractByShop(int ShopId);
        ICollection<Contract> GetContractByTenant(int TenantId);
        bool CreateContract(Contract Contract);
        bool UpdateContract(Contract Contract);
        bool DeleteContract(Contract Contract);
        bool ContractExist(int id);
        bool Save();
    }
}
