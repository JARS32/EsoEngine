using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	class QuaternionHelper
	{
		//calculates the quaternion used to rotate a point around the 'Axis' by 'Angle'
		public static Quaternion fromAxisAngle(Point3F Axis, float Angle)
		{
			float factor = Convert.ToSingle(Math.Sin(Angle / 2));

			float x = Axis.X * factor;
			float y = Axis.Y * factor;
			float z = Axis.Z * factor;

			float w = Convert.ToSingle(Math.Cos(Angle / 2));

			return new Quaternion(w, x, y, z).normalise();
		}

		//need to check
		public static Quaternion fromEulerAngles(Point3F angles)
		{
			float w = Convert.ToSingle((Math.Cos(angles.Z / 2) * Math.Cos(angles.X / 2) * Math.Cos(angles.Y / 2)) + (Math.Sin(angles.Z / 2) * Math.Sin(angles.X / 2) * Math.Sin(angles.Y / 2)));
			float x = Convert.ToSingle((Math.Sin(angles.Z / 2) * Math.Cos(angles.X / 2) * Math.Cos(angles.Y / 2)) - (Math.Cos(angles.Z / 2) * Math.Sin(angles.X / 2) * Math.Sin(angles.Y / 2)));
			float y = Convert.ToSingle((Math.Cos(angles.Z / 2) * Math.Sin(angles.X / 2) * Math.Cos(angles.Y / 2)) + (Math.Sin(angles.Z / 2) * Math.Cos(angles.X / 2) * Math.Sin(angles.Y / 2)));
			float z = Convert.ToSingle((Math.Cos(angles.Z / 2) * Math.Cos(angles.X / 2) * Math.Sin(angles.Y / 2)) - (Math.Sin(angles.Z / 2) * Math.Sin(angles.X / 2) * Math.Cos(angles.Y / 2)));

			return new Quaternion(w, x, y, z).normalise();
		}
	}
}
