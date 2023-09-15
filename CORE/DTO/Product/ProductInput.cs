using CORE.DAL;

namespace CORE.DTO;

public class ProductInput
{
    public int Id { get; set; } = 0;
    public string? Name { get; set; }
    public int Category_Id { get; set; }
}
