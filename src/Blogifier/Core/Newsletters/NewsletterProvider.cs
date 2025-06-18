using AutoMapper;
using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Newsletters;

public class NewsletterProvider(AppDbContext dbContext, IMapper mapper) : AppProvider<Newsletter, int>(dbContext)
{
  private readonly IMapper _mapper = mapper;

  public async Task<IEnumerable<NewsletterDto>> GetItemsAsync()
  {
    var query = _dbContext.Newsletters
       .AsNoTracking()
       .Include(n => n.Post)
       .OrderByDescending(n => n.CreatedAt);
    return await _mapper.ProjectTo<NewsletterDto>(query).ToListAsync();
  }

  public async Task<NewsletterDto?> FirstOrDefaultByPostIdAsync(int postId)
  {
    var query = _dbContext.Newsletters
       .Where(m => m.PostId == postId);
    return await _mapper.ProjectTo<NewsletterDto>(query).FirstOrDefaultAsync();
  }

  public async Task AddAsync(int postId, bool success)
  {
    var entry = new Newsletter
    {
      PostId = postId,
      Success = success,
    };
    await AddAsync(entry);
  }

  public async Task UpdateAsync(int id, bool success)
  {
    await _dbContext.Newsletters
      .Where(m => m.Id == id)
      .ExecuteUpdateAsync(setters =>
        setters.SetProperty(b => b.Success, success));
  }
}
