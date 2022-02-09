using SCG.CAD.ETAX.DAL.CONTROLLER;
using SCG.CAD.ETAX.DAL.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.DAL.INTERFACE
{
    public abstract class DatabaseAction<T> 
    {
        public abstract OutputOnDbModel Search(T dataItem);
        public abstract OutputOnDbModel Insert(T dataItem);
        public abstract OutputOnDbModel Update(T dataItem);
        public abstract OutputOnDbModel Delete(T dataItem);

    }
}
