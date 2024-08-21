﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class _3DModels
    {
        [Key]
        public string? object_name { get; set; }

        public int subject_id { get; set; }

        public int FK_Users_Id { get; set; }

        public string? description { get; set; }

        public DateTime datetime { get; set; }

        public bool hidden { get; set; }

        public string? tags { get; set; }

        public string? user_picture { get; set; }

        public string? qr_code { get; set; }

        public string? file { get; set; }

        public int category { get; set; }

        public int copyright { get; set; }

        public string? copyright_info { get; set; }


    }
}
