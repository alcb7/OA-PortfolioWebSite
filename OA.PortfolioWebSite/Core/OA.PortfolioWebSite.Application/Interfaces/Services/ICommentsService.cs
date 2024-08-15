using Ardalis.Result;
using OA.PortfolioWebSite.Application.DTOs;
using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.PortfolioWebSite.Application.Interfaces.Services
{
    public interface ICommentsService
    {
        Task<Result<IEnumerable<Comments>>> GetAllCommentsAsync();
        Task<Result<Comments>> GetCommentsByIdAsync(int id);
        Task<Result<Comments>> AddCommentsAsync(CommentsCreateDto dto);
        Task<Result<Comments>> UpdateCommentsAsync(CommentsUpdateDto dto);
        Task<Result> DeleteCommentsAsync(int id);
    }
}
