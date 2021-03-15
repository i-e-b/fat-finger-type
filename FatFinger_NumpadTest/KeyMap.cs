#undef ONE_FINGER
#define THREE_FINGERS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FatFinger_NumpadTest {
	public class KeyMap {
		#region PInvoke crap
		[StructLayout(LayoutKind.Sequential)]
		private struct KEYBOARD_INPUT {
			public uint type;
			public ushort vk;
			public ushort scanCode;
			public uint flags;
			public uint time;
			public uint extrainfo;
			public uint padding1;
			public uint padding2;
		}
		const uint INPUT_KEYBOARD = 1;
		const int KEY_EXTENDED = 0x0001;
		const uint KEY_UP = 0x0002;
		const uint KEY_SCANCODE = 0x0004;

		[DllImport("User32.dll")]
		private static extern uint SendInput (uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] KEYBOARD_INPUT[] input, int structSize);

		private void sendKey (int VirtualKey, bool press) {
			KEYBOARD_INPUT[] input = new KEYBOARD_INPUT[1];
			input[0] = new KEYBOARD_INPUT();
			input[0].type = INPUT_KEYBOARD;
			input[0].flags = 0;// KEY_SCANCODE; // using virtual key codes!

			if ((VirtualKey & 0xFF00) == 0xE000) { // extended key?
				input[0].flags |= KEY_EXTENDED;
			}

			input[0].scanCode = 0;
			input[0].vk = (ushort)VirtualKey;

			if (press) { // press?
				//input[0].scanCode = (ushort)(scanCode & 0xFF);
			} else { // release?
				//input[0].scanCode = (ushort)(scanCode);
				input[0].flags |= KEY_UP;
			}

			uint result = SendInput(1, input, Marshal.SizeOf(input[0]));

			if (result != 1) {
				throw new Exception("Could not send key: " + VirtualKey);
			}
		}
		#endregion
		public enum MapMode {
			Alpha = -10,
			Numeric = -20,
			Accents = -30,
			Symbols = -40,
		}

		public enum Modifiers {
			None = 0,
			ShiftKey = 64,
			CtrlKey = 128,
			AltKey = 256
		}

		public MapMode CurrentMode { get; set; }

		/// <summary>
		/// Change the character map based on mode-key press.
		/// Also returns any modifier key presses
		/// </summary>
		public Modifiers SetNewMode (int ModeKeyCoord) {
			Modifiers m = Modifiers.None;
			switch (ModeKeyCoord) {
				case (int)(KeyMap.MapMode.Alpha): // Alpha / numeric switch
					{
						if (CurrentMode == MapMode.Alpha) CurrentMode = MapMode.Numeric;
						else CurrentMode = MapMode.Alpha;
						break;
					}
				case (int)(KeyMap.MapMode.Symbols): // Symbols mode.
					CurrentMode = MapMode.Symbols;
					break;
				case (int)(KeyMap.MapMode.Accents): // Accents
					CurrentMode = MapMode.Accents;
					break;
				case -(int)(KeyMap.Modifiers.ShiftKey): // Shift key / mode
					m |= Modifiers.ShiftKey;
					break;
				case -(int)(KeyMap.Modifiers.CtrlKey): // Ctrl key
					m |= Modifiers.CtrlKey;
					break;
				case -(int)(KeyMap.Modifiers.AltKey): // Alt key
					m |= Modifiers.AltKey;
					break;

				default: return m;
			}
			return m;
		}

		public KeyMap () {
			CurrentMode = MapMode.Alpha;
		}

		public void SendWindowsKeystroke (int down, int up, Modifiers mods) {
			if (down < 0 || down > 8 || up < 0 || up > 8) return; // invalid key

			int vk = VirtualKey(down, up);
			string sk = KeyChar(down, up).ToString();

			if (vk == 0) {
				System.Windows.Forms.SendKeys.Send(CleanUpSK(sk)); // non-VK keys don't get mods.
				return;
			} else {
				try {
					// ALT isn't working yet
					//if ((mods & Modifiers.AltKey) != 0) vk += (int)Keys.Alt;

					if ((mods & Modifiers.ShiftKey) != 0) sendKey((int)Keys.ShiftKey, true);
					if ((mods & Modifiers.CtrlKey) != 0) sendKey((int)Keys.ControlKey, true);

					sendKey(vk, true); // press
					sendKey(vk, false); // release


					if ((mods & Modifiers.CtrlKey) != 0) sendKey((int)Keys.ControlKey, false);
					if ((mods & Modifiers.ShiftKey) != 0) sendKey((int)Keys.ShiftKey, false);
				} catch {
					System.Windows.Forms.SendKeys.Send("X?");
				}
			}
		}

		private string CleanUpSK (string sk) {
			return "{" + sk + "}";
		}


		/// <summary>
		/// Return the Virtual Key code for a key position
		/// </summary>
		public char VirtualKey (int down, int up) {
			switch (CurrentMode) {
				case MapMode.Alpha:
					return (char)LowerCase_VKs[down, up];
				case MapMode.Numeric:
					return (char)Numbers_VKs[down, up];
				case MapMode.Accents:
					return (char)Accent_VKs[down, up];
				case MapMode.Symbols:
					return (char)Symbol_VKs[down, up];
				default: return (char)(0);
			}
		}

		/// <summary>
		/// Return the Display Character for a key position
		/// </summary>
		public char KeyChar (int down, int up) {
			switch (CurrentMode) {
				case MapMode.Alpha:
					return (char)LowerCase_Chs[down, up];
				case MapMode.Numeric:
					return (char)Numbers_Chs[down, up];
				case MapMode.Accents:
					return (char)Accent_Chs[down, up];
				case MapMode.Symbols:
					return (char)Symbol_Chs[down, up];
				default: return (char)(0);
			}
		}

		#region Layout for single finger sliding
