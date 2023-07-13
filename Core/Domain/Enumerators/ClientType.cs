using System.ComponentModel;

namespace Domain.Enumerators
{
    public enum UserType
    {
        [Description("Administrador da Conta")]
        Adm = 0,
        [Description("Médico")]
        Professional = 1,
        [Description("Paciente")]
        Patient = 2
    }
}
