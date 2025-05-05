using EvoFast.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.ReviewSessions.Queries.GetTotalReviewSessions;

public class GetTotalReviewSessionHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetTotalReviewSessionQuery, GetTotalReviewSessionResult>
{
    public async Task<GetTotalReviewSessionResult> Handle(GetTotalReviewSessionQuery query, CancellationToken cancellationToken)
    {
        var total = await dbContext.ReviewSessions.CountAsync(r => r.NextReviewDate <= DateTime.UtcNow ,cancellationToken: cancellationToken);
        return new GetTotalReviewSessionResult(total);
    }
}