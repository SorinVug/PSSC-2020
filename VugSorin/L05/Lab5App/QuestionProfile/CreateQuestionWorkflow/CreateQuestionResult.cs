using CSharp.Choices;
using DomainQuestion.CreateUserWorkflow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainQuestion.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }
        public class QuestionPublished : ICreateQuestionResult
        {
            [Required]
            public UserCreated User { get; private set; }

            [Required]
            public CreateQuestionCommand Question { get; private set; }

            [Required]
            public Guid QuestionID { get; private set; }

            [Required]
            public int Votes { get; private set; }

            [Required]
            public Dictionary<Guid, bool> votesMap { get; private set; }

            public QuestionPublished(CreateQuestionCommand question, Guid questionID, UserCreated user)
            {
                Question = question;
                QuestionID = questionID;
                User = user;
                Votes = 0;
                votesMap = new Dictionary<Guid, bool>();
            }

            public void VoteQuestion(UserCreated fromUser, bool vote)
            {
                if (votesMap.Contains(new KeyValuePair<Guid, bool>(fromUser.UserId, vote)))
                {
                    return;
                }
                else if (votesMap.ContainsKey(fromUser.UserId))
                {
                    if (vote)
                    {
                        Votes += 2;
                        votesMap[fromUser.UserId] = true;
                    }
                    else
                    {
                        Votes -= 2;
                        votesMap[fromUser.UserId] = false;
                    }
                }
                else
                {
                    if (vote)
                    {
                        Votes += 1;
                    }
                    else
                    {
                        Votes -= 1;
                    }
                    votesMap.Add(fromUser.UserId, vote);
                }
            }
        }

        public class QuestionNotPublished : ICreateQuestionResult
        {
            [Required]
            public string reason_ { get; private set; }

            public QuestionNotPublished(string reason)
            {
                reason_ = reason;
            }
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            [Required]
            public IEnumerable<string> validationErrors_ { get; private set; }

            public QuestionValidationFailed(IEnumerable<string> validationErrors)
            {
                validationErrors_ = validationErrors.AsEnumerable();
            }
        }
    }
}