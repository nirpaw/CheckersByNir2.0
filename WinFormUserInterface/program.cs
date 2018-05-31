using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormUserInterface
{
    public class program
    {
        public static void Main()
        {
            FormGameSettings formGameSettings = new FormGameSettings();
            if (formGameSettings.ShowDialog() == DialogResult.OK)
            {
                FormGame G = new FormGame(formGameSettings.PlayerOne, formGameSettings.PlayerTwo, formGameSettings.BoardSize, formGameSettings.IsVsComputer);
                G.ShowDialog();
            }
        }
    }
}
