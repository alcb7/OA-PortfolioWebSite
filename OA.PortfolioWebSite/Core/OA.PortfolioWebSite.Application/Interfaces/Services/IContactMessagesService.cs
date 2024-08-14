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
    public interface IContactMessagesService 
    {
        Task<Result<IEnumerable<ContactMessages>>> GetAllContactMessagesAsync();
        Task<Result<ContactMessages>> GetContactMessagesByIdAsync(int id);
        Task<Result<ContactMessages>> AddContactMessagesAsync(ContactMessagesCreateDto dto);
        Task<Result<ContactMessages>> UpdateContactMessagesAsync(ContactMessagesUpdateDto dto);
        Task<Result> DeleteContactMessagesAsync(int id);
    }
}

