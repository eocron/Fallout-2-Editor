using System;
using System.Linq;
using System.Windows.Forms;

namespace F2E.Forms
{
    public enum BrightnessType
    {
        Night = 1,
        Morning,
        Forenoon,
        Highnoon,
    }

    public partial class EditorSettingsControl : UserControl
    {
        public int GameBrightnessMultiplier => (int)TimeOfDayInGameComboBox.SelectedItem;

        public event EventHandler<EditorSettingsControl> OnSettingsChanged; 

        public EditorSettingsControl()
        {
            InitializeComponent();
            var values = new[]
                         {
                             BrightnessType.Night,
                             BrightnessType.Morning, 
                             BrightnessType.Forenoon, 
                             BrightnessType.Highnoon, 
                         }
                .Distinct()
                .Cast<object>()
                .ToArray();
            TimeOfDayInGameComboBox.Items.AddRange(values);
            TimeOfDayInGameComboBox.SelectedItem = values[2];
        }

        private void TimeOfDayInGameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSettingsChanged?.Invoke(this, this);
        }
    }
}
