
using Api.Models.DataBase;
using interfaces.DataBase;


namespace infrastructure.DataBase
{

    public class MyUnitOfWork(JvfcontrolContext context) : BaseUnitOfWork<JvfcontrolContext>(context), IMyUnitOfWork
    {
       
    }
}
