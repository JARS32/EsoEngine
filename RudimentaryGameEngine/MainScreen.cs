using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace RudimentaryGameEngine
{
	public partial class MainScreen : Form
	{
		public World world = new World();
		public Primitives primitives = new Primitives();
		public OBJSerialiser serialiser = new OBJSerialiser();
		public Utilities utils = new Utilities();
		public Dictionary<string, SceneObject> objectTypes = new Dictionary<string, SceneObject>();
		public Bitmap brushPreviewBM = new Bitmap(72, 27);
		public Bitmap colourPreviewBM = new Bitmap(150, 22);
		public Graphics brushPreviewPanel;
		public Graphics colourPreviewPanel;

		public bool inputLock = false;

		public MainScreen()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Thanks!");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			world.setPcBox(pcBoxScreen);
			foreach (string type in Enum.GetNames(typeof(RenderType)))
			{
				CMBBoxRenderType.Items.Add(type);
			}
			CMBBoxRenderType.SelectedIndex = 0;

			brushPreviewPanel = Graphics.FromImage(brushPreviewBM);
			colourPreviewPanel = Graphics.FromImage(colourPreviewBM);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			world.Update();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			//if ()
			//MessageBox.Show($"Form.KeyPress: '{e.KeyCode}' pressed.");
			world.enableKey(e.KeyCode.ToString());
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			//MessageBox.Show($"Form.KeyPress: '{e.KeyCode}' pressed.");
			world.disableKey(e.KeyCode.ToString());
		}

		private void cmbBoxSceneObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbBoxSceneObjects.Items[cmbBoxSceneObjects.SelectedIndex] != null)
			{
				SceneObjectGPBox.Enabled = true;

				inputLock = true;

				NUPLocX.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getLocation().X.ToString();
				NUPLocY.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getLocation().Y.ToString();
				NUPLocZ.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getLocation().Z.ToString();

				NUPLocX.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getLocation().X);
				NUPLocY.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getLocation().Y);
				NUPLocZ.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getLocation().Z);

				NUPRotX.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getRotation().X.ToString();
				NUPRotY.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getRotation().Y.ToString();
				NUPRotZ.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getRotation().Z.ToString();

				NUPRotX.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getRotation().X);
				NUPRotY.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getRotation().Y);
				NUPRotZ.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getRotation().Z);

				NUPScaleX.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getScale().X.ToString();
				NUPScaleY.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getScale().Y.ToString();
				NUPScaleZ.Text = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getScale().Z.ToString();

				NUPScaleX.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getScale().X);
				NUPScaleY.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getScale().Y);
				NUPScaleZ.Value = Convert.ToDecimal(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getScale().Z);

				CHKBoxNoisy.Checked = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getNoise();

				if (world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length < 1)
				{
					NUPBrush.Enabled = false;
					NUPFace.Enabled = false;
					NUPFaceBrush.Enabled = false;
					btnAddBrush.Enabled = false;
					btnChangeBrush.Enabled = false;
				}
				else
				{
					NUPBrush.Enabled = true;
					NUPFace.Enabled = true;
					NUPFaceBrush.Enabled = true;
					btnAddBrush.Enabled = true;
					btnChangeBrush.Enabled = true;

					loadColourPreview();
				}

				inputLock = false;
			}
			else {
				SceneObjectGPBox.Enabled = false;

				NUPLocX.Text = "";
				NUPLocY.Text = "";
				NUPLocZ.Text = "";

				NUPRotX.Text = "";
				NUPRotY.Text = "";
				NUPRotZ.Text = "";

				NUPScaleX.Text = "";
				NUPScaleY.Text = "";
				NUPScaleZ.Text = "";
			}
		}

		#region Transform Changes

		private void NUPLocX_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].teleport(Convert.ToSingle(NUPLocX.Value), Convert.ToSingle(NUPLocY.Value), Convert.ToSingle(NUPLocZ.Value));
		}

		private void NUPLocY_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].teleport(Convert.ToSingle(NUPLocX.Value), Convert.ToSingle(NUPLocY.Value), Convert.ToSingle(NUPLocZ.Value));
		}

		private void NUPLocZ_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].teleport(Convert.ToSingle(NUPLocX.Value), Convert.ToSingle(NUPLocY.Value), Convert.ToSingle(NUPLocZ.Value));
		}

		private void NUPRotX_ValueChanged(object sender, EventArgs e)
		{
			NUPRotX.Value = NUPRotX.Value % 360;
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].rotateTo(Convert.ToSingle(NUPRotX.Value), Convert.ToSingle(NUPRotY.Value), Convert.ToSingle(NUPRotZ.Value));
		}

		private void NUPRotY_ValueChanged(object sender, EventArgs e)
		{
			NUPRotY.Value = NUPRotY.Value % 360;
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].rotateTo(Convert.ToSingle(NUPRotX.Value), Convert.ToSingle(NUPRotY.Value), Convert.ToSingle(NUPRotZ.Value));
		}

		private void NUPRotZ_ValueChanged(object sender, EventArgs e)
		{
			NUPRotZ.Value = NUPRotZ.Value % 360;
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].rotateTo(Convert.ToSingle(NUPRotX.Value), Convert.ToSingle(NUPRotY.Value), Convert.ToSingle(NUPRotZ.Value));
		}

		private void NUPScaleX_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].setScale(Convert.ToSingle(NUPScaleX.Value), Convert.ToSingle(NUPScaleY.Value), Convert.ToSingle(NUPScaleZ.Value));
		}

		private void NUPScaleY_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].setScale(Convert.ToSingle(NUPScaleX.Value), Convert.ToSingle(NUPScaleY.Value), Convert.ToSingle(NUPScaleZ.Value));
		}

		private void NUPScaleZ_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].setScale(Convert.ToSingle(NUPScaleX.Value), Convert.ToSingle(NUPScaleY.Value), Convert.ToSingle(NUPScaleZ.Value));
		}

		#endregion

		private void btnTrash_Click(object sender, EventArgs e)
		{
			world.sceneObjects.Remove(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex]);
			world.sceneObjectMap.RemoveAt(cmbBoxSceneObjects.SelectedIndex);
			cmbBoxSceneObjects.Items.RemoveAt(cmbBoxSceneObjects.SelectedIndex);

			inputLock = true;

			SceneObjectGPBox.Enabled = false;

			NUPLocX.Text = "";
			NUPLocY.Text = "";
			NUPLocZ.Text = "";

			NUPRotX.Text = "";
			NUPRotY.Text = "";
			NUPRotZ.Text = "";

			NUPScaleX.Text = "";
			NUPScaleY.Text = "";
			NUPScaleZ.Text = "";

			NUPBrush.Value = 1;
			NUPFace.Value = 1;
			NUPFaceBrush.Value = 1;

			brushPreviewPanel.FillRectangle(new SolidBrush(Color.Gray), 0, 0, 72, 27);
			PCBoxBrushPreview.Image = brushPreviewBM;

			colourPreviewPanel.FillRectangle(new SolidBrush(Color.Gray), 0, 0, 150, 22);
			PCBoxBrushPreview.Image = colourPreviewBM;

			inputLock = false;
		}

		private void addToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		public void addWaitingSolid()
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.AllowFullOpen = false;
			colorDialog.SolidColorOnly = true;
			colorDialog.ShowDialog();

			world.waitingObject.setBrush(new SolidBrush(colorDialog.Color));
			cmbBoxSceneObjects.Items.Add("Solid " + world.waitingObject.getName() + " " + colorDialog.Color.ToString().Substring(6));
			world.addWaitingObject();
		}

		public void addWaitingMesh()
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.AllowFullOpen = false;
			colorDialog.SolidColorOnly = true;
			colorDialog.ShowDialog();
			WidthDialog width = new WidthDialog();
			width.Visible = false;
			width.ShowDialog();
			world.waitingObject.setBrushes(new SolidBrush[] { });
			world.waitingObject.setPen(new Pen(colorDialog.Color, width.width));
			cmbBoxSceneObjects.Items.Add("Mesh " + world.waitingObject.getName() + " [Width=" + width.width.ToString() + "" + colorDialog.Color.ToString().Substring(12));
			world.addWaitingObject();
		}

		#region add scene primitives

		private void cubeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			world.setWaitingObject(primitives.Cube(world));
		}

		private void cubeToolStripMenuItem_MouseHover(object sender, EventArgs e)
		{
			world.setWaitingObject(primitives.Cube(world));
		}

		private void planeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			world.setWaitingObject(primitives.Plane(world));
		}

		private void planeToolStripMenuItem_MouseHover(object sender, EventArgs e)
		{
			world.setWaitingObject(primitives.Plane(world));
		}

		private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			world.setWaitingObject(primitives.Tri(world));
		}

		private void triangleToolStripMenuItem_MouseHover(object sender, EventArgs e)
		{
			world.setWaitingObject(primitives.Tri(world));
		}

		private void solidToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addWaitingSolid();
		}

		private void meshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			addWaitingMesh();
		}

		private void rubikToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SolidBrush devBrush = new SolidBrush(Color.Black);
			SolidBrush[] rubikBrushes = new SolidBrush[] { new SolidBrush(Color.Red), new SolidBrush(Color.LightGray), new SolidBrush(Color.Blue), new SolidBrush(Color.Green), new SolidBrush(Color.Yellow), new SolidBrush(Color.Orange) };
			Pen devPen = new Pen(Color.Black, 1);
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
			obj.addFace(new face(new int[] { 0, 1, 2 }, 5, obj));
			obj.addFace(new face(new int[] { 0, 2, 3 }, 5, obj));
			obj.addFace(new face(new int[] { 4, 5, 6 }, 1, obj));
			obj.addFace(new face(new int[] { 4, 6, 7 }, 1, obj));
			obj.addFace(new face(new int[] { 1, 2, 5 }, 2, obj));
			obj.addFace(new face(new int[] { 2, 5, 6 }, 2, obj));
			obj.addFace(new face(new int[] { 0, 3, 4 }, 3, obj));
			obj.addFace(new face(new int[] { 3, 4, 7 }, 3, obj));
			obj.addFace(new face(new int[] { 0, 1, 4 }, 4, obj));
			obj.addFace(new face(new int[] { 1, 4, 5 }, 4, obj));
			obj.addFace(new face(new int[] { 2, 3, 7 }, 0, obj));
			obj.addFace(new face(new int[] { 2, 6, 7 }, 0, obj));
			world.setWaitingObject(obj);
			world.addWaitingObject();
			cmbBoxSceneObjects.Items.Add("Solid Cube [Rubik]");
		}

		private void solidToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			addWaitingSolid();
		}

		private void meshToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			addWaitingMesh();
		}

		private void solidToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			addWaitingSolid();
		}

		private void meshToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			addWaitingMesh();
		}

		#endregion

		private void CHKBoxOrtho_CheckedChanged(object sender, EventArgs e)
		{
			world.orthographic = CHKBoxOrtho.Checked;
		}

		private void loadObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SceneObject SO = serialiser.loadObject(world);

			if (SO == null)
				return;
			if (objectTypes.ContainsKey(SO.getName()))
				return;

			ToolStripMenuItem item = new ToolStripMenuItem(SO.getName());
			addToolStripMenuItem.DropDownItems.Add(item);
			item.Click += loadCustomObject;
			item.MouseHover += loadCustomObject;
			ToolStripItem solidItem = new ToolStripMenuItem("Solid");
			item.DropDownItems.Add(solidItem);
			item.DropDownItems[0].Click += addCustomObjectSolid;
			ToolStripItem meshItem = new ToolStripMenuItem("Mesh");
			item.DropDownItems.Add(meshItem);
			item.DropDownItems[1].Click += addCustomObjectMesh;
			objectTypes.Add(SO.getName(), SO);
		}

		void loadCustomObject(object sender, EventArgs e)
		{
			world.setWaitingObject(objectTypes[sender.ToString()].deepCopy());
		}

		void addCustomObjectSolid(object sender, EventArgs e)
		{
			addWaitingSolid();
		}

		void addCustomObjectMesh(object sender, EventArgs e)
		{
			addWaitingMesh();
		}

		private void CHKBoxNoisy_CheckedChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].setNoise(CHKBoxNoisy.Checked);
		}

		private void CMBBoxRenderType_SelectedIndexChanged(object sender, EventArgs e)
		{
			world.renderType = (RenderType)CMBBoxRenderType.SelectedIndex;
		}

		private void CMBBoxRenderType_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

		}

		private void PCBoxBrushPreview_Click(object sender, EventArgs e)
		{
			
		}

		private void NUPBrush_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
			{
				brushPreviewPanel.FillRectangle(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes()[Convert.ToInt32(NUPBrush.Value) - 1], 0, 0, 72, 27);
				PCBoxBrushPreview.Image = brushPreviewBM;
			}
		}

		private void loadColourPreview(bool resetValues = true)
		{
			NUPBrush.Maximum = utils.Clamp(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length, 1, 9999999);
			if (resetValues)
				NUPBrush.Value = 1;

			NUPFaceBrush.Maximum = utils.Clamp(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length, 1, 9999999);
			if (resetValues)
				NUPFaceBrush.Value = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getFace(0).brushIndice + 1;

			NUPFace.Maximum = utils.Clamp(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getFaceCount(), 1, 9999999);

			updateColourPreview();
			updateBrushPreview();
		}

		private void updateColourPreview()
		{
			SolidBrush[] brushes = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes();
			for (int i = 0; i < brushes.Length; i++)
			{
				if (i == brushes.Length - 1)
					colourPreviewPanel.FillRectangle(brushes[i], ((150 / brushes.Length) * i), 0, 150, 27);
				else
					colourPreviewPanel.FillRectangle(brushes[i], ((150 / brushes.Length) * i), 0, (150/brushes.Length), 27);
			}
			PCBoxColourPreview.Image = colourPreviewBM;
		}

		private void updateBrushPreview()
		{
			brushPreviewPanel.FillRectangle(world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes()[Convert.ToInt32(NUPBrush.Value) - 1], 0, 0, 72, 27);
			PCBoxBrushPreview.Image = brushPreviewBM;
		}

		private void btnAddBrush_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.AllowFullOpen = false;
			colorDialog.SolidColorOnly = true;
			colorDialog.ShowDialog();

			inputLock = true;

			SolidBrush[] newBrushes = new SolidBrush[world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length + 1];
			for (int i = 0; i < world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length; i++)
			{
				newBrushes[i] = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes()[i];
			}
			newBrushes[world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length] = new SolidBrush(colorDialog.Color);
			world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].setBrushes(newBrushes);
			
			loadColourPreview(false);

			inputLock = false;
		}

		private void NUPFaceBrush_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
				world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getFace(Convert.ToInt32(NUPFace.Value) - 1).brushIndice = Convert.ToInt32(NUPFaceBrush.Value) - 1;
		}

		private void NUPFace_ValueChanged(object sender, EventArgs e)
		{
			if (!inputLock)
			{
				inputLock = true;
				NUPFaceBrush.Value = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getFace(Convert.ToInt32(NUPFace.Value) - 1).brushIndice + 1;
				inputLock = false;
			}
		}

		private void btnChangeBrush_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.AllowFullOpen = false;
			colorDialog.SolidColorOnly = true;
			colorDialog.ShowDialog();

			inputLock = true;

			SolidBrush[] newBrushes = new SolidBrush[world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length];
			for (int i = 0; i < world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length; i++)
			{
				newBrushes[i] = world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes()[i];
			}
			newBrushes[Convert.ToInt32(NUPBrush.Value) - 1] = new SolidBrush(colorDialog.Color);
			world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].setBrushes(newBrushes);

			loadColourPreview(false);

			inputLock = false;
		}

		private void PCBoxColourPreview_MouseClick(object sender, MouseEventArgs e)
		{

			int brushIndex = (int)((e.Location.X / 150f) * world.sceneObjectMap[cmbBoxSceneObjects.SelectedIndex].getBrushes().Length);

			NUPBrush.Value = brushIndex + 1;
		}

		private void MainScreen_SizeChanged(object sender, EventArgs e)
		{
			int maxRatio = (this.Size.Width - 260) / 4;
			if ((this.Size.Height - 90) / 3 < maxRatio)
				maxRatio = (this.Size.Height - 90) / 3;
			pcBoxScreen.SetBounds(10, 40, 4 * maxRatio, 3 * maxRatio);
		}

		private void pcBoxScreen_MouseClick(object sender, MouseEventArgs e)
		{
			pcBoxScreen.Select();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			world.getCamera().setResolution((int)NUPCameraResX.Value, (int)NUPCameraResY.Value);
			world.newScreenSize(new PointF((int)NUPCameraResX.Value, (int)NUPCameraResY.Value));
			int maxRatio = (this.Size.Width - 260) / (int)world.getCamera().getAspectRatio().X;
			if ((this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y < maxRatio)
				maxRatio = (this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y;
			pcBoxScreen.SetBounds(10, 40, (int)world.getCamera().getAspectRatio().X * maxRatio, (int)world.getCamera().getAspectRatio().Y * maxRatio);
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			world.getCamera().setResolution((int)NUPCameraResX.Value, (int)NUPCameraResY.Value);
			world.newScreenSize(new PointF((int)NUPCameraResX.Value, (int)NUPCameraResY.Value));
			int maxRatio = (this.Size.Width - 260) / (int)world.getCamera().getAspectRatio().X;
			if ((this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y < maxRatio)
				maxRatio = (this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y;
			pcBoxScreen.SetBounds(10, 40, (int)world.getCamera().getAspectRatio().X * maxRatio, (int)world.getCamera().getAspectRatio().Y * maxRatio);
		}

		private void numericUpDown3_ValueChanged(object sender, EventArgs e)
		{
			world.getCamera().setAspectRatio((int)NUPCameraARX.Value, (int)NUPCameraARY.Value);
			world.newScreenSize(new PointF((int)NUPCameraResX.Value, (int)NUPCameraResY.Value));
			int maxRatio = (this.Size.Width - 260) / (int)world.getCamera().getAspectRatio().X;
			if ((this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y < maxRatio)
				maxRatio = (this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y;
			pcBoxScreen.SetBounds(10, 40, (int)world.getCamera().getAspectRatio().X * maxRatio, (int)world.getCamera().getAspectRatio().Y * maxRatio);
		}

		private void numericUpDown4_ValueChanged(object sender, EventArgs e)
		{
			world.getCamera().setAspectRatio((int)NUPCameraARX.Value, (int)NUPCameraARY.Value);
			world.newScreenSize(new PointF((int)NUPCameraResX.Value, (int)NUPCameraResY.Value));
			int maxRatio = (this.Size.Width - 260) / (int)world.getCamera().getAspectRatio().X;
			if ((this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y < maxRatio)
				maxRatio = (this.Size.Height - 90) / (int)world.getCamera().getAspectRatio().Y;
			pcBoxScreen.SetBounds(10, 40, (int)world.getCamera().getAspectRatio().X * maxRatio, (int)world.getCamera().getAspectRatio().Y * maxRatio);
		}

		private void NUPCameraSpeed_ValueChanged(object sender, EventArgs e)
		{
			world.getCamera().setSpeed((float)NUPCameraSpeed.Value);
		}
	}
}
