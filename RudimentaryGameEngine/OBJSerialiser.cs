using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3DMathFramework;

namespace RudimentaryGameEngine
{
	public class OBJSerialiser
	{
		public string[] openObj(string path)
		{
			if (!File.Exists(path))
				return null;

			return File.ReadAllLines(path);
		}

		public SceneObject loadObject(World world)
		{
			OpenFileDialog file = new OpenFileDialog();
			file.Filter = "obj files (*.obj)|*.obj";
			file.ShowDialog();
			string[] lines = openObj(file.FileName);
			if (lines == null)
				return null;

			SolidBrush[] devBrushes = new SolidBrush[] { new SolidBrush(Color.Black) };

			SceneObject obj = new SceneObject(new Point3F(world.getCamera().location.X, world.getCamera().location.Y, world.getCamera().location.Z + 200), new Point3F[] { }, devBrushes);

			obj.setParent(world);
			obj.setName((file.FileName.Split('\\')[file.FileName.Split('\\').Length - 1]).Split('.')[0]);

			List<Point3F> vertices = new List<Point3F>();
			foreach (string line in lines)
			{
				switch (line.Substring(0, 2))
				{
					case "v ":
						string[] values = line.Split(' ');
						vertices.Add(new Point3F(Convert.ToSingle(values[1]), Convert.ToSingle(values[2]) * -1, Convert.ToSingle(values[3])));
						break;
					case "f ":
						string[] verts = line.Split(' ');
						switch (verts.Length)
						{
							case 4:
								obj.addFace(new face(new int[] { Convert.ToInt32(verts[1].Split('/')[0]) - 1, Convert.ToInt32(verts[2].Split('/')[0]) - 1, Convert.ToInt32(verts[3].Split('/')[0]) - 1 }, obj));
								break;
							case 5:
								obj.addFace(new face(new int[] { Convert.ToInt32(verts[1].Split('/')[0]) - 1, Convert.ToInt32(verts[2].Split('/')[0]) - 1, Convert.ToInt32(verts[3].Split('/')[0]) - 1 }, obj));
								obj.addFace(new face(new int[] { Convert.ToInt32(verts[1].Split('/')[0]) - 1, Convert.ToInt32(verts[3].Split('/')[0]) - 1, Convert.ToInt32(verts[4].Split('/')[0]) - 1 }, obj));
								break;
							default:
								obj.addFace(new face(new int[] { Convert.ToInt32(verts[1].Split('/')[0]) - 1, Convert.ToInt32(verts[2].Split('/')[0]) - 1, Convert.ToInt32(verts[3].Split('/')[0]) - 1 }, obj));
								obj.addFace(new face(new int[] { Convert.ToInt32(verts[1].Split('/')[0]) - 1, Convert.ToInt32(verts[3].Split('/')[0]) - 1, Convert.ToInt32(verts[4].Split('/')[0]) - 1 }, obj));
								break;
						}
						break;
					default:
						break;
				}
			}

			obj.setOffsets(vertices.ToArray());
			return obj;
		}
	}
}
