using CORE.DAL;

namespace CORE.DTO;

public class ProductOutput
{
    public string? Name { get; set; }
    public int Category_Id { get; set; }
    public CategoryInput? _Category { get; set; }
}
