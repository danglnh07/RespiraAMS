using RespiraAMS.Domain.Models;

namespace RespiraAMS.Domain.RepositoryContracts;

public interface ICriterionRepository : IGenericRepository<Criterion>
{
    Task<int> CreateCriteriaAsync(List<Criterion> criteria);    
}