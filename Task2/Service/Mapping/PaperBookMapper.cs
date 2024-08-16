namespace Task2.Service;

public class PaperBookMapper : IMapper<DataTransferObjects.PaperBook,Model.Catalog.PaperBook>
{ 
        public List<Model.Catalog.PaperBook> Map(List<DataTransferObjects.PaperBook> sourceObjs)
        {
                return sourceObjs
                    .Select(dto => new Model.Catalog.PaperBook(dto.TotalPages,dto.ID,dto.Title,dto.Price,dto.ISBN))
                    .ToList();
        }
}