using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.WordSets.Queries.GetWordSetCategories;

public class GetWordSetCategoriesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetWordSetCategoriesQuery, GetWordSetCategoriesResult>
{
    public async Task<GetWordSetCategoriesResult> Handle(GetWordSetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.WordSetCategories
            .Include(wc => wc.WordSet)
            .Include(wc => wc.Category)
            .Where(c => c.WordSetId == query.WordSetId);
        var wordSetCategories = await baseQuery.ToListAsync(cancellationToken);
        var wordSetsDto = wordSetCategories.Adapt<List<WordSetCategoryDto>>();
        return new GetWordSetCategoriesResult(wordSetsDto);
    }
}