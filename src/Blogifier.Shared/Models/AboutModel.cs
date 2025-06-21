using Blogifier.Shared.Models;

namespace Blogifier.Shared
{
	public class AboutModel : TransactionalInformation
    {
		public string Version { get; set; }
		public string OperatingSystem { get; set; }
        public string DatabaseProvider { get; set; }
	}
}
