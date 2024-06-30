using DogsCareSystem.Models;

namespace DogsCareSystem.Services.Abstractions;

public interface ISearchService
{
    Task<List<Breed>> Search(string str);
}