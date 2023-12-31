﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightDocs.Service.DocumentApi.Models
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } 
        public string Name { get; set; }

        public string? Note { get; set; }

        public string CreateBy { get; set; }

    }
}
