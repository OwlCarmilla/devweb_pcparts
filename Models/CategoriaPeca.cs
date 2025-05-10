using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class CategoriaPeca
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Peca> Pecas { get; set; } = new List<Peca>();
}
