using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0._1.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
