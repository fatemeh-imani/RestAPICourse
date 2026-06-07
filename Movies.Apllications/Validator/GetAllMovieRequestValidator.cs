using FluentValidation;
using Movies.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.Validator
{
    public class GetAllMovieRequestValidator : AbstractValidator<GetAllMovieRequest>
    {
        private readonly string[] _allowedSortBy = { "title", "yearofrelease" };

        public GetAllMovieRequestValidator()
        {
            // 1. ولیدیشن مربوط به Pagination
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید حداقل ۱ باشد.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("تعداد آیتم در هر صفحه باید بین ۱ تا ۵۰ باشد.");

            // 2. ولیدیشن مربوط به فیلترها
            RuleFor(x => x.YearOfRelease)
                .InclusiveBetween(1900, DateTime.UtcNow.Year)
                .When(x => x.YearOfRelease.HasValue) // فقط در صورتی که مقدار داشته باشد بررسی شود
                .WithMessage("سال انتشار باید معتبر باشد.");

            // 3. ولیدیشن مربوط به Sorting
            RuleFor(x => x.SortBy)
                .Must(x => _allowedSortBy.Contains(x!.ToLower()))
                .When(x => !string.IsNullOrEmpty(x.SortBy))
                .WithMessage("فیلد مرتب‌سازی نامعتبر است.");

            RuleFor(x => x.SortOrder)
                .Must(x => x!.ToLower() == "asc" || x!.ToLower() == "desc")
                .When(x => !string.IsNullOrEmpty(x.SortOrder))
                .WithMessage("ترتیب مرتب‌سازی فقط می‌تواند 'asc' یا 'desc' باشد.");
        }
    }
}
