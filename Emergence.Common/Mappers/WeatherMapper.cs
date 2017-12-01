using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using System;

namespace Emergence.Common.Mappers
{
    public class WeatherMapper : IMapper<Weather, WeatherUiModel>
    {
        public void MapToDataModel(WeatherUiModel uiModel, Weather model)
        {
            throw new NotImplementedException();
        }

        public void MapToUiModel(Weather data, WeatherUiModel uiModel)
        {
            if (data == null || uiModel == null)
            {
                return;
            }

            uiModel.City = data.City;

            uiModel.Cityid = data.Cityid;
            uiModel.Citycode = data.Citycode;
            uiModel.Updatetime = data.Updatetime;

            uiModel.Date = data.Date;
            uiModel.DayOfWeek = data.DayOfWeek;
            uiModel.WeatherName = data.WeatherName;
            uiModel.Temp = data.Temp;
            uiModel.TempHigh = data.TempHigh;
            uiModel.TempLow = data.TempLow;
            uiModel.Img = data.Img;
            uiModel.WindDirect = data.WindDirect;
            uiModel.WindPower = data.WindPower;
            uiModel.WindSpeed = data.WindSpeed;
            if (data.Daily != null)
            {
                uiModel.Daily = new WeatherUiModelBase[data.Daily.Length];
                for (int i = 0; i < data.Daily.Length; i++)
                {
                    uiModel.Daily[i] = new WeatherUiModelBase()
                    {
                        Date = data.Daily[i].Date,
                        WeatherName = data.Daily[i].WeatherName,
                        TempHigh = data.Daily[i].Day.TempHigh,
                        TempLow = data.Daily[i].Night.TempLow,
                        Img = data.Daily[i].Day.Img
                    };
                }
            }
        }
    }
}
