using System;
using System.Collections.Generic;

namespace Checkers
{
    public class Board
    {
        private int m_BoardSize;
        private Pawn[,] m_BoardMatrix;
        private Random RandNum = new Random();

        public Board(int i_boardSize)
        {
            BoardSize = i_boardSize;
            InitBoard();
        }

        public Pawn[,] getBoard()
        {
            return m_BoardMatrix;
        }
        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public void InitBoard()
        {
            m_BoardMatrix = new Pawn[m_BoardSize, m_BoardSize];
            for (int i = 0; i < (m_BoardSize / 2) - 1; i++)
            {
                for (int j = (m_BoardSize + 1 + i) % 2; j < m_BoardSize; j += 2)
                {
                    m_BoardMatrix[i, j] = new Pawn('O', 0);
                }
            }

            for (int i = m_BoardSize - 1; i > m_BoardSize / 2; i--)
            {
                for (int j = (m_BoardSize + 1 + i) % 2; j < m_BoardSize; j += 2)
                {
                    m_BoardMatrix[i, j] = new Pawn('X', 1);
                }
            }
        }

        internal void ChooseRandomMove(int i_originRow, int i_originCol, bool i_isSeconedEat, ref bool i_thereIsAnotherEat, ref bool i_iAte)
        {
            List<PossibleMove> possibleMovesForComputer = SetListOfPossibleMoves(i_originRow, i_originCol, 1, i_isSeconedEat);
            PossibleMove randMove = possibleMovesForComputer[RandNum.Next(possibleMovesForComputer.Count)];
            i_iAte = randMove.EatMove;
            MakeAStep(randMove.OriginCol, randMove.OriginRow, randMove);
            i_thereIsAnotherEat = isAnotherEat(randMove.DestRow, randMove.DestCol, 1);
        }

        internal void ChooseBestMove(int i_originRow, int i_originCol, bool isSeconedEat, ref bool i_thereIsAnotherEat, ref bool i_iAte)
        {
            List<PossibleMove> possibleMovesForComputer = SetListOfPossibleMoves(i_originRow, i_originCol, 1, isSeconedEat);
            List<PossibleMove> dummyListComp;
            List<PossibleMove> dummyListPlayer;
            PossibleMove bestMove;
            Pawn[,] originalMatrix = (Pawn[,])this.m_BoardMatrix.Clone();
            List<PossibleMove> betterMoves = new List<PossibleMove>();
            foreach (PossibleMove item in possibleMovesForComputer)
            {
                MakeAStep(item.OriginCol, item.OriginRow, item);
                dummyListComp = SetListOfPossibleMoves(item.DestRow, item.DestCol, 1, true);
                if (dummyListComp.Count != 0)
                {
                    if (dummyListComp[0].DestRow == 0)
                    {
                        betterMoves.Add(item);
                        m_BoardMatrix = (Pawn[,])originalMatrix.Clone();
                        break;
                    }

                    dummyListPlayer = SetListOfPossibleMoves(item.DestRow, item.DestCol, 0, false);

                    if (!dummyListPlayer[0].EatMove)
                    {
                        betterMoves.Add(item);
                    }
                }

                m_BoardMatrix = (Pawn[,])originalMatrix.Clone();
            }

            if (betterMoves.Count != 0)
            {
                bestMove = betterMoves[RandNum.Next(betterMoves.Count)];
            }
            else
            {
                bestMove = possibleMovesForComputer[RandNum.Next(possibleMovesForComputer.Count)];
            }

            i_iAte = bestMove.EatMove;
            MakeAStep(bestMove.OriginCol, bestMove.OriginRow, bestMove);
            i_thereIsAnotherEat = isAnotherEat(bestMove.DestRow, bestMove.DestCol, 1);
        }

        internal bool IsGameOver()
        {
            int counterPlayerOne = 0, counterPlayerTwo = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (m_BoardMatrix[i, j] != null)
                    {
                        if (m_BoardMatrix[i, j].GetColorId() == 0)
                        {
                            counterPlayerOne++;
                        }
                        else
                        {
                            counterPlayerTwo++;
                        }
                    }
                }
            }

