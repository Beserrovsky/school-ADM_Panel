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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Apenas números para seu telefone!")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Não se esqueça do DDD!")]
        public string Telefone { get; set; }

        [Required]
        public Endereco Endereco { get; set; }

        public bool IsCliente { get; set; }

        public bool IsFuncionario { get; set; }

    }

    public class Endereco 
    {
        [Required]
        [MaxLength(100)]
        public string Logradouro { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        [Remote("CheckState", "Agente", HttpMethod = "POST", ErrorMessage = "Insira um Estado válido!")]
        public string Estado { get; set; }

        [Required]
        public int Numero { get; set; }
    }
}