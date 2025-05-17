using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EvoFast.Application.AiTestSectionQuestions.Commands.CompleteAiTestSectionQuestion;

public record CompleteAiTestSectionQuestionCommand(CompleteAiTestSectionQuestionRequest CompleteAiTestSectionQuestionRequest) : ICommand<CompleteAiTestSectionQuestionResult>;

public record CompleteAiTestSectionQuestionRequest(Guid AiTestSessionId, Guid AiTestSectionId, Guid QuestionId, IFormFile AudioFile, string Language);
public record CompleteAiTestSectionQuestionResult(List<AiTestChatMessageDto> ChatMessageDtos);