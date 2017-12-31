using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using log4net;
using System.Collections.Generic;
using System.Reflection;

namespace Busniess.Services
{
	public class PersonService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public EmergencyHttpListResult<PersonModel> GetTeamDetails(int pageIndex, int pageSize, long teamId, string Name = "")
		{
			return null;
		}

		public bool DeletePerson(List<string> ids)
		{
			return true;
		}
	}
}
