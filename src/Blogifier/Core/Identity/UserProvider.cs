using AutoMapper;
using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Identity;

public class UserProvider(IMapper mapper, AppDbContext dbContext)
{
  private readonly IMapper _mapper = mapper;
  private readonly AppDbContext _dbContext = dbContext;

  public async Task<UserInfo> FindAsync(int id)
  {
    var query = _dbContext.Users
      .AsNoTracking()
      .Where(m => m.Id == id);
    return await query.FirstAsync();
  }

  public async Task<UserDto> FirstByIdAsync(int id)
  {
    var query = _dbContext.Users
      .AsNoTracking()
      .Where(m => m.Id == id);
    return await _mapper.ProjectTo<UserDto>(query).FirstAsync();
  }

  public async Task<IEnumerable<UserInfoDto>> GetAsync()
  {
    var query = _dbContext.Users
      .AsNoTracking();
    return await _mapper.ProjectTo<UserInfoDto>(query).ToListAsync();
  }

  public async Task<UserInfoDto?> GetAsync(int id)
  {
    var query = _dbContext.Users
      .AsNoTracking()
      .Where(m => m.Id == id);
    return await _mapper.ProjectTo<UserInfoDto>(query).FirstOrDefaultAsync();
  }
}
