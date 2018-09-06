/*	AUTHOR:			AARON DAY
*	CREATED:		7 MAR 2017
*	MODIFIED:		13 MAR 2017
*	SUBJECT:		CST407 - CRYPTOGRAPHY
*	INSTRUCTOR:		PHONG NGUYEN
*	ASSIGNMENT:		FINAL PROJECT
*	DESCRIPTION:	ENIGMA
*	FILE:			MACHINE.CS
*/

using System;

namespace Enigma
{
    public class Machine
    {
        public Machine(PlugBoard plugboard, Rotor[] rotors, Reflector reflector)
        {
            Plugboard = plugboard;
            Rotor = rotors;
            Reflector = reflector;
        }

        public void Backspace()
        {
            // Rollback right rotor. If the new position is on a notch, rollback the next rotor, else done. (repeat for all rotors)
            for (var i = Rotor.Length - 1; i >= 0; --i)
            {
                --Rotor[i];
                if (!Rotor[i].IsNotch()) return;
            }
        }

        public char KeyPress(char c)
        {
            // Step Rotors
            var stepindex = Rotor.Length - 1; // Index of the last (right) rotor so it can be stepped
            /* 
             * Beginning on the last rotor, check if it is on a notch. If so, keep checking rotors until
             * one not on a notch is found or the end is reached; decrementing the stepindex for each notch found
             */
            for (var i = Rotor.Length - 2; i >= 0; --i)
            {
                if (!Rotor[i + 1].IsNotch()) break;
                stepindex = i;
            }

            // Step the right rotor. Step each rotor to the left of a notch on a stepped rotor.
            for (var i = stepindex; i < Rotor.Length; ++i)
            {
                ++Rotor[i];
            }

            // Encrypt character through plugboard
            var value = c;
            char newval;
            if(Plugboard.GetPlugs().TryGetValue(value, out newval)) value = newval;

            // Encrypt character through rotors
            var position = 0; // Position of the rotor relative to 'A'. Housing is stationary, therefore, position is 0
            for (var i = Rotor.Length - 1; i >= 0; --i)
            {
                value = Rotor[i].ToReflector(value, position);
                position = Convert.ToInt32(Rotor[i].Window - 'A'); // Update position based on this rotor's positon
            }

            // Encrypt character through reflector
            value = Reflector.Reflect(value, position);

            // Encrypt character through rotors again
            position = 0; // Reflector is stationary, therefore, position is 0
            foreach (var r in Rotor)
            {
                value = r.FromReflector(value, position);
                position = Convert.ToInt32(r.Window - 'A'); // Update position based on this rotor's positon
            }

            // Remove the position offset from the value returned to the housing
            var num = Convert.ToInt32(value - 'A');
            num = (26 + num - position) % 26;
            value = Convert.ToChar(num + 'A');

            // Encrypt character through plugboard again
            if(Plugboard.GetPlugs().TryGetValue(value, out newval)) value = newval;

            return value; // Return ciphertext character
        }

        public PlugBoard Plugboard;
        public Rotor[] Rotor;
        public Reflector Reflector;
    }
}
