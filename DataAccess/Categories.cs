using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Categories
{
    public int Id { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual ICollection<Activities> Activities { get; set; } = new List<Activities>();
}
