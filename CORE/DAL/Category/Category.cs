using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace CORE.DAL;

public class Category :BaseEntity
{
    public string Name { get; set; }
    public ICollection<Product>? _Product { get; set; }
}
