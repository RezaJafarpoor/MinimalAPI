﻿using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entities;
using MinimalAPI.Persistence;
using MinimalAPI.Repositories.Interfaces;

namespace MinimalAPI.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookRepository( ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CreateAsync(Book entity)
    {
        

        await _dbContext.Books.AddAsync(entity);
        
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Isbn == isbn);
        return book;
    }

    public async Task<bool> DeleteByIsbnAsync(string isbn)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Books.Where(b => b.Isbn == isbn).ExecuteDeleteAsync();
            await transaction.CommitAsync();
            return true;

        }
        catch (Exception e)
        {
           await transaction.RollbackAsync();
           return false;
        }

    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var books = await _dbContext.Books.AsNoTracking().ToListAsync();
        return books;
    }

    public Task<bool> UpdateAsync(Book entity)
    {
        throw new NotImplementedException();
    }
}