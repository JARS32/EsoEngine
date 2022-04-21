using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RudimentaryGameEngine
{
	public class Primitives
	{
		public SceneObject Cube(World world)
		{
			SolidBrush[] rubikBrushes = new SolidBrush[] { new SolidBrush(Color.Black) };
			Point3F[] test = new Point3F[8];
			test[0] = new Point3F(-20, -20, -20);
			test[1] = new Point3F(20, -20, -20);
			test[2] = new Point3F(20, 20, -20);
			test[3] = new Point3F(-20, 20, -20);
			test[4] = new Point3F(-20, -20, 20);
			test[5] = new Point3F(20, -20, 20);
			test[6] = new Point3F(20, 20, 20);
			test[7] = new Point3F(-20, 20, 20);
			face[] testFaces = new face[12];
			SceneObject obj = new SceneObject(new Point3F(world.getCamera().location.X, world.getCamera().location.Y, world.getCamera().location.Z + 200), test, rubikBrushes);
			obj.setName("Cube");
			obj.addFace(new face(new int[] { 0, 1, 2 }, obj));
			obj.addFace(new face(new int[] { 0, 2, 3 }, obj));
			obj.addFace(new face(new int[] { 4, 5, 6 }, obj));
			obj.addFace(new face(new int[] { 4, 6, 7 }, obj));
			obj.addFace(new face(new int[] { 1, 2, 5 }, obj));
			obj.addFace(new face(new int[] { 2, 5, 6 }, obj));
			obj.addFace(new face(new int[] { 0, 3, 4 }, obj));
			obj.addFace(new face(new int[] { 3, 4, 7 }, obj));
			obj.addFace(new face(new int[] { 0, 1, 4 }, obj));
			obj.addFace(new face(new int[] { 1, 4, 5 }, obj));
			obj.addFace(new face(new int[] { 2, 3, 7 }, obj));
			obj.addFace(new face(new int[] { 2, 6, 7 }, obj));
			return obj;
		}

		public SceneObject Plane(World world)
		{
			SolidBrush[] devBrushes = new SolidBrush[] { new SolidBrush(Color.Black) };
			Point3F[] test = new Point3F[4];
			test[0] = new Point3F(-20, -20);
			test[1] = new Point3F(20, -20);
			test[2] = new Point3F(20, 20);
			test[3] = new Point3F(-20, 20);
			SceneObject obj = new SceneObject(new Point3F(world.getCamera().location.X, world.getCamera().location.Y, world.getCamera().location.Z + 200), test, devBrushes);
			obj.setName("Plane");
			obj.addFace(new face(new int[] { 0, 1, 2 }, obj));
			obj.addFace(new face(new int[] { 0, 2, 3 }, obj));
			return obj;
		}

		public SceneObject Tri(World world)
		{
			SolidBrush[] devBrushes = new SolidBrush[] { new SolidBrush(Color.Black) };
			Point3F[] test = new Point3F[3];
			test[0] = new Point3F(-20, -20);
			test[1] = new Point3F(20, -20);
			test[2] = new Point3F(20, 20);
			SceneObject obj = new SceneObject(new Point3F(world.getCamera().location.X, world.getCamera().location.Y, world.getCamera().location.Z + 200), test, devBrushes);
			obj.setName("Triangle");
			obj.addFace(new face(new int[] { 0, 1, 2 }, obj));
			return obj;
		}
	}
}
