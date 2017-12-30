using Framework;

namespace Emergence.Business
{
    public interface IMapper<Model, ViewModel>
        where ViewModel : NotificationObject
    {
        void MapToViewModel(Model data, ViewModel viewModel);
        void MapToDataModel(ViewModel viewModel, Model model);
    }
}
