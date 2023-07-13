using System.ComponentModel;

namespace Domain.Enumerators
{
    public enum MessagesEnumerable
    {
        [Description("Nome é obrigatório")]
        NameRequired = 1,
        [Description("Tipo é obrigatório")]
        TypeClientRequired,
        [Description("Usuário é obrigatório")]
        UserNameRequired,
        [Description("Senha é obrigatório")]
        PasswordRequired,
        [Description("Email é obrigatório")]
        EmailRequired,
        [Description("Cpf é obrigatório")]
        DocumentRequired,
        [Description("Data de Aniversário é obrigatório")]
        BirthDate,
        [Description("Documento Profissional  é obrigatório")]
        DocumentProfessional,
    }
}
