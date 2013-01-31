using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.DAL
{
    public class DALFacade
    {
        private DAL.OraDocCap_PruebasEntities _containerRCR = new DAL.OraDocCap_PruebasEntities();
        private DAL.PEIS_DATABASEEntities _containerPEIS = new DAL.PEIS_DATABASEEntities();
        RCRRepository _repositoryRCR;
        TRRepository _repositoryTR;
        public DALFacade()
        {
            _repositoryRCR = new RCRRepository(_containerRCR);
            _repositoryTR = new TRRepository(_containerPEIS);
        }


        public List<String> getRCRTypes()
        {
            return _repositoryRCR.getRCRTypes();
        }

        public List<String> getUsernames() // Devuelve solo la lista de nombres
        {
            return _repositoryRCR.getUserNames();
        }

        public gamif_user getUserData(String name)
        {
            return _repositoryRCR.getUserData(name);
        }

        public List<Double> getRateRCR() // 0: 0.75   1: 1.0   2: 1.25
        {
            List<Double> l = new List<Double>();
            l.Add(0.75);
            l.Add(1);
            l.Add(1.25);
            return l;
        }

        public Int32 getTotalRCRsDonebyUser(String username, DateTime dateBegin, DateTime dateEnd)
        {
            return _repositoryRCR.getTotalRCRDoneByUser(username, dateBegin, dateEnd);
        }

        public List<RCR> getRCRsDoneByUser(String username, DateTime dateBegin, DateTime dateEnd)
        {
            return _repositoryRCR.getRCRDoneByUser(username, dateBegin, dateEnd);
        }

        public Double getReportByDate(DateTime StartDate, DateTime EndDate, String UserID)
        {
            return _repositoryTR.getReportByDate(StartDate, EndDate, UserID);
        }


        public RCR getRCRbyID(string p)
        {
            return _repositoryRCR.getRCRbyID(p);
        }

        public List<Models.leaderboardViewModel> getLeaderBoard(List<String> types, DateTime StartDate, DateTime EndDate)
        {
            List<Models.leaderboardViewModel> leaderBoard = new List<Models.leaderboardViewModel>();
            leaderBoard = _repositoryRCR.getLeaderBoard(types, StartDate, EndDate);
            if (types.Count == 1 && types.ElementAt(0).Equals("Todos"))
            {
                for (int i = 0; i < leaderBoard.Count; i++)
                {
                    gamif_user userdata = _repositoryRCR.getUserData(leaderBoard.ElementAt(i).username);
                    leaderBoard.ElementAt(i).iconPath = userdata.user_avatar;
                    double totalHours = _repositoryTR.getReportByDate(StartDate, EndDate, userdata.peis_id.ToString());
                    double scoreStars = Convert.ToDouble(leaderBoard.ElementAt(i).score);
                    double eficiency = (scoreStars / totalHours) * 100;
                    leaderBoard.ElementAt(i).efficiency = "" + Math.Round(eficiency, 2);

                }
            }
            else
            {
                for (int i = 0; i < leaderBoard.Count; i++)
                {
                    gamif_user userdata = _repositoryRCR.getUserData(leaderBoard.ElementAt(i).username);
                    leaderBoard.ElementAt(i).iconPath = userdata.user_avatar;
                    double totalHours = _repositoryTR.getReportByDate(StartDate, EndDate, userdata.peis_id.ToString());
                    double scoreStars = Convert.ToDouble(leaderBoard.ElementAt(i).score);
                }
            }
            //leaderBoard.
            return leaderBoard.OrderByDescending(o => Convert.ToDouble(o.score)).ThenByDescending(o => Convert.ToDouble(o.efficiency)).ToList();

        }
    }

}
