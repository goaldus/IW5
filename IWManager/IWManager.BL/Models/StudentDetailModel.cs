using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IWManager.DAL.Entities.Base.Implementation;

namespace IWManager.BL.Models
{
    public class StudentDetailModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Photo { get; set; }
        public Sex Sex { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}