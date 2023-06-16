using CodeChallenge.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Data
{
    public class CodeChallengeContext : DbContext
    {
        public CodeChallengeContext(DbContextOptions<CodeChallengeContext> options)
            : base(options)
        { }
            public DbSet<Customer> Customers { get; set; }
    }
}
