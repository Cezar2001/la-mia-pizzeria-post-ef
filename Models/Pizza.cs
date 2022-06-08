using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    /*
    public class SuperValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validation)
        {
            string fieldValue = (string)value;
            if (fieldValue == null || fieldValue.Trim().IndexOf(" ") == -1)
            {
                return new ValidationResult("Il campo deve contenere Pizza e il tipo, es. Pizza Margherita, Pizza Diavola ecc..");
            }
            return ValidationResult.Success;
        }
    }
    */

    public class Pizza
    {
        [Required(ErrorMessage = "L'ID della Pizza è obbligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il Nome della Pizza è obbligatorio")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "La descrizione della Pizza è obbligatorio")]
        public string Descrizione { get; set; }
        
        public string? sFoto { get; set; }
        
        public double Prezzo { get; set; }

        public IFormFile Foto { get; set; }
       
        public Pizza()
        {

        }

        public Pizza(int id, string nome, string descrizione, string foto, double prezzo)
        {
            Id = id;
            Nome = nome;
            Descrizione = descrizione;
            sFoto = foto;
            Prezzo = prezzo;
        }

    }
}
