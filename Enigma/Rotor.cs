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
using System.IO;
using System.Linq;

namespace Enigma
{
    public class Rotor
    {
        public Rotor(string name, char[] wires, char[] notches, char ring = 'A')
        {
            Name = name;
            if (!SetWires(wires, ring)) throw new InvalidDataException("Something went wrong when creating rotor ring and wire settings");
            if(!SetNotches(notches)) throw new InvalidDataException("Something went wrong when setting rotor notches");
            Window = 'A';
        }

        private bool SetWires(char[] wires, char ring)
        {
            if (!IsValidWires(wires)) return false;
            if (!IsValidRing(ring)) return false;
            var x = new int[26];
            for (var i = 0; i < 26; ++i)
            {
                x[i] = (26 + wires[i % 26] - 'A' - i) % 26;
            }

            var offsets = new int[26];
            var r = ring - 'A';
            for (var i = 0; i < 26; ++i)
            {
                offsets[i] = x[(26 - r + i) % 26];
            }

            Ring = ring;

            Forward = new Dictionary<char, int>();
            Reverse = new Dictionary<char, int>();
            for (var i = 0; i < 26; ++i)
            {
                Forward.Add(Convert.ToChar(i + 'A'), offsets[i]);
                Reverse.Add(Convert.ToChar(((offsets[i] + i) % 26) + 'A'), 26 - offsets[i]);
            }
            return true;
        }

        private bool SetWires(char ring)
        {
            if (Forward?.Count != 26 || Reverse?.Count != 26) return false;
            return SetWires(new List<char>(Reverse.Keys).ToArray(), ring);
        }

        private bool SetNotches(char[] notches)
        {
            if (!IsValidNotches(notches)) return false;
            Notches = notches;
            return true;
        }

        public bool SetRing(char ring)
        {
            return SetWires(ring);
        }

        public static Rotor operator ++(Rotor r)
        {
            var i = Convert.ToInt32(r.Window - 'A');
            i = (i + 1) % 26;
            r.Window = Convert.ToChar('A' + i);
            return r;
        }

        public static Rotor operator --(Rotor r)
        {
            var i = Convert.ToInt32(r.Window - 'A');
            i = (i + 25) % 26;
            r.Window = Convert.ToChar('A' + i);
            return r;
        }

        private static bool IsValidWires(char[] wires)
        {
            if (wires?.Length != 26 || wires.Distinct().ToArray().Length != 26) return false; // Must have 26 unique wires
            if (wires.Any(n => n < 'A' || n > 'Z')) return false; // Wires must be labeled with uppercase letters

            return true; // Passed Validation
        }

        private static bool IsValidNotches(char[] notches)
        {
            if (notches.Length > 26 || notches.Length <= 0) return false;
            if (notches.Length != notches.Distinct().ToArray().Length) return false;
            if (notches.Any(n => n < 'A' || n > 'Z')) return false;
            return true;
        }

        public static bool IsValidRing(char ring)
        {
            return (ring >= 'A' && ring <= 'Z');
        }

        public bool IsNotch()
        {
            return Notches.Contains(Window);
        }

        public void RingUp()
        {
            var i = Convert.ToInt32(Ring - 'A');
            i = (i + 1) % 26;
            SetRing(Convert.ToChar('A' + i));
        }

        public void RingDown()
        {
            var i = Convert.ToInt32(Ring - 'A');
            i = (i + 25) % 26;
            SetRing(Convert.ToChar('A' + i));
        }

        public char ToReflector(char input, int position)
        {
            var i = Convert.ToInt32(input - 'A');
            var w = Convert.ToInt32(Window - 'A');
            i = (26 + i + w - position) % 26;
            i = (i + Forward[Convert.ToChar(i + 'A')]) % 26;
            return Convert.ToChar(i + 'A');
        }

        public char FromReflector(char input, int position)
        {
            var w = Window - 'A';
            var i = input - 'A';
            i = (26 + i + w - position) % 26;
            i = (i + Reverse[Convert.ToChar(i + 'A')]) % 26;
            return Convert.ToChar(i + 'A');
        }
        
        public char GetRing()
        {
            return Ring;
        }

        public string Name { get; set; }
        private char[] Notches { get; set; }
        private char Ring { get; set; }
        public char Window { get; set; }
        private Dictionary<char, int> Forward { get; set; }
        private Dictionary<char, int> Reverse { get; set; }
    }
}
