using ConsoleApp51.Interfaces;

namespace ConsoleApp51.Models;

internal class Repository<T> : IRepository<T>
{
    private List<T> _entities = new List<T>();
    public void Add(T entity)
    {
        _entities.Add(entity);
    }
    public T GetById(int id)
    {
        {
            dynamic? entity = _entities
                .FirstOrDefault(x =>
                    ((dynamic)x!).Id == id);

            if (entity == null)
                throw new KeyNotFoundException(
                    "Entity tapılmadı.");

            return (T)entity;
        }
    }
    public List<T> GetAlls()
    {
        return _entities.ToList();
    }
    public void Update(T entity)
    {
     
    }
    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }
}
