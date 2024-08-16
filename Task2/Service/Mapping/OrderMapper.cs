namespace Task2.Service;

public class OrderMapper : IMapper<DataTransferObjects.Order,Model.Ordering.Order>
{
    public List<Model.Ordering.Order> Map(List<DataTransferObjects.Order> sourceObjs)
    {
        return sourceObjs
            .Select(dto => new Model.Ordering.Order(dto.ID,dto.DateTime)).
            ToList();       
    }
}