            return counterPlayerOne == 0 || counterPlayerTwo == 0;
        }

        internal int Scoring(int i_winner)
        {
            int counterPlayerOne = 0, counterPlayerTwo = 0, score;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (m_BoardMatrix[i, j] != null)
                    {
                        if (m_BoardMatrix[i, j].GetColorId() == 0)
                        {
                            if (m_BoardMatrix[i, j].IsKing())
                            {
                                counterPlayerOne += 4;
                            }
                            else
                            {
                                counterPlayerOne++;
                            }
                        }
                        else
                        {
                            if (m_BoardMatrix[i, j].IsKing())
                            {
                                counterPlayerTwo += 4;
                            }
                            else
                            {
                                counterPlayerTwo++;
                            }
                        }
                    }
                }
            }

            if (i_winner == 0)
            {
                score = counterPlayerOne - counterPlayerTwo;
            }
            else
            {
                score = counterPlayerTwo - counterPlayerOne;
            }

            return score;
        }

        public bool CheckValidation(int i_originCol, int i_originRow, int i_destCol, int i_destRow, int i_playerTurn, bool isSeconedEat) // USER TURN
        {
            bool isValid = false;
            Pawn currentPawn = m_BoardMatrix[i_originRow, i_originCol];
            if (currentPawn == null || !currentPawn.GetColorId().Equals(i_playerTurn))
            {
                isValid = false;
            }
            else if (i_destRow < 0 || i_destRow > BoardSize || i_destCol < 0 || i_destCol > BoardSize)
            {
                isValid = false;
            }
            else
            {
                List<PossibleMove> possibleMoves = SetListOfPossibleMoves(i_originRow, i_originCol, i_playerTurn, isSeconedEat);
                foreach (PossibleMove item in possibleMoves)
                {
                    if (i_originRow == item.OriginRow && i_originCol == item.OriginCol && i_destRow == item.DestRow && i_destCol == item.DestCol)
                    {
                        isValid = true;
                        MakeAStep(i_originCol, i_originRow, item);
                        break;
                    }
                }

                possibleMoves.Clear();
            }

            return isValid;
        }

        public List<PossibleMove> SetListOfPossibleMoves(int i_originRow, int i_originCol, int i_playerTurn, bool isSeconedEat)
        {
            List<PossibleMove> possibleMoves = new List<PossibleMove>();
            if (isSeconedEat)
            {
                bool isEatingOption = true;
                SetPossibleMovesListForPawn(i_originRow, i_originCol, i_playerTurn, ref isEatingOption, possibleMoves);
            }
            else
            {
                bool isEatingOption = false;
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (m_BoardMatrix[i, j] != null && m_BoardMatrix[i, j].GetColorId() == i_playerTurn)
                        {
                            SetPossibleMovesListForPawn(i, j, i_playerTurn, ref isEatingOption, possibleMoves);
                        }
                    }
                }
            }

            return possibleMoves;
        }

        private void SetPossibleMovesListForPawn(int i_originRow, int i_originCol, int i_playerTurn, ref bool isEatingOption, List<PossibleMove> possibleMoves)
        {
            bool pawnIsKing = m_BoardMatrix[i_originRow, i_originCol].IsKing();
            int i = i_originRow, j = i_originCol;
            if ((i_playerTurn == 0 || pawnIsKing) && i + 1 < BoardSize)
            {
                if (j + 1 < BoardSize)
                {
                    if (m_BoardMatrix[i + 1, j + 1] != null && m_BoardMatrix[i + 1, j + 1].GetColorId() != i_playerTurn)
                    {
                        if (j + 2 < BoardSize && i + 2 < BoardSize && m_BoardMatrix[i + 2, j + 2] == null)
                        {
                            if (!isEatingOption)
                            {
                                possibleMoves.Clear();
                            }

                            isEatingOption = true;
                            possibleMoves.Add(new PossibleMove(i, j, i + 2, j + 2, true));
                        }
                    }
                    else if (m_BoardMatrix[i + 1, j + 1] == null && !isEatingOption)
                    {
                        possibleMoves.Add(new PossibleMove(i, j, i + 1, j + 1, false));
                    }
                }

                if (j - 1 >= 0)
                {
                    if (m_BoardMatrix[i + 1, j - 1] != null && m_BoardMatrix[i + 1, j - 1].GetColorId() != i_playerTurn)
                    {
                        if (j - 2 >= 0 && i + 2 < BoardSize && m_BoardMatrix[i + 2, j - 2] == null)
                        {
                            if (!isEatingOption)
                            {
                                possibleMoves.Clear();
                            }

                            isEatingOption = true;
                            possibleMoves.Add(new PossibleMove(i, j, i + 2, j - 2, true));
                        }
                    }
                    else if (m_BoardMatrix[i + 1, j - 1] == null && !isEatingOption)
                    {
                        possibleMoves.Add(new PossibleMove(i, j, i + 1, j - 1, false));
                    }
                }
            }

            if ((i_playerTurn == 1 || pawnIsKing) && i - 1 >= 0)
            {
                if (j - 1 >= 0)
                {
                    if (m_BoardMatrix[i - 1, j - 1] != null && m_BoardMatrix[i - 1, j - 1].GetColorId() != i_playerTurn)
                    {
                        if (j - 2 >= 0 && i - 2 >= 0 && m_BoardMatrix[i - 2, j - 2] == null)
                        {
                            if (!isEatingOption)
                            {
                                possibleMoves.Clear();
                            }

                            isEatingOption = true;
                            possibleMoves.Add(new PossibleMove(i, j, i - 2, j - 2, true));
                        }
                    }
                    else if (m_BoardMatrix[i - 1, j - 1] == null && !isEatingOption)
                    {
                        possibleMoves.Add(new PossibleMove(i, j, i - 1, j - 1, false));
                    }
                }

                if (j + 1 < BoardSize)
                {
                    if (m_BoardMatrix[i - 1, j + 1] != null && m_BoardMatrix[i - 1, j + 1].GetColorId() != i_playerTurn)
                    {
                        if (j + 2 < BoardSize && i - 2 >= 0 && m_BoardMatrix[i - 2, j + 2] == null)
                        {
                            if (!isEatingOption)
                            {
                                possibleMoves.Clear();
                            }

                            isEatingOption = true;
                            possibleMoves.Add(new PossibleMove(i, j, i - 2, j + 2, true));
                        }
                    }
                    else if (m_BoardMatrix[i - 1, j + 1] == null && !isEatingOption)
                    {
                        possibleMoves.Add(new PossibleMove(i, j, i - 1, j + 1, false));
                    }
                }
            }
        }

        private void MakeAStep(int i_originCol, int i_originRow, PossibleMove item)
        {
            m_BoardMatrix[item.DestRow, item.DestCol] = m_BoardMatrix[i_originRow, i_originCol];
            m_BoardMatrix[i_originRow, i_originCol] = null;
            if (item.EatMove)
            {
                int eated_i = (i_originRow < item.DestRow) ? item.DestRow - 1 : i_originRow - 1;
                int eated_j = (i_originCol < item.DestCol) ? item.DestCol - 1 : i_originCol - 1;
                m_BoardMatrix[eated_i, eated_j] = null;
            }

            if (item.DestRow == 0 || item.DestRow == BoardSize - 1)
            {
                m_BoardMatrix[item.DestRow, item.DestCol].Coronation();                         // set as king
            }
        }

        public bool isAnotherEat(int i_newOriginRow, int i_newOriginCol, int i_playerTurn)
        {
            List<PossibleMove> possibleEatMove = new List<PossibleMove>();
            bool isEatingOption = true;
            bool isAnotherEat = true;
            SetPossibleMovesListForPawn(i_newOriginRow, i_newOriginCol, i_playerTurn, ref isEatingOption, possibleEatMove);
            if (possibleEatMove.Count == 0)
            {
                isAnotherEat = false;
            }

            possibleEatMove.Clear();
            return isAnotherEat;
        }
    }
}
