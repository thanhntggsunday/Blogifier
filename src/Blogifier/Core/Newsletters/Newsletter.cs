using Blogifier.Core.Data;
using Blogifier.Core.Posts;
using System;

namespace Blogifier.Core.Newsletters;

public class Newsletter : AppEntity<int>
{
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int PostId { get; set; }
  public bool Success { get; set; }
  public Post Post { get; set; } = default!;
}
