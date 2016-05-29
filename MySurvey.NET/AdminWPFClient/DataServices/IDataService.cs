using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminWPFClient.DataServices
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
