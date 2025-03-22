namespace EvoFast.Application.Dtos;

public record WordSetCategoryDto(
    Guid Id,
    WordSetDto WordSet,
    CategoryDto Category);