#if ONE_FINGER
		static int[,] LowerCase_VKs = {
			{'A','S','K',
			 'M', 0 , 0,
			 'Q', 0 , 0},

			/*{VK_BACK,VK_BACK,VK_DELETE,0,VK_RETURN,0,0,VK_RETURN,0},*/
			{0x08,0x08,0x7F,0,0x0D,0,0,0x0D,0},

			{'P','F','N',
			  0,  0 ,'L',
			  0,  0 ,'X'},

			{'H',0,0,
			 'E',0,0,
			 'C',0,0},

			/*{0,VK_UP,0,
			VK_LEFT, ' ',VK_RIGHT,
			0,VK_DOWN,0},*/
			{0,   0x26 ,0,
			 0x25,' ',  0x27,
			 0,   0x28 ,0},

			{0,0,'U',
			 0,0,'T',
			 0,0,'Y'},

			{'V', 0 , 0,
			 'W', 0 , 0,
			 'O','G','Z'},

			{0,0,0,
			0,0,0,
			0,0,0},

			{0 , 0 ,'J',
			 0 , 0 ,'R',
			'B','D','I'}
		};
		static int[,] LowerCase_Chs = {
			{'A','S','K',
			 'M',0,0,
			 'Q',0,'\''},

			{'‹',0,'›',0,0,0,0,'¶',0},

			{'P','F','N',
			0,0,'L',
			'"',0,'X'},

			{'H',0,0,
			'E','(','-',
			'C',0,0},

			{0,0,0,
			0,' ',0,
			0,0,0},

			{0,0,'U',
			'_',')','T',
			0,0,'Y'},

			{'V',0,0,
			'W',0,0,
			'O','G','Z'},

			{0,0,0,
			0,'!',0,
			'?','.',','},

			{0,0,'J',
			0,0,'R',
			'B','D','I'}
		};
#endif
		#endregion

		#region Layout for 3-finger tapping (numeric pad layout)
