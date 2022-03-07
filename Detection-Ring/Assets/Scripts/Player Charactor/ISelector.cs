public interface ISelector<T>
{
    public void Check();
    public T GetSelection();
}