using DomainQuestion.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using static DomainQuestion.CreateQuestionWorkflow.CreateQuestionResult;

namespace ApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CreateQuestionCommand("How to add a reference to my project in VS2017?", "Issues","VisualStudio2017", "Fixed Issues in VisualStudio 2017");
            var result = CreateQuestion(cmd);

            result.Match(
                    ProcessQuestionPublished,
                    ProcessQuestionNotPublished,
                    ProcessInvalidQuestion
                );

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();

        }
        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }
        private static ICreateQuestionResult ProcessQuestionNotPublished(QuestionNotPublished questioneNotPublishedResult)
        {
            Console.WriteLine($"Question not published: {questioneNotPublishedResult.Reason}");
            return questioneNotPublishedResult;
        }

        private static ICreateQuestionResult ProcessQuestionPublished(QuestionPublished question)
        {
            var cmdUser = new CreateUserCommand("SorinVug74", "sorin.vugEmailTest@yahoo.com");
            Console.WriteLine($"Question from {cmdUser.Username} was accepted!");
            Console.WriteLine($"Question was published with id: {question.QuestionId}");
            Console.WriteLine($"Confirmation email was sent to: {cmdUser.Email}");
            return question;
        }
        public static ICreateQuestionResult CreateQuestion(CreateQuestionCommand createQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Category) || string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Invalid category or title!" };
                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(7) > 1)
            {
                return new QuestionNotPublished("Category or title could not be verified");
            }

            var questionId = Guid.NewGuid();
            var result = new QuestionPublished(questionId, createQuestionCommand.Category, createQuestionCommand.Title);

            return result;
        }
    }
}
