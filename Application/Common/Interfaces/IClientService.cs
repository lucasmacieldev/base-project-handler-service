using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IClientService
    {
        //Task<Client> GetByIdAsync(Guid issuerId);
        Task<dynamic> GetByIdAsync(Guid issuerId);
    }
}
