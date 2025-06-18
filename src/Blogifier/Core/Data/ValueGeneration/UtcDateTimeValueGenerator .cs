using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.ValueGeneration;

public class DateTimetValueGenerator : ValueGenerator<DateTime>
{
  public override bool GeneratesTemporaryValues => false;

  public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;

  public override ValueTask<DateTime> NextAsync(EntityEntry entry, CancellationToken cancellationToken = default) =>
    ValueTask.FromResult(DateTime.UtcNow);
}
