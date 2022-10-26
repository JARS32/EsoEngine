using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using _3DMathFramework;
using Point = System.Drawing.Point;

namespace RudimentaryGameEngine
{
	public class SceneObject
	{
		private string name = "";
		private Point3F location;
		private World parent;
		private Point3F[] vertices;
		private Point3F TBforwardVector = new Point3F(0, 0, 1);
		private Point3F TBupVector = new Point3F(0, 1, 0);
		private Point3F TBrightVector = new Point3F(1, 0, 0);
		private Point3F rotation = new Point3F(0, 0, 0);
		private Point3F scale = new Point3F(1, 1, 1);
		private Point[] screenPoints;
		private face[] faces = new face[0];
		private int triCount = 0;
		private float depth = 0;
		private SolidBrush[] brushes = null;
		private Pen pen = null;
		private int colorNoise = 15;
		private bool noisy = false;

		#region constructors
		public SceneObject(string name, Point3F location, World parent, Point3F[] pointOffsets, Point3F rotation, Point3F scale, Point[] screenPoints, face[] faces, int triCount, float depth, SolidBrush[] brushes, Pen pen)
		{
			this.name = name;
			this.location = location.deepCopy();
			this.parent = parent;
			this.rotation = rotation.deepCopy();
			this.scale = scale.deepCopy();
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < pointOffsets.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
			}
			this.screenPoints = new Point[screenPoints.Length];
			for (int i = 0; i < screenPoints.Length; i++)
			{
				this.screenPoints[i] = new Point(screenPoints[i].X, screenPoints[i].Y);
			}
			this.faces = new face[faces.Length];
			for (int i = 0; i < faces.Length; i++)
			{
				this.faces[i] = faces[i].deepCopy();
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
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = rotation;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Point3F rotation, SolidBrush brush)
		{
			this.brushes = new SolidBrush[] { brush };
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = rotation;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Point3F rotation, Pen pen)
		{
			this.pen = pen;
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = rotation;
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, SolidBrush brush)
		{
			this.brushes = new SolidBrush[] { brush };
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = new Point3F(0, 0, 0);
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, SolidBrush[] brushes)
		{
			this.brushes = brushes;
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
			}
			this.screenPoints = new Point[pointOffsets.Length];
			this.location = location;
			this.rotation = new Point3F(0, 0, 0);
		}

		public SceneObject(Point3F location, Point3F[] pointOffsets, Pen pen)
		{
			this.pen = pen;
			this.vertices = new Point3F[pointOffsets.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				this.vertices[i] = new Point3F(pointOffsets[i].X, pointOffsets[i].Y, pointOffsets[i].Z);
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

		#region get sets
		public Point3F[] getTB()
		{
			return new Point3F[] { TBrightVector, TBupVector, TBforwardVector};
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

		public Point3F[] getPointOffsets()
		{
			return vertices;
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
			vertices = verts;
		}

		public void setScale(float ScaleX, float ScaleY, float ScaleZ)
		{
			float scaleFactorX = ScaleX / this.scale.X;
			float scaleFactorY = ScaleY / this.scale.Y;
			float scaleFactorZ = ScaleZ / this.scale.Z;
			this.scale = new Point3F(ScaleX, ScaleY, ScaleZ);
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = new Point3F(vertices[i].X * scaleFactorX, vertices[i].Y * scaleFactorY, vertices[i].Z * scaleFactorZ);
			}
			/*Point3F oldRotation = rotation.deepCopy();
			rotation = new Point3F(0);
			rotateTo(oldRotation.X, oldRotation.Y, oldRotation.Z);*/
		}
		#endregion

		public SceneObject deepCopy()
		{
			return new SceneObject(name, location, parent, vertices, rotation, scale, screenPoints, faces, triCount, depth, brushes, pen);
		}

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

		public void addFace(face face)
		{
			face[] newFaces = new face[faces.Length + 1];
			for(int i = 0; i < faces.Length; i++)
			{
				newFaces[i] = faces[i];
			}
			newFaces[faces.Length] = face;
			triCount += face.getTriCount();
			faces = newFaces;
		}

		public void rotateQuat(Point3F axis, float angle)
		{
			Point3F axisCopy = axis.deepCopy();
			axisCopy.normalise();
			Quaternion axisQuat = new Quaternion(0, axisCopy.X, axisCopy.Y, axisCopy.Z);
		}

		public void rotateWorld(double angle, string plane)
		{
			double radAngle;
			Quaternion qr;

			switch (plane)
			{
				case "x":
					radAngle = (angle - rotation.X) * Math.PI / 180;
					qr = QuaternionHelper.fromAxisAngle(new Point3F(1, 0, 0), Convert.ToSingle(radAngle)).normalise();
					rotation.X = Convert.ToSingle(angle) % 360;
					break;
				case "y":
					radAngle = (angle - rotation.Y) * Math.PI / 180;
					qr = QuaternionHelper.fromAxisAngle(new Point3F(0, 1, 0), Convert.ToSingle(radAngle)).normalise();
					rotation.Y = Convert.ToSingle(angle) % 360;
					break;
				case "z":
					radAngle = (angle - rotation.Z) * Math.PI / 180;
					qr = QuaternionHelper.fromAxisAngle(new Point3F(0, 0, 1), Convert.ToSingle(radAngle)).normalise();
					rotation.Z = Convert.ToSingle(angle) % 360;
					break;
				default:
					return;
			}

			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = vertices[i].rotate(qr);
			}

			TBforwardVector = TBforwardVector.rotate(qr);
			TBupVector = TBupVector.rotate(qr);
			TBrightVector = TBrightVector.rotate(qr);
		}

		public void rotateLocal(double angle, string plane)
		{
			double radAngle;
			Quaternion qr;

			switch (plane)
			{
				case "x":
					radAngle = (angle - rotation.X) * Math.PI / 180;
					qr = QuaternionHelper.fromAxisAngle(TBrightVector, Convert.ToSingle(radAngle)).normalise();
					rotation.X = Convert.ToSingle(angle) % 360;
					TBforwardVector = TBforwardVector.rotate(qr);
					break;
				case "y":
					radAngle = (angle - rotation.Y) * Math.PI / 180;
					qr = QuaternionHelper.fromAxisAngle(TBupVector, Convert.ToSingle(radAngle)).normalise();
					rotation.Y = Convert.ToSingle(angle) % 360;
					TBforwardVector = TBforwardVector.rotate(qr);
					TBrightVector = TBrightVector.rotate(qr);
					break;
				case "z":
					radAngle = (angle - rotation.Z) * Math.PI / 180;
					qr = QuaternionHelper.fromAxisAngle(TBforwardVector, Convert.ToSingle(radAngle)).normalise();
					rotation.Z = Convert.ToSingle(angle) % 360;
					break;
				default:
					return;
			}

			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = vertices[i].rotate(qr);
			}
		}

