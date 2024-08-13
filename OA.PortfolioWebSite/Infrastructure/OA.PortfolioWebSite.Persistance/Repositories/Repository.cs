using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using OA.PortfolioWebSite.Application.Interfaces.Repositories;
using OA.PortfolioWebSite.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<Result<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return Result<IEnumerable<T>>.Success(entities);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<T>>.Error(ex.Message);
            }
        }

        public async Task<Result<T>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return Result<T>.NotFound();

                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result <T>.Error(ex.Message);
            }
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Error(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return Result.NotFound();

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
        public async Task<Result<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Error(ex.Message);
            }
        }
    }
}
