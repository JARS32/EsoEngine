﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	public class Camera
	{
		public Point3F location { get; }
		public Point3F rotation = new Point3F(0, 0, 0);
		private PointF Resolution = new PointF(640, 480);
		private PointF AspectRatio = new PointF(4, 3);
		private float screenDepth = 40;
		private float flightSpeed = 100;

		public Camera(Point3F location, PointF Resolution, float depth = 5)
		{
			this.location = location;
			this.Resolution = Resolution;
			this.screenDepth = depth;
		}

		public void translate(float x, float y, float z)
		{
			location.X += x;
			location.Y += y;
			location.Z += z;
		}

		public void translate(Point3F translation)
		{
			translation = translation.rotate(new Point3F(0, 1, 0), -rotation.Y);

			location.X += translation.X;
			location.Y += translation.Y;
			location.Z += translation.Z;
		}

		public void rotate(Point3F rotation)
		{
			this.rotation.X += rotation.X;
			this.rotation.Y += rotation.Y;
			this.rotation.Z += rotation.Z;
		}

		public float getDepth()
		{
			return screenDepth;
		}

		public Point3F getLocation()
		{
			return location;
		}

		public float getSpeed()
		{
			return flightSpeed;
		}

		public void setSpeed(float speed)
		{
			flightSpeed = speed;
		}

		public void setResolution(int x, int y)
		{
			Resolution = new PointF(x, y);
		}

		public PointF getResolution()
		{
			return Resolution;
		}

		public void setAspectRatio(int x, int y)
		{
			AspectRatio = new PointF(x, y);
		}

		public PointF getAspectRatio()
		{
			return AspectRatio;
		}
	}
}
