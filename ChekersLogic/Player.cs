
namespace Checkers
{
   public class Player
    {
        private string m_UserName;
        private int m_TotalPoints;
        public Player(string i_userName)
        {
            UserName = i_userName;
            m_TotalPoints = 0;
        }

        public Player()
        {
            m_TotalPoints = 0;
        }

        public void AddPoints(int i_points)
        {
            m_TotalPoints += i_points;
        }

        public int GetTotalPoints()
        {
            return m_TotalPoints;
        }

        public string UserName 
        {
            get
            {
                return m_UserName;
            }

            set
            {
                if (value.Length > 20)
                {
                    m_UserName = value.Substring(0, 20);
                }
                else
                {
                    m_UserName = value;
                }
            }
        }
    }
}
