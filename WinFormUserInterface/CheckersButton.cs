using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Checkers;
namespace WinFormUserInterface
{
    public class CheckersButton : Button
    {
        private int m_col;
        private int m_row;

        public int Col
        {
            get
            {
                return m_col;
            }

            set
            {
                m_col = value;
            }
        }
        public int Row
        {
            get
            {
                return m_row;
            }

            set
            {
                m_row = value;
            }
        }

    }
}
