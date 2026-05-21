using Microsoft.EntityFrameworkCore;
using RespiraAMS.Domain.Models;
using RespiraAMS.Domain.RepositoryContracts;
using RespiraAMS.Infrastructure.Data;

namespace RespiraAMS.Infrastructure.Repositories;

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