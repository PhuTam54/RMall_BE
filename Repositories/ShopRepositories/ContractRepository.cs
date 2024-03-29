using RMall_BE.Data;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Models.User;

namespace RMall_BE.Repositories.ShopRepositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly RMallContext _context;

        public ContractRepository(RMallContext context)
        {
            _context = context;
        }
        public ICollection<Contract> GetAllContract()
        {
            var contracts = _context.Contracts.ToList();
            return contracts;
        }

        public Contract GetContractById(int id)
        {
            return _context.Contracts.FirstOrDefault(c => c.Id == id);
        }
        public ICollection<Contract> GetContractByShop(int ShopId)
        {
            return _context.Contracts.Where(s => s.Shop_Id == ShopId).ToList();
        }
        public ICollection<Contract> GetContractByTenant(int TenantId)
        {
            return _context.Contracts.Where(s => s.Tenant_Id == TenantId).ToList();
        }

        public bool CreateContract(Contract Contract)
        {
            _context.Add(Contract);
            return Save();
        }

        public bool UpdateContract(Contract Contract)
        {
            _context.Update(Contract);
            return Save();
        }

        public bool DeleteContract(Contract Contract)
        {
            _context.Remove(Contract);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool ContractExist(int id)
        {
            return _context.Contracts.Any(c => c.Id == id);
        }
    }
}
