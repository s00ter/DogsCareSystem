using Microsoft.AspNetCore.Mvc;

namespace DogsCareSystem.Services.Abstractions;

public interface IBreedService
{
    Task Parse();
}