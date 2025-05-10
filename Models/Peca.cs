using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Peca
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public string NumSerie { get; set; } = null!;

    public decimal Preco { get; set; }

    public int Categoria { get; set; }

    public virtual CategoriaPeca CategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<ItemEncomendum> ItemEncomenda { get; set; } = new List<ItemEncomendum>();
}
