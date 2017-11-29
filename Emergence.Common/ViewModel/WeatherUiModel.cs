using Framework;

namespace Emergence.Common.ViewModel
{
    public class WeatherUiModel : WeatherUiModelBase
    {
        public virtual string City { get; set; }
        public virtual string Cityid { get; set; }
        public virtual string Citycode { get; set; }
        public virtual string Updatetime { get; set; }
    }

    public class WeatherUiModelBase : NotificationObject
    {
        public virtual string Date { get; set; }
        public virtual string DayOfWeek { get; set; }
        public virtual string WeatherName { get; set; }
        public virtual string Temp { get; set; }
        public virtual string TempHigh { get; set; }
        public virtual string TempLow { get; set; }
        public virtual string Img { get; set; }
        public virtual string WindDirect { get; set; }
        public virtual string WindPower { get; set; }
        public virtual string WindSpeed { get; set; }        
        public virtual WeatherUiModelBase[] Daily { get; set; }
    }

}
