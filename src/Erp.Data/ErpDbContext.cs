using Microsoft.EntityFrameworkCore;

namespace Erp.Data;

public class ErpDbContext : DbContext
{
    public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
    {
    } 
}