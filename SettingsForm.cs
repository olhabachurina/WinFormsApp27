using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp27
{
    public partial class SettingsForm : Form
    {
        public Color FontColor { get; set; }
        public Color BackgroundColor { get; set; }
        public event EventHandler ColorsSelected;
        private readonly ColorDialog fontColorDialog = new ColorDialog();
        private readonly ColorDialog backgroundColorDialog = new ColorDialog();
        public Color SelectedFontColor => FontColor;
        public Color SelectedBackgroundColor => BackgroundColor;


        public SettingsForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        public void SetInitialColors(Color fontColor, Color backgroundColor)
        {
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        private void InitializeUI()
        {
            ContextMenuStrip colorMenu = new ContextMenuStrip();

            ToolStripMenuItem fontColorItem = new ToolStripMenuItem("Выбрать цвет шрифта");
            fontColorItem.Click += FontColorItem_Click;

            ToolStripMenuItem backgroundColorItem = new ToolStripMenuItem("Выбрать цвет фона");
            backgroundColorItem.Click += BackgroundColorItem_Click;

            colorMenu.Items.Add(fontColorItem);
            colorMenu.Items.Add(backgroundColorItem);

            // Создаем элементы управления для выбора цвета шрифта и фона
            System.Windows.Forms.Button colorButton = new System.Windows.Forms.Button();
            
            colorButton.Text = "Выбор цветов";
            colorButton.ContextMenuStrip = colorMenu;
            Controls.Add(colorButton);
            System.Windows.Forms.Button applyButton = new System.Windows.Forms.Button();
            applyButton.Text = "Применить";
            applyButton.Click += ApplyButton_Click;

            Controls.Add(applyButton);
        }

        private void BackgroundColorItem_Click(object sender, EventArgs e)
        {
            if (backgroundColorDialog.ShowDialog() == DialogResult.OK)
            {
                BackgroundColor = backgroundColorDialog.Color;
            }
        }

        private void FontColorItem_Click(object sender, EventArgs e)
        {
            if (fontColorDialog.ShowDialog() == DialogResult.OK)
            {
                FontColor = fontColorDialog.Color;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            ColorsSelected?.Invoke(this, EventArgs.Empty);
            DialogResult = DialogResult.OK;
            Close();
        }
        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ColorsSelected?.Invoke(this, EventArgs.Empty);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                // В этом обработчике передайте выбранные цвета обратно в Form1
                // Это произойдет при закрытии формы настроек.
                Form1 form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault() as Form1;
                if (form1 != null)
                {
                    form1.SetFontColor(FontColor);
                    form1.SetBackgroundColor(BackgroundColor);
                }
            }
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            // Сохраните выбранные цвета
            FontColor = fontColorDialog.Color;
            BackgroundColor = backgroundColorDialog.Color;

            // Вызовите событие ColorsSelected для уведомления Form1 о выбранных цветах
            ColorsSelected?.Invoke(this, EventArgs.Empty);
            Form1 form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault() as Form1;
            if (form1 != null)
            {
                form1.SetFontColor(FontColor);
            }
        }
    }
}