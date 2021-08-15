using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FelipeB_App3BI.Models
{
    public class AgenteModel
    {
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [Remote("CheckCpf", "Agente", HttpMethod = "POST")]
        public string CPF { get; set; }

        [Phone]
        public string Telefone { get; set; }

        [Required]
        public Endereco Endereco { get; set; }

        public bool IsCliente { get; set; }

        public bool IsFuncionario { get; set; }

    }

    public class Endereco 
    {
        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public int Numero { get; set; }
    }
}