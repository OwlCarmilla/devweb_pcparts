using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Utilizador
{
    public string NomeU { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Freguesia { get; set; } = null!;

    public string CodPostal { get; set; } = null!;

    public int Nif { get; set; }

    public int IdU { get; set; }

    public virtual Empregado? Empregado { get; set; }

    public virtual ICollection<Armazem> Armazems { get; set; } = new List<Armazem>();
}
