using Framework;

namespace Emergence.Common
{
    public interface IMapper<Model, UiModel>
        where UiModel : NotificationObject
    {
        void MapToUiModel(Model data, UiModel uiModel);
        void MapToDataModel(UiModel uiModel, Model model);
    }
}
