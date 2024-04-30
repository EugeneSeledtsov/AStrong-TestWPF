namespace BackendClientLib.Services
{
    using BackendClientLib.Interfaces;
    using BackendClientLib.Models;
    using System.Text.Json;

    public class BackendClient() : IBackendClient
    {
        private static readonly string baseUrl = "https://localhost:7190/api";
        public async Task<CardResponseModel> CreateCardAsync(string description, string filePath, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            MultipartFormDataContent form = new()
            {
                { new StringContent(description), "description" }
            };

            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath, cancellationToken));
            form.Add(fileContent, "fileContent", Path.GetFileName(filePath));

            HttpResponseMessage response = await httpClient.PostAsync($"{baseUrl}/Card", form, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken) ?? throw new ArgumentException("Empty response");

            return JsonSerializer.Deserialize<CardResponseModel>(responseJson) ?? throw new ArgumentException("Couldn't deserialize model");
        }

        public async Task<CardResponseModel> GetCardAsync(Guid id, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{baseUrl}/Card/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken) ?? throw new ArgumentException("Empty response");

            return JsonSerializer.Deserialize<CardResponseModel>(responseJson) ?? throw new ArgumentException("Couldn't deserialize model");
        }

        public async Task<List<CardResponseModel>> GetCardsAsync(int skip, int take, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            var response = httpClient.GetAsync($"{baseUrl}/Card?skip={skip}&take={take}", cancellationToken).Result;
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken) ?? throw new ArgumentException("Empty response");

            var resultCollection = JsonSerializer.Deserialize<List<CardResponseModel>>(responseJson);
            resultCollection?.ForEach(t => t.ImageUrl = $"{baseUrl}/Card/{t.Id}/image");
            return resultCollection ?? throw new ArgumentException("Couldn't deserialize model");
        }
    }
}
