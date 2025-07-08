
using interfaces.DataBase;

namespace infrastructure.DataBase
{

    public class MyUnitOfWork(DemoControlContext context) : BaseUnitOfWork<DemoControlContext>(context), IMyUnitOfWork
    {
       
    }
}
