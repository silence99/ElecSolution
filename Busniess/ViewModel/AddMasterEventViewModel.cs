using Business.Services;
using Emergence.Common.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busniess.ViewModel
{
	public class AddMasterEventViewModel : MasterEvent
	{
		public virtual DelegateCommand SubmitCommand { get; set; }
		public virtual List<DictItem> EventTypes { get; set; }
		public virtual List<DictItem> EventGrades { get; set; }

		public AddMasterEventViewModel()
		{
			SubmitCommand = new DelegateCommand(SubmitAction);
			EventTypes = MetaDataService.EventTypes.ToList();
			EventGrades = MetaDataService.EventGrades.ToList();
			this.Time = DateTime.Now.ToString("yyyy-MM-dd");
		}

		public void SubmitAction()
		{

		}
	}
}
