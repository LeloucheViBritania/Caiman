﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace CaimanProject.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

/*        [Required]
        [Display(Name = "Nom")]
        [StringLength(30, ErrorMessage = "Veuillez saisir votre nom svp")]*/
        public string MemberName { get; set; }

/*        [Required]
        [Display(Name = "Prénoms")]
        [StringLength(50, ErrorMessage = "Veuillez saisir votre prénom svp ")]*/
        public string MemberPnom { get; set; }

/*        [Required]
        [Display(Name = "Sexe")]
        [StringLength(8, ErrorMessage = "Nous avons besoin de connaître votre sexe")]*/
        public string MemberSex { get; set; }

/*        [Display(Name = "Description")]*/
        public string MemberDescription { get; set; }


/*        [Required]
        [Display(Name = "Date de Naissance")]
        [DataType(DataType.DateTime, ErrorMessage = "Entrez votre Date de Naissance")]*/
        public DateTime MemberNaissance { get; set; }

/*        [Display(Name = "Lieu de Naissance")]*/
        public string MemberLieuNaissance { get; set; }

/*        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Adresse Mail Invalide")]*/
        public string MemberMail { get; set; }
/*
        [Required]
        [RegularExpression(@"((\+)?[ ]?(225))?[ ]?[02456789]{1}[\d]{1}([ _.-]?[\d]{2}){3}", ErrorMessage = "Numéro de téléphone incorrecte")]*/
        public int MemberPhone { get; set; }

        public string MemberImageName { get; set; }

        public bool MemberIsArchived { get; set; }

        public string MemberStatus { get; set; }

/*        [Display(Name = "Commune d'habitation")]*/
        public string MemberCommune { get; set; }
/*
        [Display(Name = "Quartier de résidence")]*/
        public string MemberQuartier { get; set; }

        public DateTime MemberDateArchive { get; set; }

        public int MemberMissonFin { get; set; }

        public int MemberMissionActive { get; set; }

        public int MemberNote { get; set; }
        // recupere la liste des reseaux sociaux du membre
        public virtual ICollection<SocialNetwork> SocialNetworks { get; set; }

        //recupere un transport
        
        public int TransportId { get; set; }
        public virtual Transport TransportMember { get; set; }
      

        //Recupere la liste des projet dont le membre participe
        public virtual ICollection<ProjetMember> ProjetMembers { get; set; }

        //Recupete la specialite dont le membre fait partie
      
       public int SpecialiteId { get; set; }
        public virtual Specialite Specialite { get; set; }
       

        //la recuperation de la liste des competences
        public virtual ICollection<Competence> Competences { get; set; }
        public bool IsChecked { get; set; }

    }


}