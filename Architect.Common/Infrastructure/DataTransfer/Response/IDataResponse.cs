namespace Architect.Common.Infrastructure.DataTransfer.Response
{
    public interface IDataResponse<T> : IStatusResponse
    {
        T Data { get; set; }
    }
}
