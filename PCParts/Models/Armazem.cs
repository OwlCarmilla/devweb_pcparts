using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Armazem
{
    public string Nome { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Freguesia { get; set; } = null!;

    public string IdA { get; set; } = null!;

    public virtual ICollection<Empregado> Empregados { get; set; } = new List<Empregado>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<Utilizador> Trabalhadors { get; set; } = new List<Utilizador>();
}
