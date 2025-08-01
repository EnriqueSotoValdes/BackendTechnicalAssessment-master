namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public interface ICrudRepositoryExtension<TEntity> : ICrudRepository<TEntity>
{
    TEntity? GetByDocNum(params object[] keyValues);
}
