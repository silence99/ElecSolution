using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;

namespace Busniess.ViewModel
{
	public class SubEventPopupVideoViewModel : NotificationObject
	{
		public virtual ObservableCollection<CameraModel> Cameras { get; set; }
		public static int Max = 1000;
		/// <summary>
		/// should be Master event id
		/// </summary>
		public virtual double EventID { get; set; }

		public void SetCoordinate(double latitude, double longitude)
		{
			var cameraService = new CameraService();
			var data = cameraService.GetCamera(1, Max, latitude, longitude);
			if (data != null && data.Data != null)
			{
				Cameras = new ObservableCollection<CameraModel>(data.Data);
			}
			else
			{
				Cameras = new ObservableCollection<CameraModel>();
			}
		}
	}
}
