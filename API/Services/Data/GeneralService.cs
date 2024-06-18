using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Data;

public class GeneralService<TIRepository, TRequest, TResponse, TEntity> : IGeneralService<TRequest, TResponse>
    where TIRepository : IGeneralRepository<TEntity>
    where TEntity : class
{
    protected readonly IMapper _mapper;
    protected readonly TIRepository _repository;
    protected readonly ITransactionRepository _transactionRepository;

    public GeneralService(TIRepository repository, IMapper mapper, ITransactionRepository transactionRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<TResponse>?> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();
        var toDto = _mapper.Map<IEnumerable<TResponse>>(data);
        return toDto;
    }

    public async Task<TResponse?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        var toDto = _mapper.Map<TResponse>(entity);
        return toDto;
    }

    public virtual async Task CreateAsync(TRequest request)
    {
        var mapEntity = _mapper.Map<TEntity>(request);

        await _repository.CreateAsync(mapEntity);
        await _transactionRepository.SaveChangesAsync();
    }

    public virtual async Task<bool> UpdateAsync(Guid id, TRequest request)
    {
        var entity = await _repository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();

        if (entity is null) return false;
        entity = _mapper.Map(request, entity);
        //mapEntity.GetType().GetProperty("Id").SetValue(mapEntity, id);

        _repository.Update(entity);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        _transactionRepository.ChangeTrackerClear();

        if (entity is null) return false;

        _repository.Delete(entity);
        await _transactionRepository.SaveChangesAsync();

        return true;
    }

    public async Task CheckNullReference<TEntityRef>(Guid id, IGeneralRepository<TEntityRef> repository,
                                                     string propertyName) where TEntityRef : class
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null) throw new NullReferenceException($"'{propertyName}' not found.");
    }
}
