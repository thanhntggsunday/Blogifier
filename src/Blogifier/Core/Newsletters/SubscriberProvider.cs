using AutoMapper;
using Blogifier.Core.Data;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Newsletters;

public class SubscriberProvider(IMapper mapper, AppDbContext dbContext) : AppProvider<Subscriber, int>(dbContext)
{
  private readonly IMapper _mapper = mapper;

  public async Task<IEnumerable<SubscriberDto>> GetItemsAsync()
  {
    var query = _dbContext.Subscribers
     .AsNoTracking()
     .OrderByDescending(n => n.CreatedAt);
    return await _mapper.ProjectTo<SubscriberDto>(query).ToListAsync();
  }

  public async Task<int> ApplyAsync(SubscriberApplyDto input)
  {

    if (await _dbContext.Subscribers.AnyAsync(m => m.Email == input.Email))
      return 0;
    else
    {
      var data = _mapper.Map<Subscriber>(input);
      _dbContext.Subscribers.Add(data);
      await _dbContext.SaveChangesAsync();
      return 1;
    }
  }
}
