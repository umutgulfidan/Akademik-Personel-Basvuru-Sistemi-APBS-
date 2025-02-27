namespace Core.Utilities.Results
{
    public class DataResult<TData> : Result, IDataResult<TData>
    {
        public TData Data { get; set; }

        public DataResult(TData data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }
        public DataResult(TData data, bool isSucces) : base(isSucces)
        {
            Data = data;
        }
    }
}
