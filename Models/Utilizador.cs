using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Utilizador
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Freguesia { get; set; } = null!;

    public string CodPostal { get; set; } = null!;

    public string Nif { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<EmpregadosArmazem> EmpregadosArmazems { get; set; } = new List<EmpregadosArmazem>();

    public virtual ICollection<Encomendum> Encomenda { get; set; } = new List<Encomendum>();
}
