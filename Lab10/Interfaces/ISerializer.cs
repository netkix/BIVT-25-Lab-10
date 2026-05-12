using Lab9.Purple;

namespace Lab10.Interfaces;

public interface ISerializer<T> where T : Lab9.Purple.Purple
{
    T Deserialize();
    void Serialize(T obj);
}