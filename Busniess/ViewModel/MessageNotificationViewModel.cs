using Business.Services;
using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;
using System.Windows;
using
using Busniess.Services;

namespace Busniess.ViewModel
{
    public class MessageNotificationViewModel : NotificationObject
    {
        public virtual ObservableCollection<PersonModel> Members { get; set; }
        public virtual ObservableCollection<DictItem> SendTypes { get; set; }
        public virtual ObservableCollection<MessageTemplateModel> MessageTemplates { get; set; }

        public virtual MessageService MEService { get; set; }

        public MessageNotificationViewModel()
        {
            MEService = new MessageService();
            SendTypes = new ObservableCollection<DictItem>(MetaDataService.TemplateTypes);
            MessageTemplates = new ObservableCollection<MessageTemplateModel>();
        }
    }
}
