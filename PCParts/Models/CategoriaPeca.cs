using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class CategoriaPeca
{
    public string NomeC { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public int IdT { get; set; }

    public virtual ICollection<Peca> Pecas { get; set; } = new List<Peca>();
}
