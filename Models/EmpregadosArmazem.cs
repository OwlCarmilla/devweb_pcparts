using System;
using System.Collections.Generic;

namespace PCParts.Models;

public partial class EmpregadosArmazem
{
    public int IdEmpregado { get; set; }

    public int IdArmazem { get; set; }

    public DateOnly DataInicio { get; set; }

    public string? Cargo { get; set; }

    public virtual Armazem IdArmazemNavigation { get; set; } = null!;

    public virtual Utilizador IdEmpregadoNavigation { get; set; } = null!;
}
