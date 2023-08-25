using Data.Entities;
using Server.Exceptions;

namespace Server.Services.TidingServices;

public interface ITidingService
{
    /// <summary>
    /// Returns last published tiding. Throw NullReferenceException if tidings do not exist.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    Task<Tiding?> GetLastTidingsAsync();
    
    /// <summary>
    /// Returns all tidings from system. Throw NullReferenceException if tidings do not exist.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ServiceOperationNullException"></exception>
    Task<ICollection<Tiding>> GetTidingsAsync();
    
    /// <summary>
    /// Updates tiding by data. If data is the same, nothing.
    /// Throws NullReferenceException if tiding does not exist.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ServiceOperationNullException"></exception>
    Task<Tiding> UpdateTidingAsync(string title, string text, DateTimeOffset tidingKey);

    /// <summary>
    /// Returns tiding with date from parameter. Throws NullReferenceException if it is not exist;
    /// </summary>
    /// <param name="publicationDate"></param>
    /// <returns></returns>
    /// <exception cref="ServiceOperationNullException"></exception>
    Task<Tiding> GetTidingByPublicationDateAsync(DateTimeOffset publicationDate);
    
    /// <summary>
    /// Creating article and added it in system. 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<Tiding> CreateTidingAsync(string title, string text);
    
    /// <summary>
    /// Deleting tiding forever. 
    /// </summary>
    /// <param name="tiding"></param>
    /// <returns></returns>
    Task DeleteTidingAsync(Tiding tiding);
}