using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Auth;
using Movies.API.MagicStrings;
using Movies.Applications.Services;
using Movies.Contracts.Requests;

namespace Movies.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiEndpoints.Movies.Base)]
    public class RatingController(IRatingService _ratingService) : ControllerBase
    {
      [Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.Movies.Rate)]
        public async Task<IActionResult> RateMovieAsync(
              [FromRoute] Guid id
            , [FromBody] CreateRatingRequest ratingRequest 
            , CancellationToken token = default)
        {
            var userId = HttpContext.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

              await  _ratingService.RateMovieAsync(id,userId, ratingRequest, token);

            return Ok();
        }

        [HttpDelete(ApiEndpoints.Movies.DeleteRating)]
        public async Task<IActionResult> DeleteRatingAsync(
                         [FromRoute] Guid id 
                         ,CancellationToken token=default)
        {
            var userId = HttpContext.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

          await  _ratingService.DeleteRatingAsync(id,userId,token);
            return Ok();
        }

        [HttpGet(ApiEndpoints.Ratings.GetUserRatings)]
        public async Task<IActionResult> GetAllRatingAsync( CancellationToken token=default)
        {
            var userId = HttpContext.GetUserId(); 
            if (string.IsNullOrWhiteSpace(userId)) 
                return Unauthorized();

           var moviesRating =  await _ratingService.GetAllRatingAsync(userId,token);
            return Ok(moviesRating);
        }
    }
}
