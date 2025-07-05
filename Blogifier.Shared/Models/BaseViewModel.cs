using Blogifier.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Shared.Modules.Pms.Models
{
    public class BaseViewModel<T> : TransactionalInformation where T : class, new()
    {
        public T Data { get; set; }
    }
}
