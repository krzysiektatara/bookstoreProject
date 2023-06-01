﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApplicationAPI.Data.Entities
{
    public class User : Entity
    {
        public int Role_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
