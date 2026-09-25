namespace ConsoleApp51.Interfaces;

internal interface IRepository<T>
{
    public void Add(T entity);
    public T GetById(int id);
    public List<T> GetAll();
    public void Update(T entity);
    public void Delete(T entity);

}
