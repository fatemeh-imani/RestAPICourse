using Microsoft.AspNetCore.Mvc;
using Movies.Applications.Mapping;
using Movies.Applications.DataBaces.Models;
using Movies.Applications.MovieRepositories;
using Movies.Applications.Services;
using Movies.Contracts.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Movies.Applications.Controllers
{
    
    public class MoviesController(IMovieService _movieService) : ControllerBase
    {
     
        [HttpPost(MagicStrings.ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequste request)
        {
         var movie = await _movieService.CreateAsync(request);

            return CreatedAtRoute("GetMovie" , new { idOrSlug = movie.Id }, movie);
        }
        //CreatedAtRoute فقط برای Create (POST) استفاده می‌شود.

        //چون وقتی یک resource جدید ساخته می‌شود باید در header پاسخ Location بدهیم که آدرس آن resource چیست.
        

        [HttpGet(MagicStrings.ApiEndpoints.Movies.Get, Name = "GetMovie")]
        public async Task<IActionResult> GetAsync([FromRoute] string idOrSlug)
        {
           

            var movie = await _movieService.GetAsync(idOrSlug);
           

            if(movie is null)
            {
                return NotFound();
            }
       
            return Ok(movie);
        }

        [HttpGet(MagicStrings.ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieService.GetAllAsync();
            
            return Ok(movies);     
        }

        [HttpPut(MagicStrings.ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,
                                                [FromBody]UpdateMovieRequste request)
        {
            var updated = await _movieService.UpdateAsync(request, id);
            if(updated is null)  return NotFound();
           
            return Ok(updated);
        }

        [HttpDelete(MagicStrings.ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delete = await _movieService.SoftDeleteAsync(id);
            if(!delete)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
