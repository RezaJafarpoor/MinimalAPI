using MinimalAPI.Entities;

namespace MinimalAPI.Repositories.Interfaces;

public interface IBookRepository
{
    Task<bool> CreateAsync(Book entity);
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<bool> DeleteByIsbnAsync(string isbn);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<bool> UpdateAsync(Book entity);
}