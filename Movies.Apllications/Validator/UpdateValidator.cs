using FluentValidation;
using Movies.Contracts.Requests;

namespace Movies.Applications.Validator
{
    public class UpdateValidator  : AbstractValidator<UpdateMovieRequste>
    {
        public UpdateValidator()
        {
            ValidateTitle();
            ValidateYearOfRelease();
            ValidateGenre();
        }

        private void ValidateTitle()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
        private void ValidateYearOfRelease()
        {
            RuleFor(x => x.YearOfRelease)
                .NotEmpty()
                .InclusiveBetween(1988, DateTime.UtcNow.Year);
            //این هم حد بالا را کنترل می‌کند هم پایین را.
        }

        private void ValidateGenre()
        {
            RuleFor(x => x.GenreIds)
                .NotEmpty();

        }
    }
}

