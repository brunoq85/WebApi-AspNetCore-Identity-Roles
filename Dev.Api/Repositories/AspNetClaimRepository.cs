using Dev.Api.Data;
using Dev.Api.Entities;
using Dev.Api.Interfaces;

namespace Dev.Api.Repositories
{
    public class AspNetClaimRepository : RepositoryBase<AspNetClaim>, IAspNetClaimRepository
    {
        public AspNetClaimRepository(DataContext _context):base (_context)
        {

        }
    }
}
