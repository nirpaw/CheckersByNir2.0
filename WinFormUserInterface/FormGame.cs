using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

using Checkers;

namespace WinFormUserInterface
{
    public partial class FormGame : Form
    {
        private const string k_Title = "Damka";
        private readonly int r_MatrixSize;
        private bool m_IsVsComputer;
        private Label m_LablePlayerOne;
        private Label m_LablePlayerTwo;
        private CheckersButton[,] m_ButtonTable;
        private SessionManager m_CurrentSession;
        private bool m_IsAnysButtonPressed = false;
        private int m_OriginCol;
        private int m_OriginRow;
        private int m_DestCol;
        private int m_DestRow;

        public FormGame(Player i_PlayerOne, Player i_PlayerTwo, int i_BoardSize, bool i_IsVsComputer)
        {
            r_MatrixSize = i_BoardSize;
            m_IsVsComputer = i_IsVsComputer;
            m_CurrentSession = new SessionManager(i_PlayerOne, i_PlayerTwo, i_BoardSize, i_IsVsComputer);
            InitializeComponentCostum(i_PlayerOne.UserName, i_PlayerTwo.UserName, i_PlayerOne.GetTotalPoints(), i_PlayerTwo.GetTotalPoints(), r_MatrixSize);
        }

        private void button_Click(object sender, EventArgs e)
        {
            CheckersButton theButton = (CheckersButton)sender;
            if (!m_IsAnysButtonPressed && m_CurrentSession.GameBoard[theButton.Row, theButton.Col] != null && (m_CurrentSession.GameBoard[theButton.Row, theButton.Col].GetColorId() == m_CurrentSession.MovesCounter % 2))
            {
                theButton.BackColor = Color.LightSkyBlue;
                theButton.Click -= new EventHandler(button_Click);
                theButton.Click += new EventHandler(button_Click2);
                m_IsAnysButtonPressed = true;
                m_OriginCol = theButton.Col;
                m_OriginRow = theButton.Row;
            }
            else if (m_IsAnysButtonPressed && m_CurrentSession.GameBoard[theButton.Row, theButton.Col] == null)
            {
                theButton.BackColor = Color.LightSkyBlue;
                m_DestCol = theButton.Col;
                m_DestRow = theButton.Row;
                try
                {
                    DoAMove();
                    if (m_IsVsComputer && m_CurrentSession.MovesCounter % 2 == 1)
                    {
                        RefreshMatrix();
                        DoAMove();
                    }
                }
                catch (ExecutionEngineException)
                {
                    GameIsoOver();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    m_ButtonTable[m_OriginRow, m_OriginCol].Click += new EventHandler(button_Click);
                    m_ButtonTable[m_OriginRow, m_OriginCol].Click -= new EventHandler(button_Click2);
                    m_IsAnysButtonPressed = false;
                    RefreshMatrix();
                }
            }
        }

        private void GameIsoOver()
        {
            string msg = string.Format("{0} Won!{1}Another Round?", m_CurrentSession.MovesCounter % 2 == 0 ? m_CurrentSession.GetPlayerOne().UserName : m_CurrentSession.GetPlayerTwo().UserName, Environment.NewLine);
            DialogResult dialogResult = MessageBox.Show(msg, "Wooow!", MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    UpdateLables();
                    RefreshMatrix();
                    break;
                case DialogResult.No:
                    this.Close();
                    break;
            }
        }

        private void DoAMove()
        {
            m_CurrentSession.ManageTurn(m_OriginRow, m_OriginCol, m_DestRow, m_DestCol);
        }

        private void button_Click2(object sender, EventArgs e)
        {
            m_IsAnysButtonPressed = false;
            CheckersButton theButton = (CheckersButton)sender;
            theButton.BackColor = Color.WhiteSmoke;
            theButton.Click -= new EventHandler(button_Click2);
            theButton.Click += new EventHandler(button_Click);
        }

        private void UpdateLables()
        {
            m_LablePlayerOne.Text = string.Format("{0}: {1}", m_CurrentSession.GetPlayerOne().UserName, m_CurrentSession.GetPlayerOne().GetTotalPoints());
            m_LablePlayerTwo.Text = string.Format("{0}: {1}", m_CurrentSession.GetPlayerTwo().UserName, m_CurrentSession.GetPlayerTwo().GetTotalPoints());
        }

        private void InitializeComponentCostum(string i_PlayerName1, string i_PlayerName2, int i_Points1, int i_Points2, int i_BoardSize)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Text = k_Title;
            m_LablePlayerOne = new Label();
            m_LablePlayerTwo = new Label();
            m_ButtonTable = new CheckersButton[r_MatrixSize, r_MatrixSize];
            UpdateLables();
            m_LablePlayerOne.Location = new Point(50, 10);
            m_LablePlayerOne.Width = 70;
            m_LablePlayerTwo.Location = new Point(10 + ((i_BoardSize - 3) * 40), 10);
            this.Controls.Add(m_LablePlayerOne);
            this.Controls.Add(m_LablePlayerTwo);
            for (int i = 0; i < r_MatrixSize; i++)
            {
                for (int j = 0; j < r_MatrixSize; j++)
                {
                    m_ButtonTable[i, j] = new CheckersButton();
                    m_ButtonTable[i, j].Size = new Size(40, 40);
                    m_ButtonTable[i, j].Location = new Point(3 + (40 * j), 50 + (40 * i));
                    if ((i + j) % 2 == 0)
                    {
                        m_ButtonTable[i, j].BackColor = Color.DarkSlateGray;
                        m_ButtonTable[i, j].Enabled = false;
                    }
                    else
                    {
                        m_ButtonTable[i, j].BackColor = Color.WhiteSmoke;
                        m_ButtonTable[i, j].Click += new EventHandler(button_Click);
                    }

                    m_ButtonTable[i, j].Row = i;
                    m_ButtonTable[i, j].Col = j;
                    this.Controls.Add(m_ButtonTable[i, j]);
                }
            }

            RefreshMatrix();
        }

        private void RefreshMatrix()
        {
            for (int i = 0; i < r_MatrixSize; i++)
            {
                for (int j = 0; j < r_MatrixSize; j++)
                {
                    if (m_CurrentSession.GameBoard[i, j] != null)
                    {
                        if (m_CurrentSession.GameBoard[i, j].GetColorId() == 0)
                        {
                            if (m_CurrentSession.GameBoard[i, j].IsKing())
                            {
                                m_ButtonTable[i, j].BackgroundImage = WinFormUserInterface.Properties.Resources.csKing;
                            }
                            else
                            {
                                m_ButtonTable[i, j].BackgroundImage = WinFormUserInterface.Properties.Resources.csPawn;
                            }
                        }
                        else
                        {
                            if (m_CurrentSession.GameBoard[i, j].IsKing())
                            {
                                m_ButtonTable[i, j].BackgroundImage = WinFormUserInterface.Properties.Resources.javaKing;
                            }
                            else
                            {
                                m_ButtonTable[i, j].BackgroundImage = WinFormUserInterface.Properties.Resources.javaPawn;
                            }
                        }

                        m_ButtonTable[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        m_ButtonTable[i, j].BackgroundImage = null;
                    }

                    if (!((i + j) % 2 == 0))
                    {
                        m_ButtonTable[i, j].BackColor = Color.WhiteSmoke;
                    }
                }
            }

            this.Text = string.Format("{0}'s turn - {1}", m_CurrentSession.MovesCounter % 2 == 0 ? m_CurrentSession.GetPlayerOne().UserName : m_CurrentSession.GetPlayerTwo().UserName, k_Title);
        }
    }
}
