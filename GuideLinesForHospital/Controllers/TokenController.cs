using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuideLinesForHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// Evaluating the rule and preparing token.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns>return a token</returns>
        [HttpPost]
        [Route("evaluate")]
        public ActionResult<string> Evaluate(string rule)
        {
            if (string.IsNullOrEmpty(rule))
            {
                throw new ArgumentException(nameof(rule));
            }
            List<string> operators = new List<string> { "+", "-", "*" };
            List<string> variables = new List<string> { "x", "y" };
            List<string> compareOperators = new List<string> { "=", ">" };
            string token = string.Empty;
            for (int i = 0; i < rule.Length; i++)
            {
                if (rule[i] == ' ')
                {
                    continue;
                }
                // appending string to token if the string is presented in operator list.
                if (operators.Any(oper => oper == rule[i].ToString()))
                {
                    token += $"Operator: {rule[i]}; ";
                }
                // appending string to token if the string is presented in variables list.
                else if (variables.Any(vari => vari == rule[i].ToString()))
                {
                    token += $"Variables: {rule[i]}; ";
                }
                // appending string to token if the string is presented in compareOperators list.
                else if (compareOperators.Any(comp => comp == rule[i].ToString()))
                {
                    token += $"CompareOperators: {rule[i]}; ";
                }
                else
                {
                    token += $"Number: {rule[i]}; ";
                }
            }
            return Ok(new { token });

        }
    }
}
