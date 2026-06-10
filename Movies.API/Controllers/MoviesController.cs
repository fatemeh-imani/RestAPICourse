using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Auth;
using Movies.Applications.Services;
using Movies.API.MagicStrings;
using Movies.Contracts.Requests;
using Movies.Contracts.Responces;

namespace Movies.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiEndpoints.Movies.Base)]
    public class MoviesController(IMovieService _movieService) : ControllerBase
    {
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPost(ApiEndpoints.Movies.Create)]
        [ProducesResponseType(typeof(MovieResponce), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateAsync([FromBody] CreateMovieRequste request
                                                     ,CancellationToken token)
        //متدهایی که async هستند و کار I/O دارند توکن بگذار
        {
            var movie = await _movieService.CreateAsync(request , token);

            return CreatedAtRoute("GetMovie" , new { idOrSlug = movie.Id }, movie);
        }
        //CreatedAtRoute فقط برای Create (POST) استفاده می‌شود.

        //چون وقتی یک resource جدید ساخته می‌شود باید در header پاسخ Location بدهیم که آدرس آن resource چیست.
        

        [HttpGet(ApiEndpoints.Movies.Get, Name = "GetMovie")]
        [ProducesResponseType(typeof(MovieResponce), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] string idOrSlug
                                                 , CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            if (userId is null) return Unauthorized("User is not authenticated.");

            var movie = await _movieService.GetAsync(idOrSlug ,userId, token);
           

            if(movie is null)
            {
                return NotFound();
            }
       
            return Ok(movie);
        }

        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpGet(ApiEndpoints.Movies.GetAll)]
        [ProducesResponseType(typeof(List<MovieResponce>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(
                       [FromQuery] GetAllMovieRequest request
                       ,CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            if (userId is null) return Unauthorized("User is not authenticated.");

            var movies = await _movieService.GetAllAsync(request , userId , token);
            
            return Ok(movies);     
        }

        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.Movies.Update)]
        [ProducesResponseType(typeof(MovieResponce), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id,
                                                [FromBody]UpdateMovieRequste request
                                                 , CancellationToken token)
        {
            var userId = HttpContext.GetUserId();
            if (userId is null) return Unauthorized("User is not authenticated.");

            var updated = await _movieService.UpdateAsync(request, id,userId , token);
            if(updated is null)  return NotFound();
           
            return Ok(updated);
        }

        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
