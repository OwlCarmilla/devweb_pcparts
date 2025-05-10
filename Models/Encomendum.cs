using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Encomendum
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public DateTime DataEncomenda { get; set; }

    public string Estado { get; set; } = null!;

    public decimal Total { get; set; }

    public virtual Utilizador IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<ItemEncomendum> ItemEncomenda { get; set; } = new List<ItemEncomendum>();
}
