using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageEdit
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = this.folderBrowserDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var directory = this.folderBrowserDialog1.SelectedPath;
                var dirInfo = new DirectoryInfo(directory);
                CropImagesInDirectoryRecursive(dirInfo, dirInfo.Name);
            }
        }

        private void CropImagesInDirectoryRecursive(DirectoryInfo dirInfo, string rootDirName)
        {
            var split = extensions.Text.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            var trimmed = split.Select(e => e.Trim());
            foreach (var fileInfo in trimmed.SelectMany(dirInfo.GetFiles))
            {
                CropImage(fileInfo, rootDirName);
            }
            foreach (var directoryInfo in dirInfo.GetDirectories())
            {
                CropImagesInDirectoryRecursive(directoryInfo, rootDirName);
            }
        }

        private void CropImage(FileInfo fileInfo, string rootDirName)
        {
            var oldBitmap = new Bitmap(fileInfo.FullName);
            var cropX = int.Parse(this.x.Text);
            var cropY = int.Parse(this.y.Text);
            var cropWidth = int.Parse(this.width.Text);
            var cropHeight = int.Parse(this.height.Text);
            var rectangle = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            var newBitmap = CropImage(oldBitmap, rectangle);
            var newDir = fileInfo.Directory.FullName.Replace(rootDirName, rootDirName + "_New");
            Directory.CreateDirectory(newDir);
            newBitmap.Save(newDir+"\\" + fileInfo.Name);
        }

        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            var bmp = new Bitmap(section.Width, section.Height);

            var g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
    }
}
