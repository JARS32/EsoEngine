using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	public class Point3F
	{
		#region properties
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float Depth { get; set; }
		#endregion

		#region constructors
		public Point3F(float x, float y, float z, float depth)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.Depth = depth;
		}

		public Point3F(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Point3F(float x, float y)
		{
			this.X = x;
			this.Y = y;
			this.Z = 0;
		}

		public Point3F(float xyz)
		{
			this.X = xyz;
			this.Y = xyz;
			this.Z = xyz;
		}
		#endregion

		#region methods
		//calculates the length of the this point as a vector
		public float magnitude()
		{
			return Convert.ToSingle(Math.Pow(Convert.ToDouble((X * X) + (Y * Y) + (Z * Z)), (double)1 / 3));
		}

		//normalises the point as if it was a vector, effectively keeps the direction while changing magnitude to 1, or whatever is passed as the parameter
		public Point3F normalise(float distance = 1)
		{
			float vectorMag = magnitude();
			float normX = 0;
			float normY = 0;
			float normZ = 0;
			if (vectorMag > 0)
			{
				normX = X / vectorMag;
				normY = Y / vectorMag;
				normZ = Z / vectorMag;
			}
			return new Point3F(normX, normY, normZ) * distance;
		}

		//calculates the depth by finding the magnitude of the vector between this point and the given origin
		public float calculateDepth(Point3F origin)
		{
			Point3F difference = new Point3F(X-origin.X, Y-origin.Y, Z-origin.Z);
			Depth = difference.magnitude();
			return Depth;
		}

		//creates a true copy of the point that has it's own memory address
		public Point3F deepCopy()
		{
			return new Point3F(X, Y, Z, Depth);
		}

		public Point toPoint()
		{
			return new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
		}

		//calculates the points new position using a quaternion that represents a rotation around an axis
		public Point3F rotate(Quaternion q, Quaternion qInv)
		{
			Quaternion pq = new Quaternion(0, X, Y, Z);
			Quaternion pInv = q * pq;
			pInv *= qInv;
			return pInv.toPoint3F();
		}

		public Point3F rotate(Quaternion q)
		{
			Quaternion pq = new Quaternion(0, X, Y, Z);
			Quaternion pInv = q * pq;
			Quaternion qInv = -q;
			pInv *= qInv;
			return pInv.toPoint3F();
		}

		public Point3F rotate(Point3F Axis, float Angle)
		{ 
			double radAngle = Angle * Math.PI / 180;
			Quaternion q = QuaternionHelper.fromAxisAngle(Axis, Convert.ToSingle(radAngle)).normalise();

			Quaternion pq = new Quaternion(0, X, Y, Z);
			Quaternion pInv = q * pq;
			Quaternion qInv = -q;
			pInv *= qInv;
			return pInv.toPoint3F();
		}

		public override bool Equals(Object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				Point3F p = (Point3F)obj;
				Byte temp = Convert.ToByte(X == p.X);
				temp += Convert.ToByte(Y == p.Y);
				temp += Convert.ToByte(Z == p.Z);
				return (temp >= 3);
			}
		}

		public override int GetHashCode()
		{
			return Convert.ToInt32(X.ToString() + Y.ToString() + Z.ToString());
		}
		#endregion

		#region operators
		public static Point3F operator *(Point3F left, float right)
		{
			left.X *= right;
			left.Y *= right;
			left.Z *= right;
			return left;
		}

		public static Point3F operator *(Point3F left, Point3F right)
		{
			left.X *= right.X;
			left.Y *= right.Y;
			left.Z *= right.Z;
			return left;
		}

		public static Point3F operator /(Point3F left, float right)
		{
			left.X /= right;
			left.Y /= right;
			left.Z /= right;
			return left;
		}

		public static Point3F operator /(Point3F left, Point3F right)
		{
			left.X /= right.X;
			left.Y /= right.Y;
			left.Z /= right.Z;
			return left;
		}

		public static Point3F operator +(Point3F left, Point3F right)
		{
			left.X += right.X;
			left.Y += right.Y;
			left.Z += right.Z;
			return left;
		}

		public static Point3F operator -(Point3F left, Point3F right)
		{
			left.X -= right.X;
			left.Y -= right.Y;
			left.Z -= right.Z;
			return left;
		}

		public static bool operator ==(Point3F left, Point3F right)
		{
			Byte temp = Convert.ToByte(left.X == right.X);
			temp += Convert.ToByte(left.Y == right.Y);
			temp += Convert.ToByte(left.Z == right.Z);
			return (temp >= 3);
		}

		public static bool operator !=(Point3F left, Point3F right)
		{
			Byte temp = Convert.ToByte(left.X == right.X);
			temp += Convert.ToByte(left.Y == right.Y);
			temp += Convert.ToByte(left.Z == right.Z);
			return (temp < 3);
		}
		#endregion
	}
}
