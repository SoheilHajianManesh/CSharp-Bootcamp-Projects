namespace Task2.Service;

public class EBookMapper : IMapper<DataTransferObjects.EBook,Model.Catalog.EBook>
{
    public List<Model.Catalog.EBook> Map(List<DataTransferObjects.EBook> sourceObjs)
    {
        return sourceObjs
            .Select(dto => new Model.Catalog.EBook(dto.Size,dto.ID,dto.Title,dto.Price,dto.ISBN))
            .ToList();
    }
}