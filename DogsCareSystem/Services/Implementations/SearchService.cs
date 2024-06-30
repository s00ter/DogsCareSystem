using System.Data;
using System.Diagnostics;
using System.Text;
using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using DogsCareSystem.Services.Abstractions;
using Microsoft.Data.SqlClient;

namespace DogsCareSystem.Services.Implementations;

public class SearchService(
    IBreedRepository breedRepository) 
    : ISearchService
{
    public async Task<List<Breed>> Search(string str)
    {
        /*var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        string sqlExpression = $"SELECT KEY_TBL.RANK,  BreedId \nFROM Breeds art    \n     INNER JOIN  \n     FREETEXTTABLE(Breeds, (*),  N'{str}') AS KEY_TBL  \n     ON KEY_TBL.[key] =art.BreedId\nORDER BY KEY_TBL.RANK DESC  ";

        
        using (SqlConnection connection = new SqlConnection(connectionString))
        { 
            await connection.OpenAsync();
 
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            var reader = await command.ExecuteReaderAsync();
            
            var list = new List<Breed>();
            var dbBreeds = await breedRepository.GetAll();
            
            if(reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    list.Add(dbBreeds.FirstOrDefault(x => x.BreedId == int.Parse(reader.GetValue(1).ToString())));

                }
            }
            reader.Close();
            
            return list;
        }*/
        
        var p = new Process();
        var list = new List<Breed>();
        var dbBreeds = await breedRepository.GetAll();
        
        p.StartInfo = new ProcessStartInfo
        { 
            UseShellExecute = false,
            /*FileName = @"C:\\testProject3\\dist\\main.exe",*/
            FileName = @"C:\\pythonProject2\\dist\\main.exe",
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
            StandardInputEncoding = Encoding.GetEncoding("utf-8"),
        };
        p.Start();

        using (StreamWriter writer = p.StandardInput)
        {
            writer.Write(str);
        }
        
        var output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();

        var st = output.Split("\n");
        string a;
        try
        {
            a = st[st.Length - 2];
        }
        catch (Exception e)
        {
            return list;
        }
        var cleanedString = a.Substring(1, a.Length - 3);
        var c = cleanedString.Split(" ");
        int w, f=0;
        try
        {
            w = int.Parse(c[0]);
        }
        catch (Exception e)
        {
            return list;
        }
        
        for (int i = 4; i < w+5 && i < st.Length ; i++)
        {
            var d = st[i].Split(" ");
            try
            {
                int.Parse(d[0]);
            }
            catch (Exception e)
            {
                continue;
            }
            for (int j = 1; j < d.Length; j++)
            {
                try
                {
                    f = int.Parse(d[j]);
                    break;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            
            list.Add(dbBreeds.FirstOrDefault(x => x.BreedId == f));
        }
        return list;
    }
}