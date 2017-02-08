using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using F2E.Frm;
using F2E.Frm.Dto;

namespace F2E.App.Forms.Frm
{
    public partial class FrmEditor : Form
    {
        private FrmManager _frmManager;
        private FrmModel _model;
        private Palette _palette;

        private Bitmap ImportImage { get; set; }

        private FrmModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnModelChanged();
            }
        }

        private FrameModel SelectedFrame
        {
            get
            {
                if (FrameListView.SelectedItems.Count == 0)
                    return null;
                var item = FrameListView.SelectedItems[0].Tag as Tuple<FrmDirectionType, int>;
                return GetFrame(item.Item1, item.Item2);
            }
        }

        public event EventHandler<FrameModel> OnSelectedFrameChanged;

        public event EventHandler<Palette> OnPaletteChanged; 

        private Palette Palette
        {
            get { return _palette; }
            set
            {
                _palette = value;
                _frmManager = new FrmManager(_palette);
                OnPaletteChanged?.Invoke(this, _palette);
            }
        }

        public FrmEditor()
        {
            InitializeComponent();
            FrameListView.Columns.Add(new ColumnHeader());
            FrameListView.TileSize = new Size(60, 60);
            FrameListView.View = View.LargeIcon;

            for (int i = 0; i < 6; i++)
            {
                var g = new ListViewGroup(((FrmDirectionType) i).ToString());
                FrameListView.Groups.Add(g);
            }

            importSettingsControl1.OnSettingsChanged += (x, y) => ImageViewHandler();
            editorSettingsControl1.OnSettingsChanged += (x, y) => ImageViewHandler();
            editorSettingsControl1.OnSettingsChanged += (x, y) => { UpdateColorPaletteControls(); };
            this.OnSelectedFrameChanged += (x, y) => imageViewControl1.OnRedrawSelectedFrame(y, editorSettingsControl1);
            this.OnPaletteChanged += importSettingsControl1.OnPaletteChanged;
            this.OnPaletteChanged += (x, y) => { UpdateColorPaletteControls(); };
        }

        private void ImageViewHandler()
        {
            var import = ImportImage == null ? null : importSettingsControl1;
            if (import != null)
            {
                imageViewControl1.OnRedrawImportImage(import.Original,import.Modified, editorSettingsControl1);
            }
            else
            {
                imageViewControl1.OnRedrawSelectedFrame(SelectedFrame, editorSettingsControl1);
            }
        }

        private void UpdateColorPaletteControls()
        {
            colorPicker2561.Colors = Palette.GetWithBrightness(editorSettingsControl1.GameBrightnessMultiplier).ToArray();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_frmManager == null)
                throw new Exception("First, you should load palette in which file will be showed.");

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var model = LoadFrom(openFileDialog.FileName);
                SetEfaMode(model);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.FileName == null)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                SaveTo(openFileDialog.FileName);
            }
        }

        private void openPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openPaletteFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Palette = ImageHelper.LoadPalette(openPaletteFileDialog.FileName);
            }
        }

        private void OnModelChanged()
        {
            if (Model != null)
            {
                FrameListView.Items.Clear();
                var imageList = new ImageList();
                imageList.ImageSize = new Size(50, 50);
                foreach (var dir in Model.Directions)
                {
                    foreach (var frame in dir.Value.Frames)
                    {
                        var imgKey = imageList.Images.Count;
                        imageList.Images.Add(frame.Bitmap);
                        var lvi = new ListViewItem
                                  {
                                      Name = imgKey.ToString(),
                                      Text = frame.Id.ToString(),
                                      ImageIndex = imgKey,
                                      Tag = Tuple.Create(dir.Key, frame.Id),
                                      Group = FrameListView.Groups[(int)dir.Key]
                                  };
                        FrameListView.Items.Add(lvi);
                    }
                }
                FrameListView.LargeImageList = imageList;
                FrameListView.SmallImageList = imageList;
                FrameListView.Refresh();

                OnSelectedFrameChanged?.Invoke(this, SelectedFrame);
            }
        }


        private FrameModel GetFrame(FrmDirectionType dir, int id)
        {
            return Model.Directions[dir].Frames.First(x => x.Id == id);
        }


        private FrmModel LoadFrom(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case ".frm":
                    return _frmManager.LoadFromFrm(fileName);
                case ".efa":
                    return _frmManager.LoadFromEfa(fileName);
                default:
                    throw new Exception("Unknown file format.");
            }
        }

        private void SaveTo(string fileName)
        {
            if (Model == null && importSettingsControl1.Modified != null)
            {
                var img = importSettingsControl1.Modified;
                var model = _frmManager.ImportFrom(img);
                SetEfaMode(model);
            }

            if(Model == null)
                throw new Exception("Nothing to save.");

            var ext = Path.GetExtension(fileName).ToLower();
            switch (ext)
            {
                case ".frm":
                    _frmManager.SaveToFrm(fileName, Model);
                    break;
                case ".efa":
                    _frmManager.SaveToEfa(fileName, Model);
                    break;
                default:
                    throw new Exception("Unknown file format.");
            }
        }

        private void FrameListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedFrameChanged?.Invoke(this, SelectedFrame);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SetImportMode(openImageFileDialog.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveTo(saveFileDialog.FileName);
                openFileDialog.FileName = saveFileDialog.FileName;
            }
        }

        public void SetEfaMode(FrmModel model)
        {
            FrameListView.Clear();
            ImportImage = null;
            Model = model;
            importSettingsControl1.SetOriginalImage(null);
        }

        public void SetImportMode(string fileName)
        {
            FrameListView.Clear();
            ImportImage = (Bitmap)Image.FromFile(fileName);
            Model = null;
            importSettingsControl1.SetOriginalImage(ImportImage);
        }
    }
}
