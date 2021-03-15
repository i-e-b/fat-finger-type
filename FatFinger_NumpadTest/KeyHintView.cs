using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FatFinger_NumpadTest {
	public partial class KeyHintView : Control {
		public KeyMap LinkedKeyMap { get; set; }

		private int first_down, last_up;

		public int FirstDown {
			get { return first_down; }
			set { 
				first_down = value;
			}
		}
		public int LastUp {
			get { return last_up; }
			set {
				last_up = value;
			}
		}

		public KeyHintView () {
			InitializeComponent();
			FirstDown = -1;
			LastUp = -1;
		}

		protected override void OnPaint (PaintEventArgs pe) {
			//base.OnPaint(pe);
			Graphics g = pe.Graphics;

			if (LinkedKeyMap == null) LinkedKeyMap = new KeyMap(); // should be "return;" but have this for testing

			if (FirstDown < 0 || FirstDown > 8) DrawBase(g);
			else DrawZoomed(g);
		}

		/// <summary>
		/// Draws a zoomed keymap (when a key has been pressed)
		/// </summary>
		private void DrawZoomed (Graphics g) {
			Rectangle[,] outer = GridNine(new Rectangle(0, 0, Bounds.Width-1, Bounds.Height-1));

			int size = Math.Min(Width, Height) / 3;
			size -= 4;
			Font fnt = new Font("Courier New", size, FontStyle.Regular, GraphicsUnit.Pixel); 
			Pen p = Pens.Black;
			Brush b = Brushes.Black;
			Brush sel = new SolidBrush(Color.FromArgb(200,200,255));

			for (int y = 0; y < 3; y++) {
				for (int x = 0; x < 3; x++) {
					int idx = (3 * y) + x;
					string caption = LinkedKeyMap.KeyChar(FirstDown, idx).ToString();

					if (idx == last_up) {
						g.FillRectangle(sel, outer[x, y]);
					}

					g.DrawRectangle(p, outer[x, y]);
					g.DrawString(caption.Replace("_", "̳"), fnt, b, outer[x, y]);
				}
			}
		}

		/// <summary>
		/// Draws the entire keymap
		/// </summary>
		private void DrawBase (Graphics g) {
			Rectangle[,] outer = GridNine(new Rectangle(0,0, Bounds.Width-1, Bounds.Height-1));

			int size = Math.Min(Width, Height) / 9;
			size -= 2;
			Font fnt = new Font("Courier New", size, FontStyle.Regular, GraphicsUnit.Pixel);
			Pen p = Pens.Black;
			Brush b = Brushes.Black;

			// outer set
			for (int oy = 0; oy < 3; oy++) {
				for (int ox = 0; ox < 3; ox++) {

					// inner set
					g.DrawRectangle(p, outer[ox, oy]);
					Rectangle[,] inner = GridNine(outer[ox, oy]);
					for (int iy = 0; iy < 3; iy++) {
						for (int ix = 0; ix < 3; ix++) {
							string caption = LinkedKeyMap.KeyChar((3 * oy) + ox, (3 * iy) + ix).ToString();

							g.DrawString(caption.Replace("_", "̳"), fnt, b, inner[ix, iy]);
						}
					}

				}
			}
		}

		/// <summary>
		/// Takes a rectangle, and subdivides it into 9 equal subrectangles in a 3x3 grid.
		/// </summary>
		private Rectangle[,] GridNine (Rectangle r) {
			double width = r.Width / 3.0;
			double height = r.Height / 3.0;

			Rectangle[,] result = new Rectangle[3, 3];

			for (int y = 0; y < 3; y++) {
				for (int x = 0; x < 3; x++) {
					result[x, y] = new Rectangle(r.X + (int)(x * width), r.Y + (int)(y * height), (int)width, (int)height);
				}
			}

			return result;
		}
	}
}
