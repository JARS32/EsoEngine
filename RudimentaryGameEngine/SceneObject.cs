using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	public class SceneObject
	{
		private string name = "";
		private Point3F location;
		private World parent;
		private Point3F[] initialPointOffsets;
		private Point3F[] scaledPointOffsets;
		private Point3F[] transformedPointOffsets;
		private Point3F rotation = new Point3F(0, 0, 0);
		private Point3F scale = new Point3F(1, 1, 1);
		private Point[] screenPoints;
		private face[] faces = new face[0];
		private face[] faceMap = new face[0];
		private int triCount = 0;
		private float depth = 0;
		private SolidBrush[] brushes = null;
		private Pen pen = null;
		private int colorNoise = 15;
		private bool noisy = false;

		#region Constructors
		public SceneObject(string name, Point3F location, World parent, Point3F[] transformedPointOffsets, Point3F[] scaledPointOffsets, Point3F[] initialPointOffsets, Point3F rotation, Point3F scale, Point[] screenPoints, face[] faces, face[] faceMap, int triCount, float depth, SolidBrush[] brushes, Pen pen)
		{
			this.name = name;
			this.location = location.deepCopy();
			this.parent = parent;
			this.initialPointOffsets = initialPointOffsets;
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.rotation = rotation.deepCopy();
			this.scale = scale.deepCopy();
			this.screenPoints = new Point[screenPoints.Length];
			for (int i = 0; i < screenPoints.Length; i++)
			{
				this.screenPoints[i] = new Point(screenPoints[i].X, screenPoints[i].Y);
			}
			this.faces = new face[faces.Length];
			this.faceMap = new face[faceMap.Length];
			for (int i = 0; i < faces.Length; i++)
			{
				this.faces[i] = faces[i].deepCopy();
				this.faceMap[i] = faceMap[i];
				faces[i].setParent(this);
			}
			this.triCount = triCount;
			this.depth = depth;
			this.brushes = new SolidBrush[brushes.Length];
			for (int i = 0; i < brushes.Length; i++)
			{
				this.brushes[i] = new SolidBrush(brushes[i].Color);
			}
			this.pen = pen;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Point3F rotation, SolidBrush[] brushes)
		{
			this.brushes = brushes;
			this.initialPointOffsets = pointOffsets;
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = rotation;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Point3F rotation, SolidBrush brush)
		{
			this.brushes = new SolidBrush[] { brush };
			this.initialPointOffsets = pointOffsets;
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = rotation;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Point3F rotation, Pen pen)
		{
			this.pen = pen;
			this.initialPointOffsets = pointOffsets;
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = rotation;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, SolidBrush brush)
		{
			this.brushes = new SolidBrush[] { brush };
			this.initialPointOffsets = pointOffsets;
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = new Point3F(0, 0, 0);
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, SolidBrush[] brushes)
		{
			this.brushes = brushes;
			this.initialPointOffsets = pointOffsets;
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = new Point3F(0, 0, 0);
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Pen pen)
		{
			this.pen = pen;
			this.initialPointOffsets = pointOffsets;
			this.transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			this.scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				this.transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = new Point3F(0, 0, 0);
		}

		public SceneObject(Point3F location, SolidBrush[] brushes)
		{
			this.brushes = brushes;
			this.location = location;
		}

		public SceneObject(Point3F location, SolidBrush brush)
		{
			this.brushes = new SolidBrush[] { brush };
			this.location = location;
		}
		#endregion

		public float calculateDepth(Point3F origin)
		{
			Point3F diff = location.deepCopy() - origin.deepCopy();
			float mag = diff.magnitude();
			setDepth(mag);
			return depth;
		}

		public float getDepth()
		{
			return depth;
		}

		public void setDepth(float depth)
		{
			this.depth = depth;
		}

		public Point3F getScale()
		{
			return scale;
		}

		public SceneObject deepCopy()
		{
			return new SceneObject(name, location, parent, transformedPointOffsets, scaledPointOffsets, initialPointOffsets, rotation, scale, screenPoints, faces, faceMap, triCount, depth, brushes, pen);
		}

		#region location transform methods
		public void translate(float x, float y, float z)
		{
			location.X += x;
			location.Y += y;
			location.Z += z;
		}

		public void translate(Point3F translation)
		{
			location.X += translation.X;
			location.Y += translation.Y;
			location.Z += translation.Z;
		}

		public void teleport(float x, float y, float z)
		{
			location.X = x;
			location.Y = y;
			location.Z = z;
		}

		public void teleport(Point3F translation)
		{
			location.X = translation.X;
			location.Y = translation.Y;
			location.Z = translation.Z;
		}
		#endregion

		public Point3F[] getPointOffsets()
		{
			return transformedPointOffsets;
		}

		public Point3F[] getInitialPointOffsets()
		{
			return initialPointOffsets;
		}

		public void addFace(face face)
		{
			face[] newFaces = new face[faces.Length + 1];
			face[] newFaceMap = new face[faceMap.Length + 1];
			for (int i = 0; i < faces.Length; i++)
			{
				newFaces[i] = faces[i];
				newFaceMap[i] = faceMap[i];
			}
			newFaces[faces.Length] = face;
			newFaceMap[faceMap.Length] = face;
			faces = newFaces;
			faceMap = newFaceMap;
		}

		public void setParent(World parent)
		{
			this.parent = parent;
		}

		public World getParent()
		{
			return parent;
		}

		public SolidBrush getBrush()
		{
			return brushes[0];
		}

		public void setNoise(bool value)
		{
			noisy = value;
		}

		public bool getNoise()
		{
			return noisy;
		}

		public Point[] getScreenPoints()
		{
			return screenPoints;
		}

		public int getNoiseValue()
		{
			return colorNoise;
		}

		public void setBrushes(SolidBrush[] brush)
		{
			brushes = brush;
		}

		public void setBrush(SolidBrush brush)
		{
			if (brushes.Length < 1)
			{
				brushes = new SolidBrush[1];
			}
			brushes[0] = brush;
		}

		public void setPen(Pen newPen)
		{
			pen = newPen;
		}

		public SolidBrush[] getBrushes()
		{
			return brushes;
		}

		public Pen getPen()
		{
			return pen;
		}

		public Point3F getLocation()
		{
			return location;
		}

		public Point3F getRotation()
		{
			return rotation;
		}

		public face getFace(int i)
		{
			return faces[i];
		}

		public face getFaceFromMap(int i)
		{
			return faceMap[i];
		}

		public int getFaceCount()
		{
			return faces.Length;
		}

		public void setName(string name)
		{
			this.name = name;
		}

		public string getName()
		{
			return name;
		}

		public void setOffsets(Point3F[] verts)
		{
			screenPoints = new Point[verts.Length];
			initialPointOffsets = verts;
			transformedPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
				transformedPointOffsets[i] = initialPointOffsets[i].deepCopy();
			scaledPointOffsets = new Point3F[initialPointOffsets.Length];
			for (int i = 0; i < initialPointOffsets.Length; i++)
				scaledPointOffsets[i] = initialPointOffsets[i].deepCopy();
		}

		public void rotateQuat(Point3F axis, float angle)
		{
			Point3F axisCopy = axis.deepCopy();
			axisCopy.normalise();
			Quaternion axisQuat = new Quaternion(0, axisCopy.X, axisCopy.Y, axisCopy.Z);
		}

		public void rotateTo(double angleX, double angleY, double angleZ)
		{
			angleX *= Math.PI / 180;
			angleY *= Math.PI / 180;
			angleZ *= Math.PI / 180;
			Quaternion qr = new Quaternion();
			Quaternion qXr = QuaternionHelper.fromAxisAngle(new Point3F(1, 0, 0), Convert.ToSingle(angleX)).normalise();
			Quaternion qYr = QuaternionHelper.fromAxisAngle(new Point3F(0, 1, 0), Convert.ToSingle(angleY)).normalise();
			Quaternion qZr = QuaternionHelper.fromAxisAngle(new Point3F(0, 0, 1), Convert.ToSingle(angleZ)).normalise();
			Quaternion qXrInv = -qXr;
			Quaternion qYrInv = -qYr;
			Quaternion qZrInv = -qZr;

			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				transformedPointOffsets[i] = scaledPointOffsets[i].deepCopy();
			}
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				transformedPointOffsets[i] = transformedPointOffsets[i].rotate(qXr, qXrInv);
				transformedPointOffsets[i] = transformedPointOffsets[i].rotate(qYr, qYrInv);
				transformedPointOffsets[i] = transformedPointOffsets[i].rotate(qZr, qZrInv);
			}
		}

		public void setScale(float ScaleX, float ScaleY, float ScaleZ)
		{
			this.scale = new Point3F(ScaleX, ScaleY, ScaleZ);
			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				scaledPointOffsets[i] = new Point3F(initialPointOffsets[i].X * ScaleX, initialPointOffsets[i].Y * ScaleY, initialPointOffsets[i].Z * ScaleZ);
			}
			Point3F oldRotation = rotation.deepCopy();
			rotation = new Point3F(0);
			rotateTo(oldRotation.X, oldRotation.Y, oldRotation.Z);
		}

		public void rotateX(double angle)
		{
			rotation.X = (rotation.X + Convert.ToSingle(angle)) % 360;
			angle *= Math.PI / 180;
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);

			foreach (Point3F p in transformedPointOffsets)
			{
				double yNew = p.Y * cos - p.Z * sin;
				double zNew = p.Y * sin + p.Z * cos;
				p.Y = Convert.ToSingle(yNew);
				p.Z = Convert.ToSingle(zNew);
			}
		}

		public void rotateY(double angle)
		{
			rotation.Y = (rotation.Y + Convert.ToSingle(angle)) % 360;
			angle *= Math.PI / 180;
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);

			foreach (Point3F p in transformedPointOffsets)
			{
				double xNew = p.X * cos - p.Z * sin;
				double zNew = p.X * sin + p.Z * cos;
				p.X = Convert.ToSingle(xNew);
				p.Z = Convert.ToSingle(zNew);
			}
		}

		public void rotateZ(double angle)
		{
			rotation.Z = (rotation.Y + Convert.ToSingle(angle)) % 360;
			angle *= Math.PI / 180;
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);

			foreach (Point3F p in transformedPointOffsets)
			{
				double xNew = p.X * cos - p.Y * sin;
				double yNew = p.X * sin + p.Y * cos;
				p.X = Convert.ToSingle(xNew);
				p.Y = Convert.ToSingle(yNew);
			}
		}

		public face[] rasteriseObject()
		{
			Point3F cameraPoint = new Point3F(parent.getCamera().location.X, parent.getCamera().location.Y, parent.getCamera().location.Z);
			float XRatio = parent.getCamera().getResolution().X * (parent.getCamera().getAspectRatio().Y / parent.getCamera().getAspectRatio().X);

			for (int i = 0; i < initialPointOffsets.Length; i++)
			{
				Point3F difference = transformedPointOffsets[i].deepCopy() - cameraPoint;
				Point3F screenPos = new Point3F((difference.X / difference.Z) * XRatio, (difference.Y / difference.Z) * parent.getCamera().getResolution().Y);
				screenPoints[i] = (screenPos + new Point3F((int)parent.getCamera().getResolution().X / 2, (int)parent.getCamera().getResolution().Y / 2)).toPoint();
			}

			for (int i = 0; i < faces.Length; i++)
			{
				faces[i].calculateDepth();
			}

			//NEED TO UPDATE
			//REMOVE BUBBLE SORT FOR REAL ALGORITHM
			//bubblesort used to sort faces in order of depth
			bool changed = true;
			while (changed)
			{
				changed = false;
				for (int i = 0; i < faces.Length - 1; i++)
				{
					if (faces[i].getDepth() < faces[i + 1].getDepth())
					{
						face temp = faces[i];
						faces[i] = faces[i + 1];
						faces[i + 1] = temp;
						changed = true;
					}
				}
			}

			return faces;
		}

		//deprecated perspective Rasterisation code
		/*public Point[] getScreenTransform()
		{
			Point[] returnPoints = new Point[3 * triCount];
			for (int i = 0; i < transformedPointOffsets.Length; i++)
			{
				Point3F point = new Point3F(transformedPointOffsets[i].X + location.X, transformedPointOffsets[i].Y + location.Y, transformedPointOffsets[i].Z + location.Z);
				Point3F cameraPoint = new Point3F(parent.getCamera().location.X, parent.getCamera().location.Y, parent.getCamera().location.Z);
				Point3F difference = new Point3F(point.X - cameraPoint.X, point.Y - cameraPoint.Y, point.Z - cameraPoint.Z);
				Point3F screenPos;
				if (difference.Z <= 0)
				{
					continue;
				}
				else
				{
					screenPos = new Point3F((difference.X / difference.Z) * parent.getCamera().getResolution().X * (parent.getCamera().getAspectRatio().Y / parent.getCamera().getAspectRatio().X), (difference.Y / difference.Z) * parent.getCamera().getResolution().Y);
				}
				Point screenPoint = screenPos.toPoint();
				screenPoint.X += (int)parent.getCamera().getResolution().X / 2;
				screenPoint.Y += (int)parent.getCamera().getResolution().Y / 2;
				screenPoints[i] = screenPoint;
			}

			for (int i = 0; i < faces.Length; i++)
			{
				faces[i].calculateDepth();
			}

			bool changed = true;
			while (changed)
			{
				changed = false;
				for (int i = 0; i < faces.Length - 1; i++)
				{
					if (faces[i].getDepth() < faces[i + 1].getDepth())
					{
						face temp = faces[i].deepCopy();
						faces[i] = faces[i + 1];
						faces[i + 1] = temp;
						changed = true;
					}
				}
			}
			
			int ptr = 0;
			foreach (face f in faces)
			{
				if (f.getTriCount() == 1)
				{
					for (int i = 0; i < 3; i++)
					{
						returnPoints[ptr] = screenPoints[f.pointIndices[i]];
						ptr++;
					}
				}
				else
				{
					for (int i = 0; i < 6; i++)
					{
						returnPoints[ptr] = screenPoints[f.pointIndices[i]];
						ptr++;
					}
				}
			}
			return returnPoints;
		}*/

		public Point[] getScreenTransformOrtho()
		{
			Point[] returnPoints = new Point[3*triCount];
			for (int i = 0; i < transformedPointOffsets.Length; i++)
			{
				Point point = new Point(Convert.ToInt32(transformedPointOffsets[i].X + location.X), Convert.ToInt32(transformedPointOffsets[i].Y + location.Y));
				Point cameraPoint = new Point(Convert.ToInt32(parent.getCamera().location.X), Convert.ToInt32(parent.getCamera().location.Y));
				screenPoints[i] = new Point(point.X - cameraPoint.X + (int)parent.getCamera().getResolution().X/2, point.Y - cameraPoint.Y + (int)parent.getCamera().getResolution().Y / 2);
			}

			for (int i = 0; i < faces.Length; i++)
			{
				faces[i].calculateDepthOrtho();
			}

			bool changed = true;
			while (changed)
			{
				changed = false;
				for (int i = 0; i < faces.Length - 1; i++)
				{
					if (faces[i].getDepth() < faces[i + 1].getDepth())
					{
						face temp = faces[i].deepCopy();
						faces[i] = faces[i + 1];
						faces[i + 1] = temp;
						changed = true;
					}
				}
			}

			int ptr = 0;
			foreach (face f in faces)
			{
				for (int i = 0; i < 3; i++)
				{
					returnPoints[ptr] = screenPoints[f.pointIndices[i]];
					ptr++;
				}
			}
			return returnPoints;
		}
	}


	public class face
	{
		public int[] pointIndices { get; }
		public int brushIndice { get; set; } 
		float depth = 0;
		SceneObject parent;

		public face(int[] pointIndices, int brushIndice, SceneObject parent, float depth)
		{
			this.depth = depth;
			this.pointIndices = pointIndices;
			this.brushIndice = brushIndice;
			this.parent = parent;
		}

		public face(int[] pointIndices, int brushIndice, SceneObject parent)
		{
			this.pointIndices = pointIndices;
			this.brushIndice = brushIndice;
			this.parent = parent;
		}

		public face(int[] pointIndices, SceneObject parent)
		{
			this.pointIndices = pointIndices;
			this.brushIndice = 0;
			this.parent = parent;
		}

		public face deepCopy()
		{
			return new face(pointIndices, brushIndice, parent, depth);
		}

		public void calculateDepth()
		{
			//average point depth
			float sum = 0;
			foreach (int i in pointIndices)
			{
				sum += parent.getPointOffsets()[i].calculateDepth(parent.getParent().getCamera().location);
			}
			depth = (sum / pointIndices.Length);

			//minimum point depth
			/*float minDepth = parent.getPointOffsets()[pointIndices[0]].calculateDepth(parent.getParent().getCamera().location);
			foreach (int i in pointIndices)
			{
				if (parent.getPointOffsets()[i].calculateDepth(parent.getParent().getCamera().location) < minDepth)
				{
					minDepth = parent.getPointOffsets()[i].calculateDepth(parent.getParent().getCamera().location);
				}
			}
			depth = minDepth;*/

			//average point z
			/*float sum = 0;
			foreach (int i in pointIndices)
			{
				sum += parent.getPointOffsets()[i].Z;
			}
			depth = (sum / pointIndices.Length);*/
		}

		public void calculateDepthOrtho()
		{
			float sum = 0;
			foreach (int i in pointIndices)
			{
				sum += parent.getPointOffsets()[i].Z;
			}
			depth = (sum / pointIndices.Length) - parent.getParent().getCamera().location.Z;
		}

		public float getDepth()
		{
			return depth;
		}

		public void setParent(SceneObject parent)
		{
			this.parent = parent;
		}
	}
}
