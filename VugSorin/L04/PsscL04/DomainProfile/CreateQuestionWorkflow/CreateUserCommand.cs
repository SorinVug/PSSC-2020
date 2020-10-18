using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainQuestion.CreateQuestionWorkflow
{
    public struct CreateUserCommand
    {
        [Required]
        public string Username { get; private set; }
        [Required]
        public string Email { get; private set; }

        public CreateUserCommand(string username,string email)
        {
            Username = username;
            Email = email;
        }
    }
}
