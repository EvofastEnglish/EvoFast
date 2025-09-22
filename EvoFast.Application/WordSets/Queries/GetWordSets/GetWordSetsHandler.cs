using BuildingBlocks.Pagination;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.WordSets.Queries.GetWordSets;

public class GetWordSetsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetWordSetsQuery, GetWordSetsResult>
{
    public async Task<GetWordSetsResult> Handle(GetWordSetsQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        
        var pageIndex = request.PaginationRequest.PageIndex;
        var pageSize = request.PaginationRequest.PageSize;
        
        var query = dbContext.WordSets.AsQueryable();

        if (user != null)
        {
            if (!string.IsNullOrEmpty(user.Industry))
            {
                query = query.Where(ws => ws.Industry == user.Industry);
            }

            if (!string.IsNullOrEmpty(user.JobRole))
            {
                query = query.Where(ws => ws.JobRole == user.JobRole);
            }
        }
        
        var totalCount = await query.LongCountAsync(cancellationToken);
        
        var wordSets = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(ws => ws.NumberId)
            .ProjectToType<WordSetDto>()
            .ToListAsync(cancellationToken);
        
        return new GetWordSetsResult(
            new PaginatedResult<WordSetDto>(pageIndex, pageSize, totalCount, wordSets));
    }
}