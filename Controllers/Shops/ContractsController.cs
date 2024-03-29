using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Helpers;
using RMall_BE.Identity;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Interfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Models.User;

namespace RMall_BE.Controllers.Shops
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository<Tenant> _tenantRepository;

        public ContractsController(IContractRepository contractRepository, IShopRepository shopRepository, IUserRepository<Tenant> tenantRepository, IMapper mapper)
        {
            _contractRepository = contractRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        public IActionResult GetAllContract()
        {
            var categories = _mapper.Map<List<ContractDto>>(_contractRepository.GetAllContract());
            return Ok(categories);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetContract(int id)
        {
            if (!_contractRepository.ContractExist(id))
                return NotFound();

            var contractMap = _mapper.Map<ContractDto>(_contractRepository.GetContractById(id));
            return Ok(contractMap);

        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Tenant")]
        [HttpPost]
        public IActionResult CreateContract([FromQuery] int shopId, [FromQuery] int tenantId, [FromBody] ContractDto contractCreate)
        {
            if (contractCreate == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contractMap = _mapper.Map<Contract>(contractCreate);
            contractMap.Shop = _shopRepository.GetShopById(shopId);
            contractMap.Tenant = _tenantRepository.GetUserById(tenantId);
            if (!_contractRepository.CreateContract(contractMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Create Contract Successfully!");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateContract(int id, [FromBody] ContractDto contractUpdate)
        {
            if (!_contractRepository.ContractExist(id))
                return NotFound();

            if (id != contractUpdate.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contractMap = _mapper.Map<Contract>(contractUpdate);
            if (!_contractRepository.UpdateContract(contractMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Contract!");
                return StatusCode(500, ModelState);
            }

            return Ok("Update Contract Successfully");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteContract(int id)
        {
            if (!_contractRepository.ContractExist(id))
                return NotFound();

            var contractToDelete = _contractRepository.GetContractById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_contractRepository.DeleteContract(contractToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting contract");
            }
            return Ok("Delete Contract Successfully!");
        }
    }
}
