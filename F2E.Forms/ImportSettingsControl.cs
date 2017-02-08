using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using F2E.Core;
using F2E.Frm.Dto;
using F2E.Frm.Filters;

namespace F2E.Forms
{
    public partial class ImportSettingsControl : UserControl
    {
        private Bitmap _original;

        private Bitmap _modified;

        private Palette _palette;

        public Bitmap Original => _original;

        public Bitmap Modified => _modified;

        public event EventHandler<ImportSettingsControl> OnSettingsChanged;

        public ImportSettingsControl()
        {
            InitializeComponent();
            var values = new[]
                         {
                             InterpolationMode.HighQualityBicubic,
                             InterpolationMode.HighQualityBilinear,
                             InterpolationMode.Default,
                             InterpolationMode.NearestNeighbor,
                             InterpolationMode.Bilinear,
                             InterpolationMode.High,
                             InterpolationMode.Bicubic,
                             InterpolationMode.Low
                         }
                .Distinct()
                .Cast<object>()
                .ToArray();
            InterpoationComboBox.Items.AddRange(values);
        }

        private bool _updating;
        public void SetOriginalImage(Bitmap img)
        {
            _updating = true;
            this.Visible = img != null;
            _original = img;

            if (_original != null)
            {
                WidthUpDown.Value = _original.Width;
                HeightUpDown.Value = _original.Height;
            }

            BrightnessUpDown.Value = 1;
            InterpoationComboBox.SelectedItem = InterpoationComboBox.Items[0];
            _updating = false;
            ApplyModify();
        }

        public void ApplyModify()
        {
            if(_updating)
                return;
            _modified = null;
            if (_original != null && _palette != null)
            {
                _modified = ImportImageIntoFalloutFormat(_original);
            }
            OnSettingsChanged?.Invoke(this, this);
        }

        private Bitmap ImportImageIntoFalloutFormat(Bitmap img)
        {
            var filters = new List<IFilter>();

            filters.Add(new FalloutAlphaFilter(true));

            if (WidthUpDown.Value != img.Width || HeightUpDown.Value != img.Height)
            {
                filters.Add(new ResizeFilter((InterpolationMode)InterpoationComboBox.SelectedItem,
                                             new Size((int)WidthUpDown.Value, (int)HeightUpDown.Value)));
            }

            if (BrightnessUpDown.Value != 1)
            {
                filters.Add(new BrightnessFilter((float)BrightnessUpDown.Value));
            }
            filters.Add(new FalloutAlphaFilter(true));
            filters.Add(new FalloutConvertFilter(_palette, (float)BrightnessTargetUpDown.Value));
            return img.Apply(filters);
        }

        public void OnPaletteChanged(object sender, Palette e)
        {
            _palette = e;
            ApplyModify();

        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            ApplyModify();
        }
    }
}
