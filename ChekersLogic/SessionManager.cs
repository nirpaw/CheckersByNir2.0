using System;
using System.Collections.Generic;

namespace Checkers
{
    public class SessionManager
    {
        private Board m_GameBoard;
        private Player m_PlayerOne, m_PlayerTwo;
        private int m_MovesCounter;
        private bool m_IsVScoputer;
        private int m_LastCol;
        private int m_LastRowl;

        public SessionManager(Player i_playerOne, Player i_playerTwo, int i_boardSize, bool i_IsVsComputer)
        {
            m_GameBoard = new Board(i_boardSize);
            m_PlayerOne = i_playerOne;
            m_PlayerTwo = i_playerTwo;
            m_PlayerOne.UserName = m_PlayerOne.UserName == string.Empty ? m_PlayerOne.UserName = "Player 1" : m_PlayerOne.UserName;
            m_PlayerTwo.UserName = m_PlayerTwo.UserName == string.Empty ? m_PlayerTwo.UserName = "Player 2" : m_PlayerTwo.UserName;
            m_IsVScoputer = i_IsVsComputer;
            StartNewGame();
        }

        public SessionManager(Player i_playerOne, int i_boardSize)
        {
            m_GameBoard = new Board(i_boardSize);
            m_PlayerOne = i_playerOne;
            m_PlayerTwo = new Player("Computer");
            m_IsVScoputer = true;
        }

        public Player GetPlayerOne()
        {
            return m_PlayerOne;
        }

        public Player GetPlayerTwo()
        {
            return m_PlayerTwo;
        }
        public int MovesCounter
        {
            get
            {
                return m_MovesCounter;
            }
        }
        public string GetWineerName()
        {
            return m_MovesCounter % 2 == 0 ? m_PlayerOne.UserName : m_PlayerTwo.UserName;
        }

        public Pawn[,] GameBoard
        {
            get
            {
                return m_GameBoard.getBoard();
            }
        }
        private void StartNewGame()
        {
            m_GameBoard.InitBoard();
            m_MovesCounter = 0;
        }

        public void ManageTurn(int i_OriginRow, int i_OriginCol, int i_DestRow, int i_DestCol)
        {
            int playerTurn = (m_MovesCounter++) % 2;
            bool isSeconedEat = false, thereIsAnotherEat = false, iAte = false;
            if (playerTurn == 1 && m_IsVScoputer)
            {
                do
                {
                    m_GameBoard.ChooseBestMove(m_LastRowl, m_LastCol, isSeconedEat, ref thereIsAnotherEat, ref iAte);
                }
                while (iAte && thereIsAnotherEat);
            }
            else
            {
                if (isSeconedEat && (i_OriginRow != m_LastRowl || i_OriginCol != m_LastCol))
                {
                    m_MovesCounter--;
                    throw new Exception("Illegal move, Try Again");
                }
                else if (!m_GameBoard.CheckValidation(i_OriginCol, i_OriginRow, i_DestCol, i_DestRow, playerTurn, isSeconedEat))
                {
                    m_MovesCounter--;
                    throw new Exception("Illegal move, Try Again");
                }
            }

            if (m_GameBoard.IsGameOver())
            {
                EndGame(playerTurn);
                throw new ExecutionEngineException();
            }

            if ((thereIsAnotherEat && iAte) || ((playerTurn == 0 || (!m_IsVScoputer && playerTurn == 1)) && (Math.Abs(i_DestCol - i_OriginCol) != 1 && m_GameBoard.isAnotherEat(i_DestRow, i_DestCol, playerTurn))))
            {
                m_LastRowl = i_DestRow;
                m_LastCol = i_DestCol;
                m_MovesCounter--;
                isSeconedEat = true;
            }
            else
            {
                isSeconedEat = false;
            }
        }

        private void EndGame(int i_winner)
        {
            int gameScore = m_GameBoard.Scoring(i_winner);
            if (i_winner == 0)
            {
                m_PlayerOne.AddPoints(gameScore);
            }
            else
            {
                m_PlayerTwo.AddPoints(gameScore);
            }
            StartNewGame();
        }
    }
}
