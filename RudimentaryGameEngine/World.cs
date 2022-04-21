using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RudimentaryGameEngine
{
	public class World
	{
		private Bitmap screen;
		private Camera camera = new Camera(new Point3F(0, 0, -200), new PointF(640, 480));
		private Graphics screenPanel;
		private PictureBox pcBoxScreen;
		private SolidBrush clearBrush = new SolidBrush(Color.White);
		
		private Point3F worldRotation = new Point3F(0, 0, 0);
		private Random random = new Random();
		private Controller controller;

		public bool orthographic = false;
		public RenderType renderType = RenderType.Regular;

		public SceneObject waitingObject;

		public List<SceneObject> sceneObjects = new List<SceneObject>();
		public List<SceneObject> sceneObjectMap = new List<SceneObject>();

		public World()
		{
			newScreenSize(new PointF(640, 480));
			screenPanel = Graphics.FromImage(screen);
			controller = new Controller(this);
		}

		public void Update()
		{
			controller.update();
			camera.translate(controller.getCameraMove());
			render(orthographic);
			pcBoxScreen.Image = screen;
		}

		public void newScreenSize(PointF resolution)
		{
			screen = new Bitmap((int)resolution.X, (int)resolution.Y);
			screenPanel = Graphics.FromImage(screen);
		}

		private void render(bool Ortho = false)
		{
			screenPanel.FillRectangle(clearBrush, 0, 0, camera.getResolution().X, camera.getResolution().Y);
			foreach (SceneObject SO in sceneObjects)
			{
				Point3F diff = SO.getLocation().deepCopy() - camera.getLocation().deepCopy();
				float mag = diff.magnitude();
				SO.setDepth(mag);
			}

			bool changed = true;
			while (changed)
			{
				changed = false;
				for (int i = 0; i < sceneObjects.Count - 1; i++)
				{
					if (!Ortho)
					{
						if (sceneObjects[i].getDepth() < sceneObjects[i + 1].getDepth())
						{
							SceneObject temp = sceneObjects[i];
							sceneObjects[i] = sceneObjects[i + 1];
							sceneObjects[i + 1] = temp;
							changed = true;
						}
					}
					else
					{
						if (sceneObjects[i].getLocation().Z < sceneObjects[i + 1].getLocation().Z)
						{
							SceneObject temp = sceneObjects[i];
							sceneObjects[i] = sceneObjects[i + 1];
							sceneObjects[i + 1] = temp;
							changed = true;
						}
					}
				}
			}

			foreach (SceneObject SO in sceneObjects)
			{
				Point[] faces;
				if (Ortho)
					faces = SO.getScreenTransformOrtho();
				else
					faces = SO.getScreenTransform();
				for (int i = 0; i < faces.Length; i += 3)
				{
					Point[] face = new Point[3] { faces[i], faces[i + 1], faces[i + 2] };
					if (SO.getBrushes().Length > 0)
					{
						switch (renderType)
						{
							case RenderType.Regular:
								if (SO.getBrushes().Length > 1)
								{
									if (SO.getNoise())
									{
										Color regOldColor = SO.getBrushes()[SO.getFace(i / 3).brushIndice].Color;
										int regNewRed = Utilities.Clamp(regOldColor.R + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
										int regNewGreen = Utilities.Clamp(regOldColor.G + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
										int regNewBlue = Utilities.Clamp(regOldColor.B + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
										Color regNewColor = Color.FromArgb(regOldColor.A, regNewRed, regNewGreen, regNewBlue);
										SolidBrush regBrush = new SolidBrush(regNewColor);
										screenPanel.FillPolygon(regBrush, face);
									}
									else
										screenPanel.FillPolygon(SO.getBrushes()[SO.getFace(i / 3).brushIndice], face);
								}
								else
								{
									if (SO.getNoise())
									{
										Color regOldColor = SO.getBrush().Color;
										int regNewRed = Utilities.Clamp(regOldColor.R + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
										int regNewGreen = Utilities.Clamp(regOldColor.G + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
										int regNewBlue = Utilities.Clamp(regOldColor.B + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
										Color regNewColor = Color.FromArgb(regOldColor.A, regNewRed, regNewGreen, regNewBlue);
										SolidBrush regBrush = new SolidBrush(regNewColor);
										screenPanel.FillPolygon(regBrush, face);
									}
									else
										screenPanel.FillPolygon(SO.getBrush(), face);
								}
								break;

							case RenderType.ForcedNoisy:
								if (SO.getBrushes().Length > 1)
								{
									Color noiseOldColor = SO.getBrushes()[SO.getFace(i / 3).brushIndice].Color;
									int noiseNewRed = Utilities.Clamp(noiseOldColor.R + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
									int noiseNewGreen = Utilities.Clamp(noiseOldColor.G + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
									int noiseNewBlue = Utilities.Clamp(noiseOldColor.B + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
									Color noiseNewColor = Color.FromArgb(noiseOldColor.A, noiseNewRed, noiseNewGreen, noiseNewBlue);
									SolidBrush noiseBrush = new SolidBrush(noiseNewColor);
									screenPanel.FillPolygon(noiseBrush, face);
								}
								else
								{
									Color noiseOldColor = SO.getBrush().Color;
									int noiseNewRed = Utilities.Clamp(noiseOldColor.R + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
									int noiseNewGreen = Utilities.Clamp(noiseOldColor.G + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
									int noiseNewBlue = Utilities.Clamp(noiseOldColor.B + (Convert.ToInt32(SO.getNoiseValue() * ((random.NextDouble() * 2) - 1))), 0, 255);
									Color noiseNewColor = Color.FromArgb(noiseOldColor.A, noiseNewRed, noiseNewGreen, noiseNewBlue);
									SolidBrush noiseBrush = new SolidBrush(noiseNewColor);
									screenPanel.FillPolygon(noiseBrush, face);
								}
								break;

							case RenderType.Trick:
								Color sizedOldColor = SO.getBrush().Color;
								int newSaturation = Utilities.Clamp((Math.Abs(face[0].X - face[1].X) * Math.Abs(face[1].Y - face[2].Y)) / 2, 100, 200);
								Color sizedNewColor = Color.FromArgb(sizedOldColor.A, newSaturation, newSaturation, newSaturation);
								SolidBrush sizedBrush = new SolidBrush(sizedNewColor);
								screenPanel.FillPolygon(sizedBrush, face);
								break;

							case RenderType.MissingNo:
								if (SO.getBrushes().Length > 1)
								{
									if ((i / 3) % 2 == 0)
									{
										screenPanel.FillPolygon(SO.getBrushes()[SO.getFace(i / 3).brushIndice], face);
									}
									else
									{
										screenPanel.FillPolygon(new SolidBrush(Color.Black), face);
									}
								}
								else
								{
									if ((i / 3) % 2 == 0)
									{
										screenPanel.FillPolygon(SO.getBrush(), face);
									}
									else
									{
										screenPanel.FillPolygon(new SolidBrush(Color.Black), face);
									}
								}
								break;

							case RenderType.ForcedWireFrame:
								if (SO.getBrushes().Length > 1)
									screenPanel.DrawPolygon(new Pen(SO.getBrushes()[SO.getFace(i / 3).brushIndice], 1), face);
								else
									screenPanel.DrawPolygon(new Pen(SO.getBrush(), 1), face);
								break;

							case RenderType.Outline:
								if (SO.getBrushes().Length > 1)
								{
									screenPanel.FillPolygon(SO.getBrushes()[SO.getFace(i / 3).brushIndice], face);
									screenPanel.DrawPolygon(new Pen(Color.Black, 1), face);
								}
								else
								{
									screenPanel.FillPolygon(SO.getBrush(), face);
									screenPanel.DrawPolygon(new Pen(Color.Black, 1), face);
								}
								break;
						}
					}
					else
						screenPanel.DrawPolygon(SO.getPen(), face);
				}
			}
		}

		public void addObject(SceneObject sceneObject) 
		{
			sceneObject.setParent(this);
			sceneObject.teleport(new Point3F(camera.getLocation().X, camera.getLocation().Y, camera.getLocation().Z + 200));
			sceneObjects.Add(sceneObject);
			sceneObjectMap.Add(sceneObjects[sceneObjects.Count - 1]);
		}

		public void addWaitingObject()
		{
			waitingObject.setParent(this);
			waitingObject.teleport(new Point3F(camera.getLocation().X, camera.getLocation().Y, camera.getLocation().Z + 200));
			sceneObjects.Add(waitingObject);
			sceneObjectMap.Add(sceneObjects[sceneObjects.Count - 1]);
			waitingObject = null;
		}

		public void setPcBox(PictureBox pictureBox)
		{
			pcBoxScreen = pictureBox;
		}

		public void setWaitingObject(SceneObject obj)
		{
			waitingObject = obj;
		}

		public Camera getCamera()
		{
			return camera;
		}

		public Controller getController()
		{
			return controller;
		}
	}
}
