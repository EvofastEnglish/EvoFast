
using EvoFast.Application.Dtos;

namespace EvoFast.Application.WordSets.Queries.GetWordSetCategories;

public record GetWordSetCategoriesQuery(Guid WordSetId) : IQuery<GetWordSetCategoriesResult>;

public record GetWordSetCategoriesResult(List<WordSetCategoryDto> WordSetCategoryDto);