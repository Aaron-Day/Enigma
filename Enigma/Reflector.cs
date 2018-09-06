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
    public class Reflector
    {
        public Reflector(string name, char[] wires)
        {
            Name = name;
            if(!SetWires(wires)) throw new InvalidDataException("Wire map is invalid");
        }

        public bool SetWires(char[] wires)
        {
            if (!IsValidWires(wires)) return false;

            Wires = new SortedDictionary<char, char>();
            for (var i = 0; i < 26; ++i)
            {
                Wires.Add(Convert.ToChar(i + 'A'), wires[i]);
            }

            return true;
        }

        private static bool IsValidWires(char[] wires)
        {
            if (wires?.Length != 26 || wires.Distinct().ToArray().Length != 26) return false; // Must have 26 unique, non-null, wires
            if(wires.Any(w => w < 'A' || w > 'Z')) return false; // Wires must be uppercase letters (A-Z)

            // Setup for remaining validation rules
            var wireLinks = new SortedDictionary<char, char>();
            for (var i = 0; i < 26; ++i)
            {
                wireLinks.Add(Convert.ToChar(i + 'A'), wires[i]);
            }

            // Remaining rules
            if (wireLinks.Any(w => w.Key == w.Value)) return false; // Wires cannot be wired to themselves
            foreach (var wire in wireLinks) // Ensure that plug <A> goes to plug <B> and plug <B> goes to plug <A>
            {
                var key = '1';
                /* 
                 * '1' is not in the valid character set.
                 * If the following code does not change this, there was an unknown error
                 * Wire keys list should match wire values list so this should always be changed
                 */
                if (wireLinks.TryGetValue(wire.Value, out key))
                {
                    if (key != wire.Key) // Referenced wire's value must equal this wire's key
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true; // Passed Validation
        }

        public char Reflect(char input, int offset)
        {
            var inputval = input - 'A';
            input = Convert.ToChar(((26 + inputval - offset) % 26) + 'A');
            return Wires[input];
        }

        public string Name { get; set; }
        private SortedDictionary<char, char> Wires { get; set; }
    }
}
