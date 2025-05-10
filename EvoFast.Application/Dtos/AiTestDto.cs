namespace EvoFast.Application.Dtos;

public record AiTestDto(
    Guid Id,
    string Title,
    string Description,
    string DescriptionFinish,
    List<AiTestSectionDto> AiTestSections
    );