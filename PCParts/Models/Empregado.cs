using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Empregado
{
    public int IdE { get; set; }

    public string ArmazemL { get; set; } = null!;

    public virtual Armazem ArmazemLNavigation { get; set; } = null!;

    public virtual Utilizador IdENavigation { get; set; } = null!;
}
