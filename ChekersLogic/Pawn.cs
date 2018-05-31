
namespace Checkers
{
    public class Pawn
    {
        private bool m_IsKing;
        private char m_Symbol;
        private int m_ColorID;

        public Pawn(char i_symbol, int i_colorID)
        {
            m_IsKing = false;
            Symbol = i_symbol;
            m_ColorID = i_colorID;
        }

        public int GetColorId()
        {
            return m_ColorID;
        }

        public void Coronation() // set as king
        {
            if (!m_IsKing)
            {
                Symbol = Symbol == 'O' ? 'U' : 'K';
                m_IsKing = true;
            }
        }

        public bool IsKing()
        {
            return m_IsKing;
        }

        public char Symbol
        {
            get
            {
                return m_Symbol;
            }

            set
            {
                m_Symbol = value;
            }
        }
    }
}
