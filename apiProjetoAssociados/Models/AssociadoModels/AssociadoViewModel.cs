namespace apiProjetoAssociados.Models.AssociadoModels
{
    public class AssociadoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        //public DateTime DtNascimento { get; set; }
        public List<CheckBoxViewModel>? Empresas { get; set; }
    }
}
