using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    public partial class CaptureScreenForm : Form
    {
        private enum DrawAction
        {
            DrawFont,
            DrawRect,
            DrawCircle,
            DrawArrow,
            Reback,
            None
        }

        private Image _captureImage = null;

        private bool _isMouseDown = false;
        private Point _currentFormLocation = new Point(); //当前窗体位置
        private Point _currentMouseOffset = new Point(); //当前鼠标的按下位置

        private Font _font = new Font("宋体", 13);

        private Color _color = Color.Black;

        private string _drawString = string.Empty;

        private Point _startPoint = new Point();

        private Point _endPoint = new Point();

        private bool _isMove = false;

        private Stack<Bitmap> _drawRecord = new Stack<Bitmap>();

        private DrawAction _drawAction;

        private int _lineWidth = 1;

        public CaptureScreenForm(Image img)
        {
            InitializeComponent();
            _captureImage = img;
        }

        private void CaptureScreenForm_Load(object sender, EventArgs e)
        {
            //this.Height = _captureImage.Height / 2;
            //this.Width = _captureImage.Width / 2;
            picCaptureImage.Image = _captureImage;
            _drawAction = DrawAction.None;
            txtFontText.Hide();
        }

        private void picCaptureImage_Paint(object sender, PaintEventArgs e)
        {
            if (_drawRecord.Count > 0)
            {
                e.Graphics.DrawImage(_drawRecord.Peek(), new Point(0, 0));
            }
            switch (_drawAction)
            {
                case DrawAction.DrawCircle:
                    {
                        e.Graphics.DrawEllipse(new Pen(new SolidBrush(_color), _lineWidth), GetNormalizedRectangle(_startPoint, _endPoint));
                        break;
                    }
                case DrawAction.DrawFont:
                    {
                        if (_drawString != "")
                        {
                            e.Graphics.DrawString(_drawString, _font, new SolidBrush(_color), txtFontText.Location.X, txtFontText.Location.Y - txtFontText.Height);
                            _drawString = "";
                            
                        }
                        break;
                    }
                case DrawAction.DrawRect:
                    {
                        e.Graphics.DrawRectangle(new Pen(new SolidBrush(_color), _lineWidth), GetNormalizedRectangle(_startPoint, _endPoint));
                        break;
                    }
                case DrawAction.DrawArrow:
                    {
                        AdjustableArrowCap lineCap = new AdjustableArrowCap(6, 6, true);
                        Pen arrowPen = new Pen(_color);
                        arrowPen.Width = _lineWidth * 3;
                        arrowPen.StartCap = LineCap.Round;
                        arrowPen.CustomEndCap = lineCap;
                        e.Graphics.DrawLine(arrowPen, _startPoint, _endPoint);
                        break;
                    }
            }
            
            
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //RecordImage();
                _isMouseDown = true;
                _currentFormLocation = this.Location;
                _currentMouseOffset = Control.MousePosition;
               
            }
        }

        private void menuBar_MouseMove(object sender, MouseEventArgs e)
        {
            int rangeX = 0, rangeY = 0; //计算当前鼠标光标的位移，让窗体进行相同大小的位移
            if (_isMouseDown)
            {
                Point pt = Control.MousePosition;
                rangeX = _currentMouseOffset.X - pt.X;
                rangeY = _currentMouseOffset.Y - pt.Y;
                this.Location = new Point(_currentFormLocation.X - rangeX, _currentFormLocation.Y - rangeY);

            }
        }

        private void menuBar_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }

        private void menuBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }


        private void tsmiActionButton_Click(object sender,EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
           
            picCaptureImage.Cursor = Cursors.Cross;
            switch(item.Tag.ToString())
            {
                case "1":
                    {
                        picCaptureImage.Cursor = Cursors.IBeam;
                        _drawAction = DrawAction.DrawFont;
                        break;
                    }
                case "2":
                    {
                        _drawAction = DrawAction.DrawRect;
                        break;
                    }
                case "3":
                    {
                        _drawAction = DrawAction.DrawCircle;
                        break;
                    }
                case "4":
                    {
                        _drawAction = DrawAction.DrawArrow;
                        break;
                    }
                case "5":
                    {
                        picCaptureImage.Cursor = Cursors.Default;
                        if (_drawAction != DrawAction.Reback)
                        {
                            _drawRecord.Pop();
                            picCaptureImage.Invalidate();
                            _drawAction = DrawAction.Reback;
                        }
                        else
                        {
                            if (_drawRecord.Count > 0)
                            {
                                _drawRecord.Pop();
                                picCaptureImage.Invalidate();
                            }
                        }


                        break;
                    }
            }
        }

        private void tsmiSelectFont_Click(object sender, EventArgs e)
        {
            if(fontDialog.ShowDialog()== DialogResult.OK)
            {
                _font = fontDialog.Font;
            }
        }

        private void tsmiSelectColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog()== DialogResult.OK)
            {
                _color = colorDialog.Color;
            }
        }

        private void picCaptureImage_MouseDown(object sender, MouseEventArgs e)
        {
            
            switch (_drawAction)
            {
                case DrawAction.DrawFont:
                    {
                        if (txtFontText.Text.Trim() != "")
                        {
                            txtFontText.Visible = false;
                        }
                        else
                        {

                            txtFontText.Location = new Point(e.X, e.Y + txtFontText.Height - 10);
                            txtFontText.Visible = true;
                            txtFontText.Text = string.Empty;
                            txtFontText.Focus();
                        }
                        break;
                    }
                case DrawAction.DrawCircle:
                case DrawAction.DrawRect:
                case DrawAction.DrawArrow:
                    {
                        
                        _startPoint = new Point(e.X, e.Y);
                        _isMove = true;
                        break;
                    }
            }
           
        }

        private void RecordImage()
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(150);
                Bitmap img = new Bitmap(picCaptureImage.Image, new Size(picCaptureImage.Width, picCaptureImage.Height));
                Graphics graph = Graphics.FromImage(img);
                graph.CopyFromScreen(new Point(picCaptureImage.Location.X, picCaptureImage.Location.Y), new Point(0, 0), new Size(picCaptureImage.Width, picCaptureImage.Height));
                _drawRecord.Push(img);
                graph.Dispose();
                graph = null;

            });
           
        }

        private void txtFontText_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DrawText();
                
            }
            else if(e.KeyCode== Keys.Escape)
            {
                txtFontText.Text = "";
                txtFontText.Hide();
            }

        }

        private void DrawText()
        {
            
            if (txtFontText.Text.Trim() != "")
            {
                _drawString = txtFontText.Text;
                txtFontText.Text = string.Empty;
                txtFontText.Visible = false;
                picCaptureImage.Invalidate();

                RecordImage();
                
                
            }
        }

        private Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        private Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        private Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                x1 ^= x2;
                x2 ^= x1;
                x1 ^= x2;
            }

            if (y2 < y1)
            {
                y1 ^= y2;
                y2 ^= y1;
                y1 ^= y2;
            }

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        private void picCaptureImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (_drawAction == DrawAction.DrawRect || _drawAction == DrawAction.DrawCircle||_drawAction== DrawAction.DrawArrow)
            {
                _endPoint = new Point(e.X, e.Y);
                _isMove = false;
                picCaptureImage.Invalidate();
                RecordImage();

            }
        }

        private void picCaptureImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMove && (_drawAction == DrawAction.DrawCircle || _drawAction == DrawAction.DrawRect||_drawAction== DrawAction.DrawArrow))
            {
                _endPoint = new Point(e.X, e.Y);
                picCaptureImage.Invalidate();

            }
        }

        private void tscbxLineWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lineWidth = tscbxLineWidth.SelectedItem != null ? int.Parse(tscbxLineWidth.SelectedItem.ToString()) : 1;
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = new Bitmap(picCaptureImage.Image, new Size(picCaptureImage.Width, picCaptureImage.Height));
                Graphics graph = Graphics.FromImage(img);
                graph.CopyFromScreen(new Point(picCaptureImage.Location.X, picCaptureImage.Location.Y), new Point(0, 0), new Size(picCaptureImage.Width, picCaptureImage.Height));
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.DefaultExt = "png";
                dialog.Filter = "PNG Image (*.png)|*.png|GIF Image (*.gif)|*.gif|JPEG Image File (*.jpg)|*.jpg|Bitmaps (*.bmp)|*.bmp";
                dialog.FileName = "波形截图-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    img.Save(dialog.FileName, ImageFormat.Png);
                    graph.Dispose();
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("保存截图失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("保存截图失败:" + ex.Message);
            }
        }

        private void txtFontText_VisibleChanged(object sender, EventArgs e)
        {
            
            //if (txtFontText.Visible == false && txtFontText.Text.Trim() != "")
            //{
            //    //RecordImage();
            //    picCaptureImage.Invalidate();
            //}
        }

    }
}
