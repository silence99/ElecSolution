using Framework;

namespace Utils
{
    public interface IMapper<Model, UiModel>
        where UiModel : NotificationObject
    {
        UiModel ToUiModel(Model data);
        Model ToDataModel(UiModel uiModel);
    }
}
