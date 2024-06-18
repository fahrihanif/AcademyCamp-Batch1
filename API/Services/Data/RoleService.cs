using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class RoleService : GeneralService<IRoleRepository, RoleRequestDto, RoleResponseDto, Role>, IRoleService
{
    public RoleService(IRoleRepository repository, IMapper mapper, ITransactionRepository transactionRepository) :
        base(repository, mapper, transactionRepository) { }
}
