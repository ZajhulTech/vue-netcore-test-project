using interfaces.infrastructure;

namespace interfaces.DataBase
{
  
    public interface IMyUnitOfWork : EntityFrameworkCore.UnitOfWork.Interfaces.IUnitOfWork, IRepositoryBulk
    {
    }
}
