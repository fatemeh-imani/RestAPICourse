using Microsoft.AspNetCore.Mvc;

using Movies.Applications.Services;
using Movies.Contracts.Requests;

namespace Movies.Applications.Controllers
{
    
    public class MoviesController(IMovieService _movieService) : ControllerBase
    {
     
        [HttpPost(MagicStrings.ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequste request
                                                     ,CancellationToken token)
        //متدهایی که async هستند و کار I/O دارند توکن بگذار
        {
            var movie = await _movieService.CreateAsync(request , token);

            return CreatedAtRoute("GetMovie" , new { idOrSlug = movie.Id }, movie);
        }
        //CreatedAtRoute فقط برای Create (POST) استفاده می‌شود.

        //چون وقتی یک resource جدید ساخته می‌شود باید در header پاسخ Location بدهیم که آدرس آن resource چیست.
        

        [HttpGet(MagicStrings.ApiEndpoints.Movies.Get, Name = "GetMovie")]
        public async Task<IActionResult> GetAsync([FromRoute] string idOrSlug
                                                 , CancellationToken token)
        {
           

            var movie = await _movieService.GetAsync(idOrSlug , token);
           

            if(movie is null)
            {
                return NotFound();
            }
       
            return Ok(movie);
        }

        [HttpGet(MagicStrings.ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            var movies = await _movieService.GetAllAsync(token);
            
            return Ok(movies);     
        }

        [HttpPut(MagicStrings.ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,
                                                [FromBody]UpdateMovieRequste request
                                                 , CancellationToken token)
        {
            var updated = await _movieService.UpdateAsync(request, id, token);
            if(updated is null)  return NotFound();
           
            return Ok(updated);
        }

        [HttpDelete(MagicStrings.ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var delete = await _movieService.SoftDeleteAsync(id , token);
            if(!delete)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
