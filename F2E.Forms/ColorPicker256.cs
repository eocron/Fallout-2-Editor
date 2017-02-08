using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace F2E.Forms
{
    public partial class ColorPicker256 : UserControl
    {
        private static readonly Color[] DefaultColors =
            Enumerable.Range(0, 256).Select(x => Color.FromArgb(255, x, x, x)).ToArray();

        private const int CellSize = 15;
        private const int ModCellSize = CellSize + 1;

        public event EventHandler<Color[]> OnColorPaletteChanged;

        public event EventHandler<int> OnColorSelected;

        private Color[] _colors;

        public Color[] Colors
        {
            get { return _colors ?? DefaultColors; }
            set
            {
                if (_colors != null)
                {
                    if (_colors.Length != 256)
                    {
                        throw new ArgumentOutOfRangeException("palette", "You should specify 256 colors.");
                    }
                }
                _colors = value;
                RedrawPalette();
                OnColorPaletteChanged?.Invoke(this, value);
            }
        }

        public int SelectedColorIndex;

        public Color SelecteColor => Colors[SelectedColorIndex];

        private readonly PictureBox _selectorBox = new PictureBox();
        public ColorPicker256()
        {
            InitializeComponent();
            var size = new Size(ModCellSize * 16, ModCellSize * 16);
            this.MaximumSize = size;
            this.MinimumSize = size;
            this.Size = size;
            var image = new Bitmap(ModCellSize+1, ModCellSize+1);
            using (var gfx = Graphics.FromImage(image))
            {
                using (var pen = new Pen(Color.Red, 4))
                {
                    gfx.DrawRectangle(pen, 0,0,image.Width, image.Height);
                }
            }
            _selectorBox.Width = image.Width;
            _selectorBox.Height = image.Height;
            _selectorBox.Image = image;
            _selectorBox.BackColor = Color.Transparent;
            _selectorBox.Parent = PickerPictureBox;

            DrawColorSelection(-1);
            RedrawPalette();
        }

        private void RedrawPalette()
        {
            PickerPictureBox.BackgroundImage = DrawColorBoxPicture();
        }

        private int FindColorId(Point point)
        {
            var xi = point.X / ModCellSize;
            var yi = point.Y / ModCellSize;
            int id = yi << 4 | xi;
            return id;
        }

        private Point FindColorLocation(int id)
        {
            int xi = id & 0x0F;
            int yi = id >> 4;
            return new Point(xi * ModCellSize, yi * ModCellSize);
        }

        private Image DrawColorBoxPicture()
        {
            int width = PickerPictureBox.Width;
            int height = PickerPictureBox.Height;

            var bitmap = new Bitmap(width, height);
            var backColor = PickerPictureBox.BackColor;
            var gridColor = Color.DarkGray;

            using (var gfx = Graphics.FromImage(bitmap))
            {
                using (var brush = new SolidBrush(backColor))
                {
                    gfx.FillRectangle(brush, 0, 0, width, height);
                }

                using (var pen = new Pen(gridColor))
                {
                    for (int i = 0; i < 16; i++)
                    {
                        int wi = i * ModCellSize;
                        gfx.DrawLine(pen, wi, 0, wi, height);
                    }

                    for (int i = 0; i < 16; i++)
                    {
                        int hi = i * ModCellSize;
                        gfx.DrawLine(pen, 0, hi, width, hi);
                    }
                }

                for (int i = 0; i < Colors.Length; i++)
                {
                    var point = FindColorLocation(i);

                    using (var brush = new SolidBrush(Colors[i]))
                    {
                        gfx.FillRectangle(brush, point.X + 1, point.Y + 1, ModCellSize-1, ModCellSize-1);
                    }
                }
            }
            return bitmap;
        }

        private void DrawColorSelection(int id)
        {
            if (id < 0)
            {
                _selectorBox.Visible = false;
                _selectorBox.Location = new Point(0,0);
            }
            else
            {
                var point = FindColorLocation(id);
                _selectorBox.Location = point;
                _selectorBox.Visible = true;
            }
        }
        private void PickerPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var id = FindColorId(e.Location);
            if (id < 0)
            {
                SelectedColorIndex = -1;
            }
            else
            {
                SelectedColorIndex = id;
            }
            DrawColorSelection(id);
            OnColorSelected?.Invoke(this, id);
        }
    }
}
