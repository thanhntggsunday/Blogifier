using Blogifier.Shared.Models;
using System.Collections.Generic;

namespace Blogifier.Shared
{
	public class PageListModel : TransactionalInformation
    {
      public IEnumerable<PostItem> Posts { get; set; }
      public Pager Pager { get; set; }
   }
}
