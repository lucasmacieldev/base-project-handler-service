using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enumerators
{
    public enum ClientType
    {
        [Description("Administrador da Conta")]
        Adm = 0,
        [Description("Usuario apenas Leitor")]
        Visitor = 1
    }
}
