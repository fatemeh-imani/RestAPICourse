using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.DataBaces.Models
{
    public class Rating :BaseModel
    {
   
        public int Score { get; set; }

        public Guid MovieId { get; init; }
        public Movie Movie { get; set; } = default!;

        public string UserId { get; set; } = default!;
        // UserId از نوع string است چون IdentityUser.Id = string است.
        //علامت ! یعنی: «من مطمئنم این بعداً مقدار می‌گیرد، warning نده».
        public ApplicationUser User { get; set; } = default!;
    }
}
