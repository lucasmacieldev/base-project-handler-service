using System.ComponentModel;

namespace Domain.Enumerators
{
    public enum MessagesEnumerableOrderTag
    {
        [Description("Nome é obrigatório")]
        NameRequired = 1,
        [Description("Tipo de Cliente é obrigatório")]
        TypeClientRequired = 2,
    }
}
