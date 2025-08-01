using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public interface ICrudRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(params object[] keyValues);
    TEntity? GetByDocNum(params object[] keyValues);

    void Create(TEntity item);
    void Update(TEntity item);
    void Delete(TEntity item);
}
