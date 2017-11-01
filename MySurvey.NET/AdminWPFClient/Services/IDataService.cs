using System;

namespace AdminWPFClient.Services
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
