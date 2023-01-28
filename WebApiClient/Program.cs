
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using WebApiClient;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".Net Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

    foreach (var repository in repositories ?? Enumerable.Empty<Repository>())
    {
        Console.Write(repository.name);
    }
}