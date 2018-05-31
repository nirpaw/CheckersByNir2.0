using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Checkers;

namespace WinFormUserInterface
{
    public partial class FormGameSettings : Form
    {
        private const string k_computerName = "Computer";
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private int m_BoardSize;
        private bool m_IsVsComputer = true;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        public bool IsVsComputer
        {
            get
            {
                return m_IsVsComputer;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }
        public Player PlayerOne
        {
            get
            {
                return m_PlayerOne;
            }
        }
        public Player PlayerTwo
        {
            get
            {
                return m_PlayerTwo;
            }
        }

        private void button_Click(object i_Sender, EventArgs i_E)
        {
            if (i_Sender is CheckBox)
            {
                if ((i_Sender as CheckBox).Checked == true)
                {
                    textBox_Player2.Enabled = true;
                    textBox_Player2.Text = string.Empty;
                    m_IsVsComputer = false;
                }
                else
                {
                    textBox_Player2.Enabled = true;
                    textBox_Player2.Text = k_computerName;
                    textBox_Player2.Enabled = false;
                    m_IsVsComputer = true;
                }
            }
            
            if(i_Sender is Button)
            {
                m_PlayerOne = new Player(textBox_Player1.Text);
                m_PlayerTwo = new Player(textBox_Player2.Text);
                m_BoardSize = setBoardSize();
            }
        }

        private int setBoardSize()
        {
            int boardSize;
            if(radioButton_size6.Checked)
            {
                boardSize = 6;
            }
            else if(radioButton_size8.Checked)
            {
                boardSize = 8;
            }
            else
            {
                boardSize = 10;
            }
            return boardSize;
        }
    }
}
