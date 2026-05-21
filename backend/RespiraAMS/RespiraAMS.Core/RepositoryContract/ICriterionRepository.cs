using RespiraAMS.Core.Models;

namespace RespiraAMS.Core.RepositoryContract;

public interface ICriterionRepository : IGenericRepository<Criterion>
{
    Task<int> CreateCriteriaAsync(List<Criterion> criteria);    
}