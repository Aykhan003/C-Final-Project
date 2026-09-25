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
    public List<T> GetAll()
    {
        return _entities.ToList();
    }
    public void Update(T entity)
    {
        dynamic? existingEntity = _entities
                .FirstOrDefault(x =>
                    ((dynamic)x!).Id == ((dynamic)entity!).Id);
        if (existingEntity == null)
            throw new KeyNotFoundException(
                "Entity tapılmadı.");
        int index = _entities.IndexOf(existingEntity);
        _entities[index] = entity;
    }
    public void Delete(T entity)
    {
        _entities.Remove(entity);
    } 
}
