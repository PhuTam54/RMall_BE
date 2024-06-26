﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Helpers;
using RMall_BE.Identity;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Shops
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategory());
            return Ok(categories);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExist(id))
                return NotFound();

            var categoryMap = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryById(id));
            return Ok(categoryMap);

        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="categoryCreate"></param>
        /// <remarks>
        /// "name": "Clothes",
        /// </remarks>
        /// <returns></returns>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            categoryMap.Slug = CreateSlug.Init_Slug(categoryMap.Name);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Create Category Successfully!");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryUpdate)
        {
            if (!_categoryRepository.CategoryExist(id))
                return NotFound();

            if (id != categoryUpdate.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryUpdate);
            categoryMap.Slug = CreateSlug.Init_Slug(categoryMap.Name);
            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Category!");
                return StatusCode(500, ModelState);
            }

            return Ok("Update Category Successfully");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteCategory(int id)
        {
            if (!_categoryRepository.CategoryExist(id))
                return NotFound();

            var categoryToDelete = _categoryRepository.GetCategoryById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }
            return Ok("Delete Category Successfully!");
        }
    }
}
