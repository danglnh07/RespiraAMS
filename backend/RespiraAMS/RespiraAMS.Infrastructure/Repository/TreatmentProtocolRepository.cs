using Microsoft.EntityFrameworkCore;
using RespiraAMS.Core.Models;
using RespiraAMS.Core.RepositoryContract;
using RespiraAMS.Infrastructure.Data;

namespace RespiraAMS.Infrastructure.Repository;

public class TreatmentProtocolRepository(AppDbContext context) : GenericRepository<TreatmentProtocol>(context), ITreatmentProtocolRepository
{
    private readonly AppDbContext _context = context;

    public async Task<TreatmentProtocol?> GetTreatmentProtocolByIdAsNoTrackingAsync(Guid id,
        Func<IQueryable<TreatmentProtocol>, IQueryable<TreatmentProtocol>>? query)
    {
        var queryable = _context.Set<TreatmentProtocol>().AsNoTracking().AsQueryable();
        if (query is not null)
        {
            queryable = query(queryable);
        }
        return await queryable.FirstOrDefaultAsync(t => t.Id == id);
    }
}