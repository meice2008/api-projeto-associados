namespace apiProjetoAssociados.Models.EmpresaModels
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public List<CheckBoxViewModel>? Associados { get; set; }
    }
}
