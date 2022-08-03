namespace SearchFunction.DataLayer
{
    public interface IDataRepository
    {

        IDataRepository Clone();
        IList<T> QueryEntity<T>(string filter, string sort);
    }
}
