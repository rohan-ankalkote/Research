using System.Collections.Generic;
using System.Linq;
using API.Common.Models;

namespace API.Repositories
{
    public class MaintenanceRepository<TModel> : IMaintenanceRepository<TModel>
        where TModel : ModelBase, new()
    {
        private readonly List<TModel> _models = new List<TModel>();

        public virtual TModel Insert(TModel model)
        {
            model.Id = _models.Count == 0 ? 1 : _models.Select(c => c.Id).Max() + 1;
            _models.Add(model);
            return model;
        }

        public virtual TModel Update(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel Delete(int key)
        {
            var model = Get(key);

            if(model != null)
            {
                _models.Remove(model);
            }

            return model;
        }

        public virtual TModel Get(int key)
        {
            return _models.FirstOrDefault(m => m.Id == key);
        }
    }
}