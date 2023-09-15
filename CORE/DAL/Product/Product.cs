using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DAL;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public int Category_Id { get; set; }
    public Category? _Category { get; set; }
}
