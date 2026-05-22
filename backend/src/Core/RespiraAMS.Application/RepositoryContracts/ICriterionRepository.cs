using System.Collections.Generic;
using System.Threading.Tasks;
using RespiraAMS.Domain.Models;

namespace RespiraAMS.Application.RepositoryContracts;

public interface ICriterionRepository : IGenericRepository<Criterion>
{
    Task<int> CreateCriteriaAsync(List<Criterion> criteria);    
}