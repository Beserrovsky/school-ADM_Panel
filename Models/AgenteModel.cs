using FelipeB_App3BI.DB;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FelipeB_App3BI.Models
{
    public class AgenteModel : Model
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

        public override bool Validate()
        {
            return IsCpfValid(CPF) && Endereco.IsEstadoValid(Endereco.Estado);
        }

        public static bool IsCpfValid(string cpf)
        {

            if (cpf == null || cpf.Length != 11) return false; // Verifica se Array está mal formatado

            int[] cpf_arr = new int[11];
            for (int i = 0; i < cpf.Length; i++)
                if (!int.TryParse(cpf[i].ToString(), out cpf_arr[i])) return false; // Converte de String para int[] e retorna falso caso não seja um número

            int sum = 0;

            for (int i = 10; i >= 2; i--)
            { // Multiplica os 9 primeiros números pela seqência decrescente de 10 a 2
                sum += cpf_arr[10 - i] * i;
            }

            double mod = (sum * 10) % 11;

            if (mod != cpf_arr[9]) return false; // Verfica se o resto da soma anterior quando multipliacada por 10 e dividida por 11 é igual ao primeiro digito da confirmação

            sum = 0;

            for (int i = 11; i >= 2; i--)
            { // Multiplica os 10 primeiros números pela seqência decrescente de 11 a 2
                sum += cpf_arr[11 - i] * i;
            }

            mod = (sum * 10) % 11;

            if (mod != cpf_arr[10]) return false; // Repete a etapa de verificação, mas para o segundo digito

            return true;
        }
    }

    public class Endereco 
    {
        [Required]
        [MaxLength(2)]
        [Remote("CheckState", "Agente", HttpMethod = "POST", ErrorMessage = "Insira um Estado válido!")]
        public string Estado { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(100)]
        public string Logradouro { get; set; }

        [Required]
        public int Numero { get; set; }

        public static bool IsEstadoValid(string estado)
        {
            return new AgenteDAO().EstadoExists(estado);
        }
    }
}