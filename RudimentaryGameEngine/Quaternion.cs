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
		/// <summary>
		/// Creates a Quaternion with a real value of 1 and all imaginary have values of 0
		/// </summary>
		public Quaternion()
		{
		}

		/// <summary>
		/// Creates a Quaternion based on the input parameters
		/// </summary>
		/// <param name="w">Real component of the Quaternion</param>
		/// <param name="x">First imaginary component of the Quaternion, i</param>
		/// <param name="y">Second imaginary component of the Quaternion, j</param>
		/// <param name="z">Third imaginary component of the Quaternion, k</param>
		public Quaternion(float w, float x, float y, float z)
		{
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Creates a Quaternion that is a copy of the quaternion parameter
		/// </summary>
		/// <param name="q">Quaternion to copy</param>
		public Quaternion(Quaternion q)
		{
			this.W = q.W;
			this.X = q.X;
			this.Y = q.Y;
			this.Z = q.Z;
		}
		#endregion

		#region operators
		/// <summary>
		/// calculates the hamilton product of the (Quaternion 'Left') * (Quaternion 'Right'), order important as the calculation isn't commutive
		/// </summary>
		/// <param name="left">Quaternion on the left side of the equation</param>
		/// <param name="right">Quaternion on the right side of the equation</param>
		/// <returns>Quaternion that is the Hamilton product of the parameter Quaternions</returns>
		public static Quaternion operator *(Quaternion left, Quaternion right)
		{
			Quaternion r = new Quaternion(left);
			r.W = ((left.W * right.W) - (left.X * right.X) - (left.Y * right.Y) - (left.Z * right.Z));
			r.X = ((left.W * right.X) + (left.X * right.W) + (left.Y * right.Z) - (left.Z * right.Y));
			r.Y = ((left.W * right.Y) - (left.X * right.Z) + (left.Y * right.W) + (left.Z * right.X));
			r.Z = ((left.W * right.Z) + (left.X * right.Y) - (left.Y * right.X) + (left.Z * right.W));
			return r;
		}

		/// <summary>
		/// calculates the conjugate of the quaternion which in the case of unit quaternions used in rotations is the same as it's inverse
		/// </summary>
		/// <param name="right">The quaternion whos Conjugate is to be calculated</param>
		/// <returns>The conjugate of the parameter quaternion</returns>
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
		/// <summary>
		/// calculates the magnitude of the quaternions, effectively 4 dimensional pythagoras
		/// </summary>
		/// <returns>the magnitude of the quaternion</returns>
		public float magnitude()
		{
			return Convert.ToSingle(Math.Pow(Convert.ToDouble((W * W) + (X * X) + (Y * Y) + (Z * Z)), (double)1 / 4));
		}

		/// <summary>
		/// calculates the normalised version of the quaternion, effectively transforms it into a unit vector with the same direction
		/// </summary>
		/// <returns>the normalised version of this quaternion</returns>
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
		/// <summary>
		/// returns a Point3 using the imaginary components of the quaternion
		/// </summary>
		/// <returns>a Point3F of the imaginary components of the quaternion</returns>
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