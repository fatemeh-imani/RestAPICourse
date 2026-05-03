using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Applications.Models;
using Movies.Applications.MovieRpositories;
using Movies.Contracts.Requests;

namespace Movies.API.Controllers
{
    [Route("api")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRpository _movieRepository;

        public MoviesController(IMovieRpository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        [HttpPost(MagicStrings.ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequste request)
        {
            var movie = request.MapToMovie();
            await _movieRepository.CreatAsync(movie);
            return CreatedAtAction(nameof(Get), new { idOrSlug = movie.Id }, movie);
        }

        [HttpGet(MagicStrings.ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug)
        {
            var movie =Guid.TryParse(idOrSlug, out var id)
                ? await _movieRepository.GetByIdAsync(id)
                :await _movieRepository.GetBySlugAsync(idOrSlug);

            if(movie is null)
            {
                return NotFound();
            }
            var response = movie.MapToResponce();
            return Ok();
        }

        [HttpGet(MagicStrings.ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieRepository.GetAllAsync();
            var moviesRespoce = movies.MapToResponce();
            return Ok(moviesRespoce);     
        }

        [HttpPut(MagicStrings.ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid id,
                                                [FromBody]UpdateMovieRequste request)
        {
            var movie = request.MapToMovie(id);
            var updated = await _movieRepository.UpdateAsync(movie);
            if(!updated)
            {
                return NotFound();
            }
            var response = movie.MapToResponce();
            return Ok(response);
        }

        [HttpDelete(MagicStrings.ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delete = await _movieRepository.DeleteByIdAsync(id);
            if(!delete)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
