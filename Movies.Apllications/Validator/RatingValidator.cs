using FluentValidation;
using Movies.Applications.DataBaces.Models;
using Movies.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Validator
{
    public class RatingValidator : AbstractValidator<CreateRatingRequest>
    {
        public RatingValidator() 
        {
            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5);
            // کاربر فقط می‌تواند بین ۱ تا ۵ امتیاز بدهد.
        }
    }
}
