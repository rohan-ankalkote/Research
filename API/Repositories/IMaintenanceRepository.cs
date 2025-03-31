using API.Common.Models;

namespace API.Repositories
{
    public interface IMaintenanceRepository<TModel> where TModel : ModelBase, new()
    {
        TModel Insert(TModel model);
        TModel Update(TModel model);
        TModel Delete(int key);
        TModel Get(int key);
    }
}