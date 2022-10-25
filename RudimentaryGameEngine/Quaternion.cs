using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	public class Quaternion
	{
		#region properties
		float W = 1, X = 0, Y = 0, Z = 0;
		#endregion

		#region constructors
		public Quaternion()
		{
		}

		public Quaternion(float w, float x, float y, float z)
		{
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Quaternion(Quaternion q)
		{
			this.W = q.W;
			this.X = q.X;
			this.Y = q.Y;
			this.Z = q.Z;
		}
		#endregion

		#region operators
		//calculates the hamilton product of the (Quaternion 'Left') * (Quaternion 'Right'), order important as the calculation isn't commutive
		public static Quaternion operator *(Quaternion left, Quaternion right)
		{
			Quaternion r = new Quaternion(left);
			r.W = ((left.W * right.W) - (left.X * right.X) - (left.Y * right.Y) - (left.Z * right.Z));
			r.X = ((left.W * right.X) + (left.X * right.W) + (left.Y * right.Z) - (left.Z * right.Y));
			r.Y = ((left.W * right.Y) - (left.X * right.Z) + (left.Y * right.W) + (left.Z * right.X));
			r.Z = ((left.W * right.Z) + (left.X * right.Y) - (left.Y * right.X) + (left.Z * right.W));
			return r;
		}

		//calculates the conjugate of the quaternion which in the case of unit quaternions used in rotations is the same as it's inverse
		public static Quaternion operator -(Quaternion right)
		{
			Quaternion r = new Quaternion();
			r.W = right.W;
			r.X = right.X * -1;
			r.Y = right.Y * -1;
			r.Z = right.Z * -1;
			return r;
		}
		#endregion

		#region methods
		//calculates the magnitude of the quaternions, effectively 4 dimensional pythagoras
		public float magnitude()
		{
			return Convert.ToSingle(Math.Pow(Convert.ToDouble((W * W) + (X * X) + (Y * Y) + (Z * Z)), (double)1 / 4));
		}

		//calculates the normalised version of the quaternion, effectively transforms it into a unit vector with the same direction
		public Quaternion normalise()
		{
			float vectorMag = magnitude();
			float normW = 0;
			float normX = 0;
			float normY = 0;
			float normZ = 0;
			if (vectorMag > 0)
			{
				normW = W / vectorMag;
				normX = X / vectorMag;
				normY = Y / vectorMag;
				normZ = Z / vectorMag;
			}
			return new Quaternion(normW, normX, normY, normZ);
		}
		#endregion

		#region type transforms and get/sets
		//returns a Point3 using the imaginary components of the quaternion
		public Point3F toPoint3F()
		{
			return new Point3F(X, Y, Z);
		}

		public float getW()
		{
			return W;
		}

		public float getX()
		{
			return X;
		}

		public float getY()
		{
			return Y;
		}

		public float getZ()
		{
			return Z;
		}

		public override string ToString()
		{
			return ("W:" + W.ToString() + " X:" + X.ToString() + " Y:" + Y.ToString() + " Z:" + Z.ToString());
		}
		#endregion
	}
}