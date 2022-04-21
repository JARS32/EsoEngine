using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	public class Controller
	{
		private Dictionary<string, bool> controls = new Dictionary<string, bool>();

		private World owner;

		private Point3F upcomingCameraTranslation = new Point3F(0, 0, 0);

		public Controller(World world)
		{
			owner = world;
			controls.Add("W", false);
			controls.Add("A", false);
			controls.Add("S", false);
			controls.Add("D", false);
			controls.Add("E", false);
			controls.Add("Q", false);
			controls.Add("ShiftKey", false);
			controls.Add("ControlKey", false);
			controls.Add("Space", false);
		}

		public void update()
		{
			upcomingCameraTranslation.X = Convert.ToByte(controls["D"]) - Convert.ToByte(controls["A"]);
			upcomingCameraTranslation.Y = Convert.ToByte(controls["Q"]) - Convert.ToByte(controls["E"]);
			upcomingCameraTranslation.Z = Convert.ToByte(controls["W"]) - Convert.ToByte(controls["S"]);
			upcomingCameraTranslation = upcomingCameraTranslation.normalise();
			upcomingCameraTranslation *= Convert.ToByte(controls["ShiftKey"]) + 1;
			upcomingCameraTranslation *= 2 * owner.getCamera().getSpeed() / 100;
		}

		public void enableKey(string key)
		{
			controls[key] = true;
		}

		public void disableKey(string key)
		{
			controls[key] = false;
		}

		public Point3F getCameraMove()
		{
			return upcomingCameraTranslation;
		}
	}
}
