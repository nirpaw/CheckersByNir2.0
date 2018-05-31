
namespace Checkers
{
    public struct PossibleMove
    {
        private int m_OriginRow;
        private int m_OriginCol;
        private int m_DestRow;
        private int m_DestCol;
        private bool m_EatMove;
        
        public PossibleMove(int i_originRow, int i_originCol, int i_destRow, int i_destCol, bool i_eat)
        {
            m_OriginRow = i_originRow;
            m_OriginCol = i_originCol;
            m_DestRow = i_destRow;
            m_DestCol = i_destCol;
            m_EatMove = i_eat;
        }

        public bool EatMove
        {
            get
            {
                return m_EatMove;
            }

            set
            {
                m_EatMove = value;
            }
        }      

        public int OriginRow
        {
            get
            {
                return m_OriginRow;
            }

            set
            {
                m_OriginRow = value;
            }
        }

        public int OriginCol
        {
            get
            {
                return m_OriginCol;
            }

            set
            {
                m_OriginCol = value;
            }
        }

        public int DestRow
        {
            get
            {
                return m_DestRow;
            }

            set
            {
                m_DestRow = value;
            }
        }

        public int DestCol
        {
            get
            {
                return m_DestCol;
            }

            set
            {
                m_DestCol = value;
            }
        }
    }
}
