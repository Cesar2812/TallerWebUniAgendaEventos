using System;
using System.Collections.Generic;

namespace DataAccess;

public partial class Activities
{
    public int Id { get; set; }

    public string NameActivitie { get; set; } = null!;

    public DateOnly StarDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int CategorieId { get; set; }

    public string Notes { get; set; } = null!;

    public virtual Categories Categorie { get; set; } = null!;
}
