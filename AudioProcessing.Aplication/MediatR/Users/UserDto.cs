﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioProcessing.Aplication.MediatR.Users
{
    public class UserDto
    {
        public Guid UserId { get; set; }    
        public string Name { get; set; }    
        public string Email { get; set; }

        public UserDto(
            Guid userId,
            string name, 
            string email)
        {
            UserId = userId;
            Name = name;
            Email = email;
        }
    }
}
