using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalCaimanProject.VM
{
    public class SaveMemberViewModel
    {
       /* [Required]
        [StringLength(30, ErrorMessage = "Le champ Nom est requis")]
        [Display(Name = "Nom")]*/
        public string MemberName { get; set; }

        /*[Required]
        [StringLength(90, ErrorMessage = "Le champ Prénoms est requis")]
        [Display(Name = "Prénoms")]*/
        public string MemberPnom { get; set; }

        /*[Required]
        [Display(Name = "Sexe")]*/
        public string MemberSex { get; set; }

        /*[Display(Name ="Description")]
        [StringLength(1000, ErrorMessage ="Entrez une bref description")]*/
        public string MemberDescription { get; set; }

        [Required]
        public DateTime MemberNaissance { get; set; }

        /*[Required(ErrorMessage ="Veuillez remplir ce champ")]
        [Display(Name ="Lieu de Naissance")]
        [StringLength(30, ErrorMessage ="Nom trop long")]*/
        public string MemberLieuNaissance { get; set; }

        /*[Required]
        [EmailAddress]
        [Display(Name = "Adresse Mail")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Adresse Mail Invalide")]*/
        public string MemberMail { get; set; }

/*
        [Required]
        [Phone]
        [Display(Name = "Téléphone")]
        [RegularExpression(@"((\+)?[ ]?(225))?[ ]?[02456789]{1}[\d]{1}([ _.-]?[\d]{2}){3}", ErrorMessage = "Ce champ requiert un numéro ivoirien")]*/
        public int MemberPhone { get; set; }

        public string MemberStatus { get; set; }

        /*[Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Commune d'habitation")]
        [StringLength(30, ErrorMessage = "Nom trop long")]*/
        public string MemberCommune { get; set; }

        /*[Required(ErrorMessage = "Veuillez remplir ce champ")]
        [Display(Name = "Quartier de Résidence")]
        [StringLength(30, ErrorMessage = "Nom trop long")]*/
        public string MemberQuartier { get; set; }

        /*[Required(ErrorMessage = "Entrer le moyen de transport")]*/
        public int TranportId { get; set; }

       /* [Required(ErrorMessage = "Entrer la specialite")]*/
        public int SpecialiteId { get; set; }


        //atttribute

      /*  [Required(ErrorMessage = "Entrer le moyen de transport")]*/
        public string TranportName { get; set; }


       /* [Required(ErrorMessage = "Entrer la specialite")]*/
        public string SpecialiteName { get; set; }

    }
}