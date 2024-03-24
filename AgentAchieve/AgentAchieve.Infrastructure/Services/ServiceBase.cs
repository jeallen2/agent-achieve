using AgentAchieve.Core.Common;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AgentAchieve.Infrastructure.Services;

/// <summary>
/// Base class for services that operate on entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IEntityPk
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<TEntity> _repository;
    protected readonly IMapper _mapper;
    protected readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBase{TEntity}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<TEntity>();
        _mapper = mapper;
        _logger = logger;
    }

    #region Entity-based methods

    /// <summary>
    /// Gets an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>The entity with the specified ID, or null if not found.</returns>
    public virtual async Task<TEntity?> GetByIdAsync(object id)
    {
        _logger.LogInformation($"Getting entity by ID: {id}");
        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Retrieves all entities. Optionally, includes related entities in the results.
    /// </summary>
    /// <param name="includes">A function to include related entities in the results. If null, no related entities are included.</param>
    /// <returns>An <see cref="IQueryable{TEntity}"/> representing the collection of entities.</returns>
    public virtual IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
    {
        return _repository.GetAll(includes);
    }

    /// <summary>
    /// Inserts a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    public virtual async Task InsertAsync(TEntity entity)
    {
        _logger.LogInformation("Inserting entity");
        await _repository.InsertAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        _logger.LogInformation("Updating entity");
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// Deletes an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    public virtual async Task DeleteAsync(object id)
    {
        _logger.LogInformation($"Deleting entity by ID: {id}");
        await _repository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    #endregion

    #region Methods for working with DTOs

    /// <summary>
    /// Gets a DTO by its ID asynchronously.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="id">The ID of the DTO.</param>
    /// <returns>The DTO with the specified ID, or null if not found.</returns>
    public async Task<TDto?> GetDtoByIdAsync<TDto>(object id) where TDto : class
    {
        _logger.LogInformation($"Getting DTO by ID: {id}");
        var entity = await GetByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    /// <summary>
    /// Gets all DTOs.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="includes">A function to include related entities in the results. If null, no related entities are included.</param>
    /// <returns>An <see cref="IQueryable{TDto}"/> representing all DTOs.</returns>
    public IQueryable<TDto> GetAllDto<TDto>(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null) where TDto : class
    {
        return GetAll(includes).ProjectTo<TDto>(_mapper.ConfigurationProvider);
    }
    /// <summary>
    /// Inserts a new DTO asynchronously.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="dto">The DTO to insert.</param>
    /// <returns>The inserted DTO.</returns>
    public async Task<TDto> InsertDtoAsync<TDto>(TDto dto) where TDto : class
    {
        _logger.LogInformation("Inserting DTO");
        var entity = _mapper.Map<TEntity>(dto);
        await InsertAsync(entity);
        return _mapper.Map<TDto>(entity);
    }

    /// <summary>
    /// Updates an existing DTO asynchronously.
    /// </summary>
    /// <typeparam name="TDto">The type of DTO.</typeparam>
    /// <param name="id">The ID of the DTO to update.</param>
    /// <param name="dto">The updated DTO.</param>
    /// <returns>The updated DTO.</returns>
    public async Task<TDto> UpdateDtoAsync<TDto>(object id, TDto dto) where TDto : class
    {
        _logger.LogInformation($"Updating DTO by ID: {id}");
        var entity = await _repository.GetByIdAsync(id) ?? throw new ArgumentException($"Entity with ID {id} not found.");
        _mapper.Map(dto, entity);
        await UpdateAsync(entity);
        return _mapper.Map<TDto>(entity);
    }

    #endregion
}