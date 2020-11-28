using Access.Primitives.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.EF.Models;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [Route("questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        public QuestionController(IInterpreterAsync interpreter)
        {
            _interpreter = interpreter;
        }

        [HttpPost("createQuestion")]
        public async Task<ActionResult> CreateQuestion(int questionId,string title, string tags)
        {
            CreateQuestionCommand cmd = new CreateQuestionCommand();
            var questionWriteContext = new QuestionWriteContext(new List<CreateQuestionCommand>()
            {
                new CreateQuestionCommand()
                {
                    QuestionId = 7
                },
                new CreateQuestionCommand()
                {
                    Title = "How to convert from Guid to string in C#?"
                },
                new CreateQuestionCommand()
                {
                    Tags = "C#"
                }
            });
           
            var expr = from questionResult in QuestionDomain.CreateQuestion(questionId,"123", "C#")
                       select questionResult;
            
            var result = await _interpreter.Interpret(expr, questionWriteContext, new object());

            return result.Match(
               created => Ok(created),
               notCreated => BadRequest(notCreated),
               invalidRequest => ValidationProblem());
        }
    }
}
