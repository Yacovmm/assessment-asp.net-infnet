using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudWebAppli.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " Digite o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Data de Aniversário")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dt_nascimento { get; set; }

        private static int CalculaDiasaniversario(DateTime dt_nascimento)
        {
            DateTime today = DateTime.Today;
            DateTime next = new DateTime(today.Year, dt_nascimento.Month, dt_nascimento.Day);

            if (next < today)
                next = next.AddYears(1);

            int numDays = (next - today).Days;

            return numDays;
        }
    }
    
}