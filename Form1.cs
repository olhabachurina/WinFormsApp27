
using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;




namespace WinFormsApp27
{
    public partial class Form1 : Form
    {
        private string currentFilePath = null;
        private TextBox textBox;
        private ColorDialog fontColorDialog;
        private ColorDialog backgroundColorDialog;
        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }
        private void InitializeUI()
        {
            textBox = new TextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            Controls.Add(textBox);

            ToolStrip toolstrip = new ToolStrip();
            ToolStripButton openButton = new ToolStripButton("�������");
            ToolStripButton saveButton = new ToolStripButton("���������");
            ToolStripButton newDocumentButton = new ToolStripButton("����� ��������");
            ToolStripButton copyButton = new ToolStripButton("����������");
            ToolStripButton cutButton = new ToolStripButton("��������");
            ToolStripButton pasteButton = new ToolStripButton("��������");
            ToolStripButton undoButton = new ToolStripButton("��������");
            ToolStripButton settingsButton = new ToolStripButton("��������� ���������");
            settingsButton.Font = new Font(settingsButton.Font.FontFamily, 14, FontStyle.Bold);

            toolstrip.Items.AddRange(new ToolStripItem[] { openButton, saveButton, newDocumentButton, copyButton, cutButton, pasteButton, undoButton, settingsButton });

            openButton.Click += OpenButton_Click;
            saveButton.Click += SaveButton_Click;
            newDocumentButton.Click += NewDocumentButton_Click;
            copyButton.Click += CopyButton_Click;
            cutButton.Click += CutButton_Click;
            pasteButton.Click += PasteButton_Click;
            undoButton.Click += UndoButton_Click;
            settingsButton.Click += SettingsButton_Click;
            Controls.Add(toolstrip);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("����������");
            ToolStripMenuItem cutMenuItem = new ToolStripMenuItem("��������");
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("��������");
            ToolStripMenuItem undoMenuItem = new ToolStripMenuItem("��������");

            copyMenuItem.Click += CopyButton_Click;
            cutMenuItem.Click += CutButton_Click;
            pasteMenuItem.Click += PasteButton_Click;
            undoMenuItem.Click += UndoButton_Click;

            contextMenu.Items.AddRange(new ToolStripItem[] { copyMenuItem, cutMenuItem, pasteMenuItem, undoMenuItem });

            textBox.ContextMenuStrip = contextMenu;

            Text = "��������� ��������";
            Size = new System.Drawing.Size(800, 600);
            fontColorDialog = new ColorDialog();
            backgroundColorDialog = new ColorDialog();
        }

        private void UpdateFormTitle()
        {
            if (currentFilePath != null)
            {
                Text = $"��������� �������� - {currentFilePath}";
               
            }
            else
            {
                Text = "��������� �������� - ����� ��������";
                            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                UpdateFormTitle();
                
                if (File.Exists(currentFilePath))
                {
                    string fileContent = File.ReadAllText(currentFilePath);
                    
                    textBox.Text = fileContent;
                                    }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*";

            if (currentFilePath != null)
            {
                File.WriteAllText(currentFilePath, textBox.Text);
                
            }
            else
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    File.WriteAllText(currentFilePath, textBox.Text);
                    Text = $"��������� �������� - {currentFilePath}";
                    
                }
            }
        }

        private void NewDocumentButton_Click(object sender, EventArgs e)
        {
            currentFilePath = null;
            Text = "��������� �������� - ����� ��������";
            textBox.Text = "";
            
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            textBox.Cut();
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            textBox.Undo();
        }

        public void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ColorsSelected += (s, e) =>
            {
                SetFontColor(settingsForm.SelectedFontColor);
                SetBackgroundColor(settingsForm.SelectedBackgroundColor);
            };

            settingsForm.ShowDialog();
        }

        public void SetFontColor(Color fontColor)
        {
            textBox.ForeColor = fontColor;
        }

        public void SetBackgroundColor(Color backgroundColor)
        {
            textBox.BackColor = backgroundColor;
        }
    }
}

