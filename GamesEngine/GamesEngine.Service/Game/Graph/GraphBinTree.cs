namespace GamesEngine.Service.Game.Graph;

public interface IGraphBinTree<T1, T2>
{
    T2 Get(T1 key);
    bool ContainsKey(T1 key);
    void Add(T1 key, T2 value);
    void Remove(T1 key);
    void Update(T1 key, T2 value);
    List<T1> GetKeys();
    List<T2> GetValues();
}

public class GraphBinTree<T1, T2> : IGraphBinTree<T1, T2>
{
    SortedDictionary<T1, T2> tree = new SortedDictionary<T1, T2>();

    public T2 Get(T1 key)
    {
        return tree[key];
    }

    public bool ContainsKey(T1 key)
    {
        return tree.ContainsKey(key);
    }

    public void Add(T1 key, T2 value)
    {
        tree.Add(key, value);
    }

    public void Remove(T1 key)
    {
        tree.Remove(key);
    }

    public void Update(T1 key, T2 value)
    {
        Remove(key);
        Add(key, value);
    }

    public List<T1> GetKeys()
    {
        return tree.Keys.ToList();
    }

    public List<T2> GetValues()
    {
        return tree.Values.ToList();
    }
}