using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class JobService : GeneralService<IJobRepository, JobRequestDto, JobResponseDto, Job>, IJobService
{
    public JobService(IJobRepository repository, IMapper mapper, ITransactionRepository transactionRepository) :
        base(repository, mapper, transactionRepository) { }
}
