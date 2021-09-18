using System.ComponentModel.DataAnnotations;

namespace FelipeB_App3BI.Models
{
    public class ProdutoModel : Model
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public override bool Validate()
        {
            // TODO: VALIDATE MODEL 
            return true;
        }
    }
}