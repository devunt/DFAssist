using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    public class TabControlBlack : TabControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Parent.BackColor);
            e.Graphics.FillRectangle(Brushes.White, 4, 4, ItemSize.Height - 4, Height - 8);

            int inc = 0;

            foreach(TabPage tp in TabPages)
            {
                Color fore = Color.Black;
                Font fontF = Font;
                Rectangle tabrect = GetTabRect(inc), rect = new Rectangle(tabrect.X + 4, tabrect.Y + 4, tabrect.Width - 8, tabrect.Height - 2), textrect = new Rectangle(tabrect.X + 4, tabrect.Y + 4, tabrect.Width - 8, tabrect.Height - 4);

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                if (inc == SelectedIndex)
                {
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), rect);
                    fore = SystemColors.HighlightText;
                    fontF = new Font(Font, FontStyle.Bold);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.White, rect);
                }

                e.Graphics.DrawString(tp.Text, fontF, new SolidBrush(fore), textrect, sf);
                inc++;
            }
        }

        protected override void OnTabIndexChanged(EventArgs e)
        {
            base.OnTabIndexChanged(e);
            Invalidate();
        }

        public TabControlBlack() : base()
        {
            Alignment = TabAlignment.Left;
            
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            DoubleBuffered = true;

            ItemSize = new Size(30, 110);
            SizeMode = TabSizeMode.Fixed;
        }
    }
}
