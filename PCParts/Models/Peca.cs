using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Peca
{
    public string NomeP { get; set; } = null!;

    public string DescricaoP { get; set; } = null!;

    public int NumSerie { get; set; }

    public decimal Preco { get; set; }

    public int Categoria { get; set; }

    public int IdP { get; set; }

    public virtual CategoriaPeca CategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
