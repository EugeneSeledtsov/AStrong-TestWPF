namespace BackendClientLib.Interfaces
{
    using BackendClientLib.Models;

    public interface IBackendClient
    {
        Task<List<CardResponseModel>> GetCardsAsync(int skip, int take, CancellationToken cancellationToken);

        Task<CardResponseModel> GetCardAsync(Guid id, CancellationToken cancellationToken);

        Task<CardResponseModel> CreateCardAsync(string description, string filePath, CancellationToken cancellationToken);
    }
}
