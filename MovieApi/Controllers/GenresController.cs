using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Iservices;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _iGenreService;

        public GenresController(IGenreService IGenreService)
        {
            _iGenreService = IGenreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Genres = await _iGenreService.GetAllAsync();
            return Ok(Genres);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetGenreByIdAsync(int Id)
        {
            var Genre = await _iGenreService.GetGenreByIdAsync(Id);
            return Ok(Genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = _iGenreService.CreateAsync(dto);

            return Ok(genre);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAsync(int Id,[FromBody] GenreDto model)
        {
            var genre = await _iGenreService.GetGenreByIdAsync(Id);

            if (genre is null)
                return NotFound($"No genre was found with id: {Id}");

            genre.Name = model.Name;
            _iGenreService.UpdateAsync(genre);

            return Ok(genre);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var genre = await _iGenreService.GetGenreByIdAsync(Id);

            if (genre is null)
                return NotFound($"No genre was found with id: {Id}");

            _iGenreService.DeleteAsync(genre);

            return Ok(genre);
        }

    }
}
