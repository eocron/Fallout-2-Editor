using System;
using System.Drawing;
using System.Windows.Forms;
using F2E.Core;
using F2E.Frm;
using F2E.Frm.Dto;
using F2E.Frm.Filters;

namespace F2E.Forms
{
    public partial class ImageViewControl : UserControl
    {
        public ImageViewControl()
        {
            InitializeComponent();

            var blue = ImageHelper.CreateSolidBackground(4000, 4000, Color.Blue);
            var grid = ImageHelper.CreateGridBackground(4000, 4000, Color.LightGray, Color.DarkGray);
            pictureBox1.Image = grid;
            pictureBox2.Image = grid;

            pictureBox3.Image = blue;
            pictureBox4.Image = blue;
        }

        private void ClearPictureBoxes()
        {
            pictureBox1.Controls.Clear();
            pictureBox2.Controls.Clear();
            pictureBox3.Controls.Clear();
            pictureBox4.Controls.Clear();
        }

        private void Attach(PictureBox parent, Bitmap image, bool isFrmMode = false)
        {
            if(image == null)
                return;

            var bitmap = new Bitmap(image);
            if (isFrmMode)
            {
                bitmap.MakeTransparent(Color.White);
            }
            bitmap.MakeTransparent(Color.Transparent);
            var b = new PictureBox
                    {
                        Parent = parent,
                        Image = bitmap,
                        Width = bitmap.Width,
                        Height = bitmap.Height,
                        Location = new Point(0, 0),
                        BackColor = Color.Transparent
                    };
            b.BringToFront();
        }

        public void Clear()
        {
            ClearPictureBoxes();
        }

        public void OnRedrawImportImage(Bitmap original, Bitmap modified, EditorSettingsControl editor)
        {
            Clear();

            if (original != null)
            {
                Attach(pictureBox1, original);
                Attach(pictureBox3, original);
            }

            if (modified != null)
            {
                if (editor.GameBrightnessMultiplier != 1)
                {
                    modified = modified.Apply(new IFilter[] { new BrightnessFilter(editor.GameBrightnessMultiplier) });
                }
                Attach(pictureBox2, modified, true);
                Attach(pictureBox4, modified, true);
            }
        }

        public void OnRedrawSelectedFrame(FrameModel e, EditorSettingsControl editor)
        {
            Clear();

            var frm = e?.Bitmap;
            if (frm != null)
            {
                frm = frm.Apply(new IFilter[] { new BrightnessFilter(editor.GameBrightnessMultiplier) });
            }
            Attach(pictureBox1, e?.Bitmap, true);
            Attach(pictureBox3, e?.Bitmap, true);

            Attach(pictureBox2, frm, true);
            Attach(pictureBox4, frm, true);
        }
    }
}
