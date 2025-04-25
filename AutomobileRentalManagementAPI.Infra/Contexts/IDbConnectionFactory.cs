using System.Data;

namespace AutomobileRentalManagementAPI.Infra.Contexts
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetNewConnection();
    }
}