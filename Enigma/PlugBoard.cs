/*	AUTHOR:			AARON DAY
*	CREATED:		7 MAR 2017
*	MODIFIED:		13 MAR 2017
*	SUBJECT:		CST407 - CRYPTOGRAPHY
*	INSTRUCTOR:		PHONG NGUYEN
*	ASSIGNMENT:		FINAL PROJECT
*	DESCRIPTION:	ENIGMA
*	FILE:			PLUGBOARD.CS
*/

using System.Collections.Generic;
using System.Linq;

namespace Enigma
{
    public class PlugBoard
    {
        public PlugBoard()
        {
            // Construct an empty plugboard
            Plugs = new SortedDictionary<char, char>();
        }

        public PlugBoard(SortedDictionary<char, char> plugs)
        {
            // Construct a plugboard with given values. If the values are invalid, construct an empty plugboard
            if (!SetPlugs(plugs))
                Plugs = new SortedDictionary<char, char>();
        }

        public bool SetPlugs(SortedDictionary<char, char> plugs = null)
        {
            // If no parameters were passed, reset Plugs to an empty plugboard
            if (plugs == null)
            {
                Plugs = new SortedDictionary<char, char>();
                return true;
            }
            // Attempt to reset the plugboard to the given values. Return successfully set values bool
            if (!IsValidPlugs(plugs)) return false;
            Plugs = plugs;
            return true;
        }

        private static bool IsValidPlugs(SortedDictionary<char, char> plugs)
        {
            if (plugs.Count == 0) return true; // An empty plugboard is valid
            if (plugs.Count % 2 != 0) return false; // Plugs come in pairs
            if (plugs.Count > 13) return false; // There can't be more than 13 pairs (26 letters / 2 letters per pair)
            if (plugs.Values.Any(v => v < 'A' || v > 'Z')) return false; // Plugs must be uppercase letters (A-Z)
            if (plugs.Any(plug => plug.Key == plug.Value)) return false; // Plugs cannot be plugged into themselves
            if (plugs.All(plug => plugs.ContainsValue(plug.Key))) return false; // Plug keys list should match plug values list
            foreach (var plug in plugs) // Ensure that plug <A> goes to plug <B> and plug <B> goes to plug <A>
            {
                var key = '1';
                /* 
                 * '1' is not in the valid character set.
                 * If the following code does not change this, there was an unknown error
                 * Plug keys list should match plug values list so this should always be changed
                 */
                if (plugs.TryGetValue(plug.Value, out key))
                {
                    if (key != plug.Key) // Referenced plug's value must equal this plug's key
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true; // Pased Validation
        }

        public void AddPlug(char from, char to)
        {
            if (Plugs.ContainsKey(from)) RemovePlug(from);
            if (Plugs.ContainsKey(to)) RemovePlug(to);
            Plugs.Add(from, to);
            Plugs.Add(to, from);
        }

        public void RemovePlug(char end)
        {
            if (!Plugs.ContainsKey(end)) return;
            // Disconnect plug from 'end' and from it's other connection point
            var value = Plugs[end];
            Plugs.Remove(end);
            Plugs.Remove(value);
        }

        public SortedDictionary<char, char> GetPlugs() => Plugs;

        private SortedDictionary<char, char> Plugs { get; set; } 
    }
}
