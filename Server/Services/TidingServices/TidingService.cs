using Data.Entities;
using Data.Repositories;
using Server.Exceptions;

namespace Server.Services.TidingServices;

public class TidingService : ITidingService
{
    private readonly IRepository<Tiding> _tidingsDb;

    public TidingService(IRepository<Tiding> tidingsDb) => _tidingsDb = tidingsDb;
    
    public async Task<Tiding?> GetLastTidingsAsync() 
        => (await GetTidingsAsync()).MaxBy(x => x) 
           ?? throw new ServiceOperationNullException("Похоже, последних новостей нет!");
    
    public async Task<ICollection<Tiding>> GetTidingsAsync()
        => await _tidingsDb.GetTableAsync();

    public async Task<Tiding> UpdateTidingAsync(string title, string text, DateTimeOffset tidingKey)
    {
        var currentTiding = (await _tidingsDb.GetTableAsync())
            .FirstOrDefault(x => x.PublicationDate == tidingKey) 
            ?? throw new ServiceOperationNullException("Ой-ой, похоже, " +
                                                   "вы пытаетесь редактировать несуществующую статью!" +
                                                   " Попробуйте позже!");

        currentTiding.Title = title;
        currentTiding.Text = text;

        await _tidingsDb.UpdateAsync(currentTiding);

        return currentTiding;
    }

    public async Task<Tiding> GetTidingByPublicationDateAsync(DateTimeOffset publicationDate)
    {
        var tidings = await GetTidingsAsync();

        return tidings.FirstOrDefault(x => x.PublicationDate == publicationDate)
               ?? throw new ServiceOperationNullException("Ой-ой, похоже, статьи в такое время не выходило");
    }

    public async Task<Tiding> CreateTidingAsync(string title, string text)
    {
        var tiding = new Tiding
        {
            Title = title,
            Text = text,
            PublicationDate = DateTimeOffset.Now
        };
        await _tidingsDb.AddAsync(tiding);
        return tiding;
    }

    public async Task DeleteTidingAsync(Tiding tiding) 
        => await _tidingsDb.RemoveAsync(tiding);
}