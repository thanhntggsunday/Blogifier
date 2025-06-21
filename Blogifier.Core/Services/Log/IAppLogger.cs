using System;
using System.Collections.Generic;
using System.Text;

namespace Blogifier.Core.Services.Log
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex = null);
    }

}
