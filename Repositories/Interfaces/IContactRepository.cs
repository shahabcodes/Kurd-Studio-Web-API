using KurdStudio.Api.Models.DTOs;

namespace KurdStudio.Api.Repositories.Interfaces;

public interface IContactRepository
{
    Task<int> CreateSubmissionAsync(ContactRequest request);
}
