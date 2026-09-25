namespace ConsoleApp51.Collections;

internal class CustomCollection<T>
{
    private T[] _items;
    public int Count { get; private set; }
    public CustomCollection(int capacity = 4)
    {
        _items = new T[capacity];
        Count = 0;
    }
    public void Add(T item)
    {
        if (Count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }
        _items[Count++] = item;
    }
    public void Remove(T item)
    {
        int index = Array.IndexOf(_items, item);
        if (index >= 0)
        {
            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _items[--Count] = default!;
        }
    }
    public T Get (int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }
        return _items[index];
    }
    public bool Contains(T item)
    {
        return Array.IndexOf(_items, item, 0, Count) != -1;
    }
    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        Count = 0;
    }
}
