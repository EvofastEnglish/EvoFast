using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.Mapper;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<WordSet, WordSetDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.WordSetName, src => src.WordSetName.Value);
    }
}