		public Point[] getScreenTransform()
		{
			Point[] returnPoints = new Point[3 * triCount];
			for (int i = 0; i < vertices.Length; i++)
			{
				Point3F worldLocation = location.deepCopy() - parent.getCamera().location.deepCopy();
				worldLocation = worldLocation.rotate(new Point3F(0, 1, 0), parent.getCamera().rotation.Y);
				Point3F[] worldVertices = new Point3F[vertices.Length];
				for (int j = 0; j < vertices.Length; j++)
				{
					worldVertices[j] = vertices[j].rotate(new Point3F(0, 1, 0), parent.getCamera().rotation.Y);
				}
				Point3F point = new Point3F(worldVertices[i].X + worldLocation.X, worldVertices[i].Y + worldLocation.Y, worldVertices[i].Z + worldLocation.Z);
				Point3F cameraPoint = new Point3F(parent.getCamera().location.X, parent.getCamera().location.Y, parent.getCamera().location.Z);
				Point3F difference = new Point3F(point.X - cameraPoint.X, point.Y - cameraPoint.Y, point.Z - cameraPoint.Z);
				Point3F screenPos;
				if (worldLocation.Z <= 0)
				{
					continue;
				}
				else
				{
					screenPos = new Point3F((difference.X / difference.Z) * parent.getCamera().getResolution().X * (parent.getCamera().getAspectRatio().Y / parent.getCamera().getAspectRatio().X), (difference.Y / difference.Z) * parent.getCamera().getResolution().Y);
				}
				Point screenPoint = new Point(Convert.ToInt32(screenPos.X), Convert.ToInt32(screenPos.Y));
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
		}

		public Point[] getScreenTransformOrtho()
		{
			Point[] returnPoints = new Point[3*triCount];
			for (int i = 0; i < vertices.Length; i++)
			{
				Point point = new Point(Convert.ToInt32(vertices[i].X + location.X), Convert.ToInt32(vertices[i].Y + location.Y));
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
		}
	}


	public class face
	{
		public int[] pointIndices { get; }
		public int brushIndice { get; set; } 
		float depth = 0;
		int triCount = 0;
		SceneObject parent;

		public face(int[] pointIndices, int brushIndice, SceneObject parent, float depth)
		{
			this.depth = depth;
			this.pointIndices = pointIndices;
			triCount++;
			this.brushIndice = brushIndice;
			this.parent = parent;
		}

		public face(int[] pointIndices, int brushIndice, SceneObject parent)
		{
			this.pointIndices = pointIndices;
			triCount++;
			this.brushIndice = brushIndice;
			this.parent = parent;
		}

		public face(int[] pointIndices, SceneObject parent)
		{
			this.pointIndices = pointIndices;
			this.brushIndice = 0;
			triCount++;
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

		public int getTriCount()
		{
			return triCount;
		}

		public void setParent(SceneObject parent)
		{
			this.parent = parent;
		}
	}
}
