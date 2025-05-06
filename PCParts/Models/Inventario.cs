using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Inventario
{
    public int? Qtd { get; set; }

    public int Peca { get; set; }

    public string Armazem { get; set; } = null!;

    public virtual Armazem ArmazemNavigation { get; set; } = null!;

    public virtual Peca PecaNavigation { get; set; } = null!;
}
