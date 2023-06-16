using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Data
{
    public class CodeChallengeContext : DbContext
    {
        public CodeChallengeContext(DbContextOptions<CodeChallengeContext> options)
            : base(options)
        { }
    }
}
