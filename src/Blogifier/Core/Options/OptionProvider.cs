using Blogifier.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Options;

public class OptionProvider(
  ILogger<OptionProvider> logger,
  IDistributedCache distributedCache,
  AppDbContext dbContext)
{
  private readonly ILogger _logger = logger;
  private readonly AppDbContext _dbContext = dbContext;

  public async Task<bool> AnyKeyAsync(string key) => await _dbContext.Options.AnyAsync(m => m.Key == key);

  public async Task<string?> GetByValueAsync(string key) =>
    await _dbContext.Options
      .AsNoTracking()
      .Where(m => m.Key == key)
      .Select(m => m.Value)
      .FirstOrDefaultAsync();

  public async Task SetValue(string key, string value)
  {
    var option = await _dbContext.Options
      .Where(m => m.Key == key)
      .FirstOrDefaultAsync();
    if (option == null)
    {
      _dbContext.Options.Add(new OptionInfo { Key = key, Value = value });
    }
    else
    {
      option.Value = value;
    }
    await _dbContext.SaveChangesAsync();
  }
}