#if THREE_FINGERS
		static int[,] LowerCase_VKs = {
			/*{VK_BACK,VK_BACK,VK_DELETE,0,VK_RETURN,0,0,VK_RETURN,0},
			{0x08,0x08,0x7F,0,0x0D,0,0,0x0D,0},*/

			{'A','F','W',
			 'Q', 0 , 0,
			  0 , 0 , 0},

			{'U', 'H' ,'B',
			  0,  0 , 0,
			  0,  0 , 0},

			{'C','R','O',
			  0,  0,  0,
			 0, 0,  0},


			{0, 0,  0,
			 'E','L','D',
			 0, 0,  0},

			{ 0, (int)Keys.Tab, 0,
			 (int)Keys.Back,' ',0,
			  0, (int)Keys.Return, 0},

			{0,0,0,
			'M','P','T',
			0,0,0},

			{0,0,0,
			'X',0,0,
			'I','V','G'},

			{0, 0, 0,
			 0, 0, 0,
			'K','S','Z'},

			{0,0,0,
			0,0,0,
			'Y','J','N'}


		};
		static int[,] LowerCase_Chs = {
			{'A','F','W',
			 'Q', 0 , 0,
			  0 , 0 , '\''},

			{'U', 'H' ,'B',
			  0,   0  , 0,
			  0,   0  , 0},

			{'C','R','O',
			  0,  0,  0,
			 '"', 0,  0},


			{'(', 0,  0,
			 'E','L','D',
			 ')', 0,  0},

			{ 0, '→', 0,
			 '‹',' ','-',
			  0, '¶', 0},

			{0,0,0,
			'M','P','T',
			0,0,0},

			{0,0,'.',
			'X',0,0,
			'I','V','G'},

			{0, '!', 0,
			 0, '?', 0,
			'K','S','Z'},

			{',',0,0,
			0,0,0,
			'Y','J','N'}
		};

		static int[,] Numbers_VKs = {
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,(int)Keys.Back,(int)Keys.Delete,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0}
		};
				static int[,] Numbers_Chs = {
			{'1', '2',0,
			0,0,0,
			 0,0,0},

			{'4','3',0,
			 0,0,0,
			 0,0,0},

			{0,'6','5',
			 '‹','›',0,
			 0,0,0},

			{0,0,0,
			 '7','8',0,
			 0,0,0},

			{0,0,0,
			'9','0','_',
			0,0,0},

			{0,0,'\'',
			0,'–','.',
			0,0,','},

			{0,0,0,
			'\\',0,0,
			'/','>','{'},

			{'#','×','%',
			0,'÷',0,
			'[','+',']'},

			{0,0,0,
			0,0,':',
			'}','<',';'}
		};

				static int[,] Accent_VKs = {
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0}
		};
				static int[,] Accent_Chs = {
			{'à','á','â',  // àáâãäåæąă
			 'ã','ä','å',
			 'æ','ą','ă'},

			{'è','é','ê',  // èéêëėěęȩə  œǽ
			 'ë','ė','ě',
			 'ǽ','œ','ę'},

			{'ì','í','î',  // ìíîïĩīĭįı
			 'ï','ĩ','ī',
			 'ĭ','į','ı'},

			{'ò','ó','ô',  // òóôõöøōŏő
			 'õ','ö','ø',
			 'ō','ŏ','ő'},

			{'ù','ú','û',  // ùúûüũūŭůű
			 'ü','ũ','ū',
			 'ŭ','ů','ű'},

			{'ń','ñ','ŋ',  // ćĉċčďđĝĵņňŉŋñńğġģĥħ
			 'ŉ','ň','ņ',
			 'ğ','ĝ','ģ'},

			{'ć','ĉ','ċ',
			 'č','ç','đ',
			 'ĥ','ħ','ð'},

			{'ķ','ĸ','Þ',  // ķĸ Þß ţť ýÿŷ 
			 'ß','ţ','ť',
			 'ý','ÿ','ŷ'},

			{'ź','ż','ž',  // źżžśŝşš ſƒ
			 'ś','ŝ','ş',
			 'š','ſ','ƒ'}
		};

				static int[,] Symbol_VKs = {
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0}
		};
				static int[,] Symbol_Chs = {
			{'$','€','£', // ₠₡₢₣₤₥₧₨₪₩₯
			 '¢',0,0,
			 '¥',0,'₧'},

			{'{','±','}',  // ±°µ
			 '[','°',']',
			 '(','µ',')'},

			{'³','²','¹',  // ¹²³•●◊▫▪□
			 '•','●','ⁿ',
			 '□','▪','◊'},

			{'≤','≥','≠',  // ≤≥≠≈∫∞∂∆∑
			 '≈','∫','∞',
			 '∂','∆','∑'},

			{'~','#','^',
			 '’','*','@',
			 '”','¬','¦'},

			{'º','ª','®',  // πª©®¤¡»«º
			 '«','»','©',
			 '¿','¡','¤'},

			{'α','β','γ',  // θαλβγσφωΦς  
			 'σ','θ','λ',
			 'φ','ω','Φ'},

			{'⅜','½','⅝',  // ¼½¾÷⅛⅜⅝⅞∕
			 '¼','∕','¾',
			 '⅛','&','⅞'},

			{'д','ж','§', // ¶·∕∙√♯∏Ωджε
			 '∏','Ω','¶',
			 '√','·','`'}
		};

#endif
		#endregion
	}
}
