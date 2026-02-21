using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Shared.Models
{
    class ListViewModel<T> : TransactionalInformation where T : class, new()
    {
        public ListViewModel()
        {
            Data = new List<T>();
        }

        public List<T> Data { get; set; }
    }
}
