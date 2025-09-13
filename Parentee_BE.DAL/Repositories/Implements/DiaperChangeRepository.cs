using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Entities;
using Parentee_BE.DAL.Data.Repositories;

public class DiaperChangeRepository : GenericRepository<DiaperChangeEntity>
{
    public DiaperChangeRepository(DbContext dbContext) : base(dbContext)
    {
        
    }
}