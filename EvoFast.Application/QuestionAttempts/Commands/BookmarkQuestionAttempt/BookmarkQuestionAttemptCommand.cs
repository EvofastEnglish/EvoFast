using EvoFast.Application.Dtos;

namespace EvoFast.Application.QuestionAttempts.Commands.BookmarkQuestionAttempt;

public record BookmarkQuestionAttemptCommand(BookmarkQuestionAttemptRequest QuestionAttempt) : ICommand<BookmarkQuestionAttemptResult>;
public record BookmarkQuestionAttemptRequest(Guid Id, bool IsBookmarked);
public record BookmarkQuestionAttemptResult(QuestionAttemptDto QuestionAttempt);