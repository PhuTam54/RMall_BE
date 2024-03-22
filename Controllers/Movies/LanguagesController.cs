using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies.Languages;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguagesController(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllLanguage()
        {

            var languages = _mapper.Map<List<LanguageDto>>(_languageRepository.GetAllLanguage());

            return Ok(languages);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Language))]
        [ProducesResponseType(400)]
        public IActionResult GetLanguageById(int id)
        {
            if (!_languageRepository.LanguageExist(id))
                return NotFound();

            var language = _mapper.Map<LanguageDto>(_languageRepository.GetLanguageById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(language);
        }

        /// <summary>
        /// Create Language
        /// </summary>
        /// <param name="LanguageCreate"></param>
        /// <returns></returns>
        /// <remarks>
        /// POST/Language
        /// {
        /// "name": "Quan",
        /// "email": "quan123@gmail.com",
        /// "phone": "0987654321",
        /// "message": "Nice!"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLanguage([FromBody] LanguageDto languageCreate)
        {
            if (languageCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var languageMap = _mapper.Map<Language>(languageCreate);


            if (!_languageRepository.CreateLanguage(languageMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLanguage(int id, [FromBody] LanguageDto updatedLanguage)
        {
            if (!_languageRepository.LanguageExist(id))
                return NotFound();
            if (updatedLanguage == null)
                return BadRequest(ModelState);

            if (id != updatedLanguage.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var languageMap = _mapper.Map<Language>(updatedLanguage);
            if (!_languageRepository.UpdateLanguage(languageMap))
            {
                ModelState.AddModelError("", "Something went wrong updating language");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLanguage(int id)
        {
            if (!_languageRepository.LanguageExist(id))
            {
                return NotFound();
            }

            var languageToDelete = _languageRepository.GetLanguageById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_languageRepository.DeleteLanguage(languageToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting language");
            }

            return NoContent();
        }
    }
}
