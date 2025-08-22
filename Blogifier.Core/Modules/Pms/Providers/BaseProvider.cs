using Blogifier.Core.AdoNet.SQLServer;

namespace Blogifier.Core.Modules.Pms.Providers
{
    public class BaseProvider
    {
        private Blogifier.Core.AdoNet.SQLServer.DataAccess _dbContext;

        public BaseProvider()
        {
            _dbContext = new DataAccess();
        }

        public DataAccess DbContext
        {
            get
            {
                if (_dbContext == null || _dbContext.Disposed) _dbContext = new DataAccess();

                return _dbContext;
            }
        }
    }
}