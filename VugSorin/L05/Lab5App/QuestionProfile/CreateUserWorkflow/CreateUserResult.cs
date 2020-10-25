using DomainQuestion.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainQuestion.CreateUserWorkflow
{
    public interface ICreateUser { }
    public class UserCreated : ICreateUser
    {
        [Required]
        public CreateUserCommand User { get; private set; }

        [Required]
        public Guid UserId { get; private set; }

        public UserCreated(CreateUserCommand user, Guid userId)
        {
            User = user;
            UserId = userId;
        }
    }
    public class UserNotCreated : ICreateUser
    {
        [Required]
        public string reason_ { get; private set; }

        public UserNotCreated(string reason)
        {
            reason_ = reason;
        }
    }

    public class UserNotValidated : ICreateUser
    {
        [Required]
        public IEnumerable<string> validationErrors_ { get; private set; }

        public UserNotValidated(IEnumerable<string> validationErrors)
        {
            validationErrors_ = validationErrors.AsEnumerable();
        }
    }
}
