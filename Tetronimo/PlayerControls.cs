using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;

namespace Tetromino
{
    public class PlayerControls
    {
        /// <summary>
        ///  Represents the keys used for input (moving left & right, rotation, and dropping)
        /// </summary>

        public struct PlayerInputs
        {
            public readonly Keys left, right, rotate, drop;

            public PlayerInputs(PlayerInputs inputs) :
                this(inputs.left, inputs.right, inputs.rotate, inputs.drop)
            {

            }

            public PlayerInputs(Keys left, Keys right, Keys rotate, Keys drop)
            {
                this.left = left;
                this.right = right;
                this.rotate = rotate;
                this.drop = drop;
            }
        }

        /// <summary>Represents the keys used for input (moving left & right, rotation, and dropping)
        /// as strings. Used for the strings in the Combo Boxes in the Player Controls
        /// Form</summary>

        public struct PlayerInputStrings
        {
            public readonly string left, right, rotate, drop;

            public PlayerInputStrings(PlayerInputStrings inputStrings) :
                this(inputStrings.left, inputStrings.right, inputStrings.rotate, inputStrings.drop)
            {

            }

            public PlayerInputStrings(string left, string right, string rotate, string drop)
            {
                this.left = (!playerInputsDict.Keys.Contains(left) ? DefaultLeft : left);
                this.right = (!playerInputsDict.Keys.Contains(right) ? DefaultRight : right);
                this.rotate = (!playerInputsDict.Keys.Contains(rotate) ? DefaultRotate : rotate);
                this.drop = (!playerInputsDict.Keys.Contains(drop) ? DefaultDrop : drop);
            }
        }

        static Dictionary<string, Keys> playerInputsDict = new Dictionary<string, Keys>
        {
            { "A", Keys.A }, { "B", Keys.B }, { "C", Keys.C }, { "D", Keys.D },
            { "E", Keys.E }, { "F", Keys.F }, { "G", Keys.G }, { "H", Keys.H },
            { "I", Keys.I }, { "J", Keys.J }, { "K", Keys.K }, { "L", Keys.L },
            { "M", Keys.M }, { "N", Keys.N }, { "O", Keys.O }, { "P", Keys.P },
            { "Q", Keys.Q }, { "R", Keys.R }, { "S", Keys.S }, { "T", Keys.T },
            { "U", Keys.U }, { "V", Keys.V }, { "W", Keys.W }, { "X", Keys.X },
            { "Y", Keys.Y }, { "Z", Keys.Z },
            { "0", Keys.D0 }, { "1", Keys.D1 }, { "2", Keys.D2 }, { "3", Keys.D3 },
            { "4", Keys.D4 }, { "5", Keys.D5 }, { "6", Keys.D6 }, { "7", Keys.D7 },
            { "8", Keys.D8 }, { "9", Keys.D9 },
            { "-", Keys.OemMinus }, { "=", Keys.Oemplus }, { "]", Keys.OemCloseBrackets },
            { "[", Keys.OemOpenBrackets }, { "+", Keys.Oemplus }, { ",", Keys.Oemcomma },
            { ".", Keys.OemPeriod }, { "/", Keys.OemQuestion }, { ";", Keys.OemSemicolon },
            { "'", Keys.OemQuotes }, { "*", Keys.Multiply }, { "`", Keys.Oemtilde },
            { "Up Arrow", Keys.Up }, { "Down Arrow", Keys.Down }, { "Left Arrow", Keys.Left },
            { "Right Arrow", Keys.Right }, { "Tab", Keys.Tab }, { "Caps Lock", Keys.CapsLock },
            { "Space", Keys.Space }, { "Backspace", Keys.Back }, { "Enter", Keys.Enter }
        };

        private PlayerInputStrings inputStrings;
        private PlayerInputs playerInputs;
        public static readonly string DefaultLeft = "Left Arrow";
        public static readonly string DefaultRight = "Right Arrow";
        public static readonly string DefaultDrop = "Down Arrow";
        public static readonly string DefaultRotate = "Up Arrow";

        public PlayerControls()
        {
            SetInputStrings(new PlayerInputStrings(Properties.Settings.Default.LeftKey,
                Properties.Settings.Default.RightKey,
                Properties.Settings.Default.RotateKey,
                Properties.Settings.Default.DropKey));
        }

        /// <summary>
        /// Save the player controls to file.
        /// </summary>

        public void Save()
        {
            var appSettings = ConfigurationManager.AppSettings;

            Properties.Settings.Default.LeftKey = inputStrings.left;
            Properties.Settings.Default.RightKey = inputStrings.right;
            Properties.Settings.Default.RotateKey = inputStrings.rotate;
            Properties.Settings.Default.DropKey = inputStrings.drop;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Get the player controls in string form (as it would be displayed in the
        /// Combo Boxes in the Player Controls Form)
        /// </summary>
        /// <returns></returns>

        public PlayerInputStrings GetInputStrings()
        {
            return new PlayerInputStrings(inputStrings);
        }

        /// <summary>
        /// Set the player controls in string form and convert them into Key objects
        /// </summary>
        /// <param name="inputStrings"></param>

        public void SetInputStrings(PlayerInputStrings inputStrings)
        {
            SetInputStrings(inputStrings.left, inputStrings.right,
                inputStrings.rotate, inputStrings.drop);
        }

        public void SetInputStrings(string left, string right, string rotate, string drop)
        {
            this.inputStrings = new PlayerInputStrings(left, right, rotate, drop);

            playerInputs = new PlayerInputs(playerInputsDict[left],
                playerInputsDict[right], playerInputsDict[rotate],
                playerInputsDict[drop]);            
        }

        public PlayerInputs GetInputs()
        {
            return new PlayerInputs(playerInputs);
        }
    }
}
