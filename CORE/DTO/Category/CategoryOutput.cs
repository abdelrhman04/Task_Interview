using CORE.DAL;

namespace CORE.DTO;

public class CategoryOutput
{
    public string Name { get; set; }
    public ICollection<ProductInput>? _Product { get; set; }
}
