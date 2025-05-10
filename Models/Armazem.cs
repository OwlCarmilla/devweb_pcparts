using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class Armazem
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Freguesia { get; set; } = null!;

    public virtual ICollection<EmpregadosArmazem> EmpregadosArmazems { get; set; } = new List<EmpregadosArmazem>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
