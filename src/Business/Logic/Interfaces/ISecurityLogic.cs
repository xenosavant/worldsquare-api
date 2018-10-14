using Stellmart.Api.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public interface ISecurityLogic
    {
        Task<List<SecurityQuestionViewModel>> GetQuestions(string userName);
    }
}
