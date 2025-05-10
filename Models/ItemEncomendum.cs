using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class ItemEncomendum
{
    public int IdEncomenda { get; set; }

    public int IdPeca { get; set; }

    public int Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public virtual Encomendum IdEncomendaNavigation { get; set; } = null!;

    public virtual Peca IdPecaNavigation { get; set; } = null!;
}
