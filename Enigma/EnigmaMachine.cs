/*	AUTHOR:			AARON DAY
*	CREATED:		7 MAR 2017
*	MODIFIED:		13 MAR 2017
*	SUBJECT:		CST407 - CRYPTOGRAPHY
*	INSTRUCTOR:		PHONG NGUYEN
*	ASSIGNMENT:		FINAL PROJECT
*	DESCRIPTION:	ENIGMA
*	FILE:			ENIGMAMACHINE.CS
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Enigma
{
    public partial class EnigmaMachine : Form
    {
        private List<Rotor> RotorList { get; set; }
        private List<Reflector> ReflectorList { get; set; }
        private List<Label> LightList { get; set; }
        private SortedDictionary<char, Keys> Keyboard { get; set; }
        private List<TextBox> PlugBoardList { get; set; } 
        private Machine EMachine { get; }

        public EnigmaMachine()
        {
            // Base machine components
            InitializeComponent();
            InitializeRotorList();
            InitializeReflectorList();
            InitializeKeyboard();
            InitializeLights();

            // Setup machine with default settings
            Rotor[] rotors = {GetRotor("I"), GetRotor("II"), GetRotor("III")};
            EMachine = new Machine(new PlugBoard(), rotors, GetReflector("B"));
            
            // Update the drop-down lists to show appropriate default values for machine setup
            InitializePlugBoardList();
            UpdateComboBoxSelections();

            // Start program with the focus on textBox1 (plaintext input)
            textBox1.Select();
        }

        public Rotor GetRotor(string name)
        {
            return RotorList.FirstOrDefault(r => r.Name == name);
        }

        public Reflector GetReflector(string name)
        {
            return ReflectorList.FirstOrDefault(r => r.Name == name);
        }

        private void InitializeRotorList()
        {
            RotorList = new List<Rotor>();

            string[] rotornames =
            {
                "I",
                "II",
                "III",
                "IV",
                "V",
                "VI",
                "VII",
                "VIII"
            };

            string[] rotormaps =
            {
                "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
                "AJDKSIRUXBLHWTMCQGZNPYFVOE",
                "BDFHJLCPRTXVZNYEIWGAKMUSQO",
                "ESOVPZJAYQUIRHXLNFTGKDCMWB",
                "VZBRGITYUPSDNHLXAWMJQOFECK",
                "JPGVOUMFYQBENHZRDKASXLICTW",
                "NZJHGRCXMYSWBOUFAIVLPEKQDT",
                "FKQHTLXOCBJSPDZRAMEWNIUYGV"
            };

            char[][] rotornotches =
            {
                new [] {'Q'},
                new [] {'E'},
                new [] {'V'},
                new [] {'J'},
                new [] {'Z'},
                new [] {'M', 'Z'},
                new [] {'M', 'Z'},
                new [] {'M', 'Z'}
            };

            if (rotornames.Length != rotormaps.Length || rotornames.Length != rotornotches.Length) return;

            for (var i = 0; i < rotornames.Length; ++i)
            {
                RotorList.Add(new Rotor(rotornames[i], rotormaps[i].ToCharArray(), rotornotches[i]));
                comboBox4.Items.Add(rotornames[i]); // Left rotor drop-down list
                comboBox5.Items.Add(rotornames[i]); // Middle rotor drop-down list
                comboBox6.Items.Add(rotornames[i]); // Right rotor drop-down list
            }
        }

        private void InitializeReflectorList()
        {
            ReflectorList = new List<Reflector>();
            
            string[] reflectornames =
            {
                "A",
                "B",
                "C",
            };

            string[] reflectormaps =
            {
                "EJMZALYXVBWFCRQUONTSPIKHGD",
                "YRUHQSLDPXNGOKMIEBFZCWVJAT",
                "FVPJIAOYEDRZXWGCTKUQSBNMHL"
            };

            if (reflectornames.Length != reflectormaps.Length) return;

            for (var i = 0; i < reflectornames.Length; ++i)
            {
                ReflectorList.Add(new Reflector(reflectornames[i], reflectormaps[i].ToCharArray()));
                comboBox10.Items.Add(reflectornames[i]); // Reflector drop-down list
            }
        }

        private void InitializeLights()
        {
            LightList = new List<Label>
            {
                label33,
                label34,
                label35,
                label36,
                label37,
                label38,
                label39,
                label40,
                label41,
                label42,
                label43,
                label44,
                label45,
                label46,
                label47,
                label48,
                label49,
                label50,
                label51,
                label52,
                label53,
                label54,
                label55,
                label56,
                label57,
                label58
            };
        }

        private void InitializeKeyboard()
        {
            Keyboard = new SortedDictionary<char, Keys>
            {
                {'A', Keys.A},
                {'B', Keys.B},
                {'C', Keys.C},
                {'D', Keys.D},
                {'E', Keys.E},
                {'F', Keys.F},
                {'G', Keys.G},
                {'H', Keys.H},
                {'I', Keys.I},
                {'J', Keys.J},
                {'K', Keys.K},
                {'L', Keys.L},
                {'M', Keys.M},
                {'N', Keys.N},
                {'O', Keys.O},
                {'P', Keys.P},
                {'Q', Keys.Q},
                {'R', Keys.R},
                {'S', Keys.S},
                {'T', Keys.T},
                {'U', Keys.U},
                {'V', Keys.V},
                {'W', Keys.W},
                {'X', Keys.X},
                {'Y', Keys.Y},
                {'Z', Keys.Z}
            };

        }

        private void InitializePlugBoardList()
        {
            PlugBoardList = new List<TextBox>
            {
                textBox3,
                textBox4,
                textBox5,
                textBox6,
                textBox7,
                textBox8,
                textBox9,
                textBox10,
                textBox11,
                textBox12,
                textBox13,
                textBox14,
                textBox15,
                textBox16,
                textBox17,
                textBox18,
                textBox19,
                textBox20,
                textBox21,
                textBox22,
                textBox23,
                textBox24,
                textBox25,
                textBox26,
                textBox27,
                textBox28
            };
        }

        private void UpdateComboBoxSelections()
        {
            comboBox1.SelectedItem = EMachine.Rotor[0].Window.ToString();
            comboBox2.SelectedItem = EMachine.Rotor[1].Window.ToString();
            comboBox3.SelectedItem = EMachine.Rotor[2].Window.ToString();
            comboBox4.SelectedItem = EMachine.Rotor[0].Name;
            comboBox5.SelectedItem = EMachine.Rotor[1].Name;
            comboBox6.SelectedItem = EMachine.Rotor[2].Name;
            comboBox7.SelectedItem = EMachine.Rotor[0].GetRing().ToString();
            comboBox8.SelectedItem = EMachine.Rotor[1].GetRing().ToString();
            comboBox9.SelectedItem = EMachine.Rotor[2].GetRing().ToString();
            comboBox10.SelectedItem = EMachine.Reflector.Name;
        }

        private void UpdateRotorLabels()
        {
            label4.Text = EMachine.Rotor[0].Name;
            label5.Text = EMachine.Rotor[1].Name;
            label6.Text = EMachine.Rotor[2].Name;
        }

        private void UpdateNotchIndicators()
        {
            label1.Text = (EMachine.Rotor[0].IsNotch() ? "->" : "");
            label2.Text = (EMachine.Rotor[1].IsNotch() ? "->" : "");
            label3.Text = (EMachine.Rotor[2].IsNotch() ? "->" : "");
        }

        private void UpdatePlugs()
        {
            foreach (var x in PlugBoardList)
            {
                var key = Convert.ToChar(x.Tag.ToString());
                x.Text = EMachine.Plugboard.GetPlugs().ContainsKey(key) ? EMachine.Plugboard.GetPlugs()[Convert.ToChar(x.Tag.ToString())].ToString() : "";
            }
        }
        /************************************************************************************************/
        // EVENT HANDLERS

        #region Reflector

        private void Reflector_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the user selects a different reflector, replace the existing one
            var box = sender as ComboBox;
            if (box == null) return;
            EMachine.Reflector = GetReflector(box.SelectedItem.ToString());
        }

        #endregion

        #region Rotor

        private void RotorButton_Click(object sender, EventArgs e)
        {
            // When the user clicks a + or -, increment or decrement the window for that rotor
            var btn = sender as Button;
            if (btn == null) return;
            if (btn.Text == @"+")
            {
                ++EMachine.Rotor[Convert.ToInt32(btn.Tag.ToString()) - 1];
            }
            else
            {
                --EMachine.Rotor[Convert.ToInt32(btn.Tag.ToString()) - 1];
            }
            UpdateComboBoxSelections(); // Update the display window to reflect the change
            UpdateNotchIndicators(); // Update the notch indicators to reflect the change
        }

        private void RotorPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the user selects a position for a rotor, set the rotor window equal to the selection
            var box = sender as ComboBox;
            if (box == null)
            {
                UpdateComboBoxSelections(); // Reset the display to remove the user's selection
                return;
            }
            EMachine.Rotor[Convert.ToInt32(box.Tag.ToString()) - 1].Window
                = Convert.ToChar(box.SelectedItem.ToString().FirstOrDefault());
            // Display window is changed automatically to the user's selection
            UpdateNotchIndicators(); // Update the notch indicators to reflect the change
        }

        private void Rotor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the user selects a different rotor, replace the existing one
            var box = sender as ComboBox;
            if (box == null)
            {
                UpdateComboBoxSelections(); // Reset the display to remove the user's selection
                return;
            }

            var selected = GetRotor(box.SelectedItem.ToString());
            selected.SetRing(EMachine.Rotor[Convert.ToInt32(box.Tag.ToString()) - 1].GetRing());
            selected.Window = EMachine.Rotor[Convert.ToInt32(box.Tag.ToString()) - 1].Window;
            EMachine.Rotor[Convert.ToInt32(box.Tag.ToString()) - 1] = selected;
            // Display window is changed automatically to the user's selection
            UpdateNotchIndicators(); // Update the notch indicators to reflect the change
            UpdateRotorLabels(); // Update the labels to show the new rotor name
        }

        #endregion

        #region Ring

        private void Ring_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the user selects a different ring setting, update the rotor to reflect the change
            var box = sender as ComboBox;
            if (box == null)
            {
                UpdateComboBoxSelections(); // Reset the display to remove the user's selection
                return;
            }
            EMachine.Rotor[Convert.ToInt32(box.Tag.ToString()) - 1]
                .SetRing(Convert.ToChar(box.SelectedItem.ToString()));
        }

        #endregion

        #region Keyboard

        private void Keyboard_Click(object sender, EventArgs e)
        {
            // When a user clicks on a keyboard key, process it through the textbox as if they typed it in
            var btn = sender as Button;
            if (btn == null) return;
            textBox1_KeyDown(sender, new KeyEventArgs(Keyboard[Convert.ToChar(btn.Text)]));
        }

        #endregion

        #region TextBox

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Process keyboard keys in the textbox
                // Backspace
            if (e.KeyData == Keys.Back && textBox1.TextLength > 0)
            {
                EMachine.Backspace(); // Decrement rotors
                textBox1.Text = textBox1.Text.Remove(textBox1.TextLength - 1, 1); // Remove plaintext character from end
                textBox2.Text = textBox2.Text.Remove(textBox2.TextLength - 1, 1); // Remove ciphertext from end
            }
            else
            {
                // Other characters
                var value = char.ToUpper(Convert.ToChar(e.KeyValue)); // Convert input to uppercase character
                if (value < 'A' || value > 'Z') return; // Ensure the character is an uppercase letter
                var crypt = EMachine.KeyPress(value); // Encrypt the text
                textBox1.AppendText(value.ToString()); // Place the plaintext at the end of textBox1
                textBox2.AppendText(crypt.ToString()); // Place the ciphertext at the end of textBox1
            }
            UpdateComboBoxSelections(); // Rotors moved so update the display windows
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Update the lightboard whenever the textbox changes
            var text = "1";
            if (!string.IsNullOrWhiteSpace(textBox2.Text)) text = textBox2.Text;
            foreach (var light in LightList)
            {
                // Compare the last character of the ciphertext box to the light character
                if (text.Substring(text.Length - 1, 1) == light.Text)
                {
                    light.ForeColor = Color.Black;
                    light.BackColor = Color.DarkKhaki;
                }
                else
                {
                    light.ForeColor = Color.AntiqueWhite;
                    light.BackColor = Color.Black;
                }
            }
        }

        #endregion

        #region PlugBoard

        private void PlugBoard_KeyDown(object sender, KeyEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null) return;
            var value = char.ToUpper(Convert.ToChar(e.KeyValue)); // Convert input to uppercase character
            if ((value < 'A' || value > 'Z') && e.KeyData != Keys.Back) return; // Ensure the character is an uppercase letter or backspace

            if (e.KeyData == Keys.Back && !string.IsNullOrWhiteSpace(box.Text)) // Process backspace
            {
                // Remove both ends of the plug
                EMachine.Plugboard.RemovePlug(Convert.ToChar(box.Text));
                EMachine.Plugboard.RemovePlug(Convert.ToChar(box.Tag.ToString()));
            }
            else // Not a backspace character
            {
                if (value == Convert.ToChar(box.Tag.ToString())) return; // Don't set the value to itself

                EMachine.Plugboard.AddPlug(Convert.ToChar(box.Tag.ToString()), value); // Add the plug (occupied ends are taken care of in PlugBoard class)
            }
        }

        private void PlugBoard_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Handle all valid characters except backspsce. Backspace will leave an extra character otherwise
            var value = char.ToUpper(e.KeyChar);
            e.Handled = (value >= 'A' && value <= 'Z');
            UpdatePlugs();
        }

        #endregion

        #region Form

        private void EnigmaMachine_Load(object sender, EventArgs e)
        {
            this.Size = new Size(605, 550);
        }

        #endregion
    }
}
