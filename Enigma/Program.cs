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
using System.Windows.Forms;

namespace Enigma
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EnigmaMachine());
        }
    }
}
