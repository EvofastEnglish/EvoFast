using EvoFast.Application.Dtos;
using EvoFast.Domain.Models;
using Mapster;

namespace EvoFast.Application.Mapper;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<WordSet, WordSetDto>
            .NewConfig();
    }
}