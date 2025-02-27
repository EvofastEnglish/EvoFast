using BuildingBlocks.Exceptions;
using EvoFast.Application.Data;
using EvoFast.Application.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EvoFast.Application.QuestionAttempts.Commands.BookmarkQuestionAttempt;

public class BookmarkQuestionAttemptHandler(IApplicationDbContext dbContext)
    : ICommandHandler<BookmarkQuestionAttemptCommand, BookmarkQuestionAttemptResult>
{
    public async Task<BookmarkQuestionAttemptResult> Handle(BookmarkQuestionAttemptCommand command, CancellationToken cancellationToken)
    {
        var questionAttempt = dbContext.QuestionAttempts
            .Include(qt => qt.Question)
            .ThenInclude(q => q.Answers)
            .FirstOrDefault(qt => qt.Id == command.QuestionAttempt.Id);
        if (questionAttempt == null)
        {
            throw new NotFoundException("QuestionAttempt", command.QuestionAttempt.Id);
        }
        questionAttempt.IsBookmarked = command.QuestionAttempt.IsBookmarked;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new BookmarkQuestionAttemptResult(questionAttempt.Adapt<QuestionAttemptDto>());    
    }
}