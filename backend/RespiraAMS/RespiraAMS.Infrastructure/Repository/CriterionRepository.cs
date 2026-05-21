using RespiraAMS.Core.Models;
using RespiraAMS.Core.RepositoryContract;
using RespiraAMS.Infrastructure.Data;

namespace RespiraAMS.Infrastructure.Repository;

public class CriterionRepository(AppDbContext context) : GenericRepository<Criterion>(context), ICriterionRepository
{
    private readonly AppDbContext _context = context;

    public async Task<int> CreateCriteriaAsync(List<Criterion> criteria)
    {
        await _context.Set<Criterion>().AddRangeAsync(criteria);
        return await _context.SaveChangesAsync();
    }
}