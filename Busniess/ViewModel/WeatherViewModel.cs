using Framework;
using System.IO;

namespace Emergence.Business.ViewModel
{
    public class WeatherViewModel : WeatherUiModelBase
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

        public string ImageFullPath
        {
            get
            {
                return Path.GetFullPath(string.Format(@"Image\MainPage\WeatherIcons\{0}.png", Img));
            }
        }

        public string TempDisplay
        {
            get
            {
                return string.Format("{0}℃", Temp);
            }
        }

        public string TempLowHighDisplay
        {
            get
            {
                return string.Format("{0}~{1}℃", TempLow, TempHigh);
            }
        }

        public string WindDisplay
        {
            get
            {
                return string.Format("{0}    {1}", WindDirect, WindPower);
            }
        }
    }

}
