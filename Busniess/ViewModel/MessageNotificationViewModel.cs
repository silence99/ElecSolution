using Business.Services;
using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Busniess.ViewModel
{
	public class MessageNotificationViewModel : NotificationObject
	{
		public virtual DelegateCommand SendMessageCommand { get; set; }
		public virtual DelegateCommand ImportMemberCommand { get; set; }
		public virtual DelegateCommand DeleteMemberCommand { get; set; }
		public virtual DelegateCommand PopupAddDialogCommand { get; set; }
		public virtual DelegateCommand AddMemberCommand { get; set; }
		public virtual DelegateCommand PopupCloseDialogCommand { get; set; }
		public virtual DelegateCommand GetTemplateCommand { get; set; }

		public virtual ObservableCollection<PersonModel> Members { get; set; }
		public virtual ObservableCollection<DictItem> SendTypes { get; set; }
		public virtual ObservableCollection<MessageTemplateModel> MessageTemplates { get; set; }
        public virtual event SetPopupHandler SetPopupEditToFullScreen;

        public virtual string SendType { get; set; }
		public virtual string TemplateId { get; set; }
		public virtual string TemplateContent { get; set; }
		public virtual string ChildEventId { get; set; }
		public virtual string ImportFile { get; set; }
        public virtual bool IsPopupOpen { get; set; }
        public virtual double PopupWidth { get; set; }
        public virtual double PopupHeight { get; set; }
        public virtual PersonModel CurrentMember { get; set; }
		public virtual bool IsEditMode { get; set; }

		public virtual MessageService MEService { get; set; }

		public MessageNotificationViewModel()
		{
			MEService = new MessageService();
			SendTypes = new ObservableCollection<DictItem>(MetaDataService.TemplateTypes);
			MessageTemplates = new ObservableCollection<MessageTemplateModel>();

			SendMessageCommand = new DelegateCommand(SendMessageAction);
			DeleteMemberCommand = new DelegateCommand(DeleteMemberAction);
			PopupAddDialogCommand = new DelegateCommand(PopupAddDialogAction);
			PopupCloseDialogCommand = new DelegateCommand(PopupCloseDialogAction);
			ImportMemberCommand = new DelegateCommand(ImportAction);
			GetTemplateCommand = new DelegateCommand(GetMessageTemplatesAction);
			AddMemberCommand = new DelegateCommand(AddOrEditAction);

			if (!string.IsNullOrEmpty(SendType))
			{
				SendType = SendTypes == null || SendTypes.Count == 0 ? "" : SendTypes[0].Value;
			}
			Members = new ObservableCollection<PersonModel>();
            PopupHeight = ResolutionService.Height;
            PopupWidth = ResolutionService.Width;
        }

		private void GetMessageTemplatesAction()
		{
			var obj = this.IsAopWapper ? this : this.CreateAopProxy();
			if (!string.IsNullOrEmpty(obj.SendType))
			{
				var members = MEService.GetTemplates(obj.SendType) ?? new MessageTemplateModel[0];
				var proxyItems = members.Select(item => item.CreateAopProxy());
				obj.MessageTemplates = new ObservableCollection<MessageTemplateModel>(proxyItems);
			}
		}

		public void SendMessageAction()
		{
			var obj = this;
			if (!this.IsAopWapper)
			{
				obj = this.CreateAopProxy();
			}
			if (obj.Members != null && obj.Members.Count > 0)
			{
				var sendInfo = Utils.JSONHelper.ToJsonString(obj.Members.Select(item => new
				{
					name = item.Name,
					phone = item.PhoneNumber
				}).ToArray());
				var result = obj.MEService.SendMessge(obj.SendType, obj.ChildEventId, obj.TemplateId, sendInfo);
                if (result)
                {
                    System.Windows.MessageBox.Show("发送成功！");
                }
                else
                {
                    System.Windows.MessageBox.Show("发送失败，请联系管理员");
                }
			}
			else
			{
				System.Windows.MessageBox.Show("请导入人员");
			}
		}

		private void DeleteMemberAction()
		{
			var obj = this.IsAopWapper ? this : this.CreateAopProxy();
			if (obj.Members != null && obj.Members.Count != 0)
			{
				obj.Members = new ObservableCollection<PersonModel>(obj.Members.Where(item => !item.IsChecked).ToArray());
            }
            System.Windows.MessageBox.Show("删除成功！");
        }

		private void PopupAddDialogAction()
		{
			var obj = this.IsAopWapper ? this : this.CreateAopProxy();
			obj.CurrentMember = new PersonModel();
			obj.IsEditMode = false;
			obj.IsPopupOpen = true;
            if (SetPopupEditToFullScreen != null)
            {
                SetPopupEditToFullScreen();
            }
		}

		private void PopupCloseDialogAction()
		{
			var obj = this.IsAopWapper ? this : this.CreateAopProxy();
			obj.IsPopupOpen = false;
		}

		private void AddOrEditAction()
		{
			var obj = this.IsAopWapper ? this : this.CreateAopProxy();
			if (obj.IsEditMode == false)
			{
				obj.Members.Add(new PersonModel() { Name = obj.CurrentMember.Name, PhoneNumber = obj.CurrentMember.PhoneNumber });
			}
			obj.Members = new ObservableCollection<PersonModel>(obj.Members.ToArray());
			PopupCloseDialogAction();
		}

        private void ImportAction()
        {
            var obj = this.IsAopWapper ? this : this.CreateAopProxy();
            var file = obj.ImportFile;

            //import logic here
        }
        public void AddPerson(List<PersonModel> pList)
        {
            var obj = this.IsAopWapper ? this : this.CreateAopProxy();
            foreach (var pm in pList)
            {
                obj.Members.Add(pm.CreateAopProxy());
            }
        }
    }
}
