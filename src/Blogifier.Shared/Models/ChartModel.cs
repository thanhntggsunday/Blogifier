using Blogifier.Shared.Models;
using System.Collections.Generic;

namespace Blogifier.Shared
{
	public class BarChartModel : TransactionalInformation
    {
		public ICollection<string> Labels { get; set; }
		public ICollection<int> Data { get; set; }
	}
}
