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
        => (await _tidingsDb.GetTableAsync()).ToList();

    public async Task<Tiding> UpdateTidingAsync(string title, string text, DateTime tidingKey)
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

    public async Task<Tiding> GetTidingByPublicationDateAsync(DateTime publicationDate)
    {
        var tidings = await GetTidingsAsync();

        return tidings.FirstOrDefault(x => x.PublicationDate == publicationDate)
               ?? throw new ServiceOperationNullException("Ой-ой, похоже, статьи в такое время не выходило");
    }

    public async Task CreateTidingAsync(string title, string text)
        => await _tidingsDb.AddAsync(new Tiding
        {
            Title = title,
            Text = text,
            PublicationDate = DateTime.Now
        });
    
    public async Task DeleteTidingAsync(Tiding tiding) 
        => await _tidingsDb.RemoveAsync(tiding);
}