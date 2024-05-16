using System.Data;

namespace MinimalApiTutorial.IService
{
    public interface IDbConnectionFactory
    {
        public IDbConnection GetConnection();
    }
}
