using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using DogsCareSystem.Repository.Implementations;
using DogsCareSystem.Services.Abstractions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;

namespace DogsCareSystem.Services.Implementations;

public class BreedService(
    IHttpClientFactory httpClientFactory,
    IBreedRepository breedRepository
    ) 
    : IBreedService
{
    public async Task Parse()
    {
         var DbBreeds = await breedRepository.GetAll();

        
        var client = httpClientFactory.CreateClient();  
        
        string url = "https://www.royalcanin.com/ru/dogs/breeds/breed-library";

        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);

        var nodes = doc.DocumentNode.SelectNodes("//a [contains(@class, 'rc-card__link')]");

        var breeds = new List<Breed>();
        
        foreach (var link in nodes)
        {
            var ib = link.InnerText.Replace("\n", "").Replace("\r", "").Split(" ").Where(p=> p.Length >= 3);

            var n = string.Join("", ib);
            
            var t1 = DbBreeds.Select(x => x.Name.Replace("\r", "").Replace(" ", ""));
            
            if (t1.Contains(n))
            {
                continue;
            }
            
            var att = link.Attributes[1].Value;
            
            HtmlDocument dogP = web.Load(att);

            var dogPic = dogP.DocumentNode.SelectSingleNode("//picture [contains(@class, 'rc-full-width')]");
            if (dogPic == null || breeds.Any(x => x.Name.Replace(" ","") == link.InnerText.Replace("\n","").Replace("\r","").Replace(" ","")))
            {
                continue;
            }
            var at = dogPic.InnerHtml.Split(",")[1].Split(";")[0];
            var response = await client.GetAsync(at);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            
            var infoNodes = dogP.DocumentNode.SelectNodes("//div [contains(@class, 'rc-column')]");

            var about = infoNodes[21].InnerText.Split("\n");
            var peculiarities = infoNodes[22].InnerText.Replace(" ","").Replace("\r","").Split("\n");

            var a = link.InnerText.Replace("\n", "").Replace("\r", "").Split(" ").Where(p=> p.Length >= 3);
            
            var breed = new Breed
            {
                Name = string.Join(" ", a),
                About = about[2].Substring(12) + about[3],
                Country = peculiarities.SkipWhile(item => item != "Страна").Skip(1).FirstOrDefault(),
                Size = peculiarities.SkipWhile(item => item != "Размернаягруппа").Skip(1).FirstOrDefault(),
                AvgLifeTime = peculiarities.SkipWhile(item => item != "Средняяпродолжительностьжизни").Skip(1).FirstOrDefault(),
                Image = bytes
            };
            breeds.Add(breed);
        }
        breedRepository.AddRange(breeds);
        breedRepository.Save();
    }
}