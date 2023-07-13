using System.ComponentModel;

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
