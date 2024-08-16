using Task2.Repository;

namespace Task2.Service;

public class MapperFactory
{
    public IMapper<TSource,TDest> GetMapper<TSource,TDest>()
    {
        if(typeof(TSource) == typeof(DataTransferObjects.EBook))
            return new EBookMapper() as IMapper<TSource,TDest>;
        else if(typeof(TSource) == typeof(DataTransferObjects.AudioBook))
            return new AudioBookMapper() as IMapper<TSource,TDest>;
        else if(typeof(TSource) == typeof(DataTransferObjects.PaperBook))
            return new PaperBookMapper() as IMapper<TSource,TDest>;
        else if(typeof(TSource) == typeof(DataTransferObjects.Order))
            return new OrderMapper() as IMapper<TSource,TDest>;
        else if(typeof(TSource) == typeof(DataTransferObjects.OrderItem))
            return new OrderItemMapper() as IMapper<TSource,TDest>;
        else
            throw new Exception("No mapper found for the given types");
    }
}