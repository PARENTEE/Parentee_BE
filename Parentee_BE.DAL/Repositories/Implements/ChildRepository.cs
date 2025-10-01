using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Entities;

namespace Parentee_BE.DAL.Data.Repositories;

public class ChildRepository : GenericRepository<ChildEntity>
{
    public ChildRepository(DbContext dbContext) : base(dbContext)
    {
    }
}