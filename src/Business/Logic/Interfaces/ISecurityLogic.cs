using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public interface ISecurityLogic
    {
        Task<List<SecurityQuestionViewModel>> GetQuestions(string userName);
    }
}
