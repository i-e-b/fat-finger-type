using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FatFinger_NumpadTest {
	public partial class Form1 : Form {

		private List<int> KeyCoords;
		private int keys_down;
		KeyMap km;
		UserActivityHook actHook;
		KeyMap.Modifiers mods;
		

		public Form1 () {
			InitializeComponent();
			KeyCoords = new List<int>(10);
			km = new KeyMap();
			keys_down = 0;
			mods = KeyMap.Modifiers.None;
			actHook = new UserActivityHook(false, true);

			actHook.KeyDown += new KeyEventHandler(Form1_KeyDown);
			actHook.KeyUp += new KeyEventHandler(Form1_KeyUp);

			keyHintView.LinkedKeyMap = km;
		}

		private void Form1_KeyDown (object sender, KeyEventArgs e) {
			if (!MainKey(e.KeyCode) && !ModeKey(e.KeyCode)) return;
			e.SuppressKeyPress = true;

			int coord = KeyCoord(e.KeyCode);

			if (KeyCoords.Count < 1 || coord != KeyCoords.Last()) {
				keys_down++; // avoid repeat screwing things up.
			}

			KeyCoords.Add(KeyCoord(e.KeyCode));

			keyHintView.FirstDown = KeyCoords.First();
			keyHintView.LastUp = KeyCoords.Last();
			keyHintView.Invalidate();
		}

		private void Form1_KeyUp (object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.NumLock) { // special case -- must reset.
				ResetKeys();
				return;
			}

			if (!MainKey(e.KeyCode) && !ModeKey(e.KeyCode)) return;
			e.SuppressKeyPress = true;
			keys_down--;


			if (keys_down <= 0) {
				if (KeyCoords.Count < 1) return; // weird condition caused by having keys down during startup
				int first_key = KeyCoords.First();
				int last_key = KeyCoords.Last();

				if (first_key >= 0 && last_key >= 0) {
					km.SendWindowsKeystroke(first_key, last_key, mods);
					if (shiftCheck.CheckState == CheckState.Indeterminate) {
						shiftCheck.Checked = false;
					}
				} else if (first_key == last_key) {
					UpdateModChecks (km.SetNewMode(last_key));
					UpdateModifiersFromChecks(null, null);
					UpdateModeRadios();
				} else {
					// mode key was canceled, or bad mapping.
				}

				ResetKeys();
			}
		}


		/// <summary>
		/// Call this when a Modifier mapped physical key is pressed, to update states
		/// </summary>
		private void UpdateModChecks (KeyMap.Modifiers newmods) {
			if ((newmods & KeyMap.Modifiers.AltKey) != 0) {
				altCheck.Checked = !altCheck.Checked;
			}
			if ((newmods & KeyMap.Modifiers.CtrlKey) != 0) {
				ctrlCheck.Checked = !ctrlCheck.Checked;
			}

			if ((newmods & KeyMap.Modifiers.ShiftKey) != 0) {
				if (shiftCheck.CheckState == CheckState.Indeterminate) {
					shiftCheck.CheckState = CheckState.Checked;
				} else if (shiftCheck.Checked) {
					shiftCheck.Checked = false;
				} else {
					shiftCheck.CheckState = CheckState.Indeterminate;
				}
			}
		}

		/// <summary>
		/// Call this when checks are changed to update the modifier state
		/// </summary>
		private void UpdateModifiersFromChecks (object sender, EventArgs e) {
			mods = KeyMap.Modifiers.None;
			if (altCheck.Checked) mods |= KeyMap.Modifiers.AltKey;
			if (ctrlCheck.Checked) mods |= KeyMap.Modifiers.CtrlKey;
			if (shiftCheck.Checked) mods |= KeyMap.Modifiers.ShiftKey;
		}

		private void ResetKeys () {
			keys_down = 0;
			KeyCoords.Clear();
			keyHintView.FirstDown = -1;
			keyHintView.LastUp = -1;
			keyHintView.Invalidate();
		}

		private int KeyCoord(Keys press) {
			switch (press) {
				case Keys.NumPad1: return 6;
				case Keys.NumPad2: return 7;
				case Keys.NumPad3: return 8;
				case Keys.NumPad4: return 3;
				case Keys.NumPad5: return 4;
				case Keys.NumPad6: return 5;
				case Keys.NumPad7: return 0;
				case Keys.NumPad8: return 1;
				case Keys.NumPad9: return 2;

				case Keys.NumPad0: return (int)(KeyMap.MapMode.Alpha); // Alpha / numeric switch
				case Keys.Multiply: return (int)(KeyMap.MapMode.Symbols); // Symbols mode.
				case Keys.Subtract: return (int)(KeyMap.MapMode.Accents); // Accents
				case Keys.Add: return -(int)(KeyMap.Modifiers.ShiftKey); // Shift key / mode
				case Keys.Decimal: return -(int)(KeyMap.Modifiers.CtrlKey); // Ctrl key
				//case Keys.Enter: return -(int)(KeyMap.Modifiers.AltKey); // Alt key
				//case Keys.Divide: return -1;	// Nothing as yet
				default: return -1;
			}
		}

		private bool ModeKey (Keys press) {
			switch (press) {
				case Keys.NumPad0: // Alpha / numeric switch
				//case Keys.Divide:	// Nothing as yet
				case Keys.Multiply: // Symbols mode.
				case Keys.Subtract: // Accents
				case Keys.Add:		// Shift key / mode
				case Keys.Decimal:	// Ctrl key
				//case Keys.Enter:	// Alt key
					return true;
				default: return false;
			}
		}

		private bool MainKey(Keys press) {
			switch (press) {
				case Keys.NumPad1:
				case Keys.NumPad2:
				case Keys.NumPad3:
				case Keys.NumPad4:
				case Keys.NumPad5:
				case Keys.NumPad6:
				case Keys.NumPad7:
				case Keys.NumPad8:
				case Keys.NumPad9: return true;
				default: return false;
			}
		}

		private void quitButton_Click (object sender, EventArgs e) {
			this.Close();
		}

		private void ClearModeRadios () {
			alphaRadio.Checked = false;
			numericRadio.Checked = false;
			accentsRadio.Checked = false;
			symbolsRadio.Checked = false;
		}


		/// <summary>
		/// Update radio button states based on current KeyMap mode
		/// </summary>
		private void UpdateModeRadios () {
			ClearModeRadios();
			switch (km.CurrentMode) {
				default:
				case KeyMap.MapMode.Alpha:
					alphaRadio.Checked = true;
					break;
				case KeyMap.MapMode.Numeric:
					numericRadio.Checked = true;
					break;
				case KeyMap.MapMode.Accents:
					accentsRadio.Checked = true;
					break;
				case KeyMap.MapMode.Symbols:
					symbolsRadio.Checked = true;
					break;
			}
		}
		
		private void ModeChange (object sender, EventArgs e) {
			if (alphaRadio.Checked) {
				km.CurrentMode = KeyMap.MapMode.Alpha;
			} else if (numericRadio.Checked) {
				km.CurrentMode = KeyMap.MapMode.Numeric;
			} else if (accentsRadio.Checked) {
				km.CurrentMode = KeyMap.MapMode.Accents;
			} else if (symbolsRadio.Checked) {
				km.CurrentMode = KeyMap.MapMode.Symbols;
			} else {
				// Error! Reset!
				/*ClearModeRadios();
				alphaRadio.Checked = true;
				km.CurrentMode = KeyMap.MapMode.Alpha;*/
			}
			keyHintView.Invalidate();
		}

		private void keyHintView_Resize (object sender, EventArgs e) {
			keyHintView.Invalidate();
		}
	}
}
