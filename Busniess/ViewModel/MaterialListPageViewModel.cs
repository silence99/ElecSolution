using Business.Services;
using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;

namespace Emergence.Business.ViewModel
{
	public class MaterialListPageViewModel : NotificationObject
	{
		public virtual ObservableCollection<MaterialModel> Materials { get; set; }
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual string QueryMaterialsName { get; set; }
		public virtual string QueryMaterialsNumber { get; set; }
		public virtual string QueryMaterialsDept { get; set; }
		public virtual int QueryIsConsumable { get; set; }
		public virtual MaterialModel CurrentMaterial { get; set; }
		public virtual double PopupWidth { get; set; }
		public virtual double PopupHeight { get; set; }
		public virtual bool IsPopoupOpen { get; set; }
		public virtual bool IsPageEnabled { get; set; }
		public virtual bool IsCreateMaterial { get; set; }
		public virtual string PopupTitle { get; set; }
		public virtual ObservableCollection<DictItem> MaterialTypes { get; set; }

		public void PopupTeamEdit()
		{
			IsPopoupOpen = true;
			IsPageEnabled = false;
		}

		public void ClosePopup()
		{
			IsPopoupOpen = false;
			IsPageEnabled = true;
		}

		public MaterialListPageViewModel()
		{
			PageIndex = 1;
			PageSize = 10;
			TotalCount = 0;
			TotalPage = 0;
			IsPopoupOpen = false;
			IsPageEnabled = true;
			PopupWidth = 640;
			PopupHeight = 360;
			IsCreateMaterial = false;
			PopupTitle = "添加物资"; //添加物资、修改物资

			MaterialTypes = new ObservableCollection<DictItem>(MetaDataService.MaterialTypes);
		}
	}
}
