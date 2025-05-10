using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Inventario
{
    public int IdPeca { get; set; }

    public int IdArmazem { get; set; }

    public int Quantidade { get; set; }

    public DateTime? UltimaAtualizacao { get; set; }

    public virtual Armazem IdArmazemNavigation { get; set; } = null!;

    public virtual Peca IdPecaNavigation { get; set; } = null!;
}
