using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IFindexScoreService
    {
        IDataResult<int> GetCustomerFindexScore(int customerId);
    }
}
