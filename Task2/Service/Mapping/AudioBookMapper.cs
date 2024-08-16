using Model.Catalog;

namespace Task2.Service;

public class AudioBookMapper : IMapper<DataTransferObjects.AudioBook,Model.Catalog.AudioBook>
{
    public List<Model.Catalog.AudioBook> Map(List<DataTransferObjects.AudioBook> sourceObjs)
    {
        return sourceObjs
            .Select(dto => new Model.Catalog.AudioBook(dto.TotalMinutes, dto.ID, dto.Title, dto.Price, dto.ISBN))
            .ToList();
    }
}