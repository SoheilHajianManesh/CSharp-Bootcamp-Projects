namespace Task2.Service;

public interface IMapper<TSource,TDest>
{
    public List<TDest> Map(List<TSource> sourceObjs);
}