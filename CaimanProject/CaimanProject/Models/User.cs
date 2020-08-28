using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaimanProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage ="Saississez votre nom svp")]
        [Display(Name ="Nom")]
        public string UserName { get; set; }

        [Required]
        [StringLength(90, ErrorMessage = "Saississez votre prénom svp")]
        [Display(Name = "Prénoms")]
        public string UserPnom { get; set; }

        [Required]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage ="Veuillez entrer une adresse valide")]
        [Display(Name ="Adresse Mail")]
        public string UserMail { get; set; }

        [Required]
        [RegularExpression(@"((\+)?[ ]?(225))?[ ]?[02456789]{1}[\d]{1}([ _.-]?[\d]{2}){3}",ErrorMessage ="Entrez un numéro de la Côte d'Ivoire")]
        [Display(Name ="Téléphone")]
        public string UserPhone { get; set; }

        [Required]
        [Display(Name ="Sexe")]
        public string Sexe { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        [Display(Name ="Mot de passe")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="ConfirmPassword")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}