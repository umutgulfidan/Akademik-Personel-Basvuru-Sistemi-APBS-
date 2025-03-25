using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntitiyFramework
{
    public class EfBildirimDal : EfRepositoryBase<Context,Bildirim>,IBildirimDal
    {
    }
}
