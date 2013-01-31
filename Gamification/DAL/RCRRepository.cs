using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.DAL
{
    public class RCRRepository
    {
        DAL.OraDocCap_PruebasEntities _containerRCR;

        public RCRRepository(DAL.OraDocCap_PruebasEntities _containerRCR)
        {
            this._containerRCR = _containerRCR;
        }

        internal RCR getRCRbyID(string p)
        {
            return _containerRCR.RCR.Where(c => c.Número.Equals(p)).FirstOrDefault();
        }

        internal List<String> getRCRTypes()
        {
            List<RCR> RCRTypes = new List<RCR>();
            RCRTypes = _containerRCR.RCR.Distinct().ToList();
            List<String> rcrTypesList = new List<String>();
            foreach (RCR element in RCRTypes)
            {
                if (!rcrTypesList.Contains(element.Tipo_RCR))
                {
                    rcrTypesList.Add(element.Tipo_RCR);
                }
            }
            return rcrTypesList;
        }


        internal gamif_user getUserData(String name)
        {
            gamif_user UserData = new gamif_user();
            UserData = _containerRCR.gamif_user.Where(c => c.username.Equals(name)).FirstOrDefault();
            return UserData;
            /**
             * Metodo por si a Mauro no le sirve obtener un tipo gamif_user
            /*List<String> ReturnString = new List<string>();
            String codTR = ""+UserData.peis_id;
            String avatarRoute = UserData.user_avatar;
            ReturnString.Add(codTR);
            ReturnString.Add(avatarRoute);
            return ReturnString;*/
        }
        internal List<String> getUserNames()
        {
            List<gamif_user> completeUserData = _containerRCR.gamif_user.ToList();
            List<String> UserNames = new List<string>();
            foreach (gamif_user a in completeUserData)
            {
                UserNames.Add(a.username);
            }
            return UserNames;
        }

        internal Int32 getTotalRCRDoneByUser(String name, DateTime dateBegin, DateTime dateEnd)
        {
            return _containerRCR.RCR.Where(c => c.Analista_FACT.Equals(name) && c.Fec_hora_Pase_a_Prod__CSOPE_ > dateBegin && c.Fec_hora_Pase_a_Prod__CSOPE_ < dateEnd).Count();
        }

        internal List<RCR> getRCRDoneByUser(String name, DateTime dateBegin, DateTime dateEnd)
        {
            return _containerRCR.RCR.Where(c => c.Analista_FACT.Equals(name) && c.Fec_hora_Pase_a_Prod__CSOPE_ > dateBegin && c.Fec_hora_Pase_a_Prod__CSOPE_ < dateEnd).ToList();
        }

        internal List<RCR> getRCRDoneByUser(String name, DateTime dateBegin, DateTime dateEnd, List<String> types)
        {
            List<RCR> result2 = new List<RCR>();
            List<RCR> result1 = _containerRCR.RCR.Where(c => c.Analista_FACT.Equals(name) && c.Fec_hora_Pase_a_Prod__CSOPE_ > dateBegin && c.Fec_hora_Pase_a_Prod__CSOPE_ < dateEnd).ToList();
            foreach (RCR r in result1)
            {
                if (types.Contains(r.Tipo_RCR))
                {
                    result2.Add(r);
                }
            }
            return result2;
        }

        internal List<Models.leaderboardViewModel> getLeaderBoard(List<String> typesRCR, DateTime StartDate, DateTime EndDate)
        {
            /**
             * Primero flitrar por tipo
             * Despues por usuario
             * Despues por prioridad
             * Retornar 
             */
            //Crear la respuesta
            List<String> names = getUserNames();
            List<Models.leaderboardViewModel> response = new List<Models.leaderboardViewModel>();
            //Verificar si el tipo es Todos
            if (typesRCR.Count == 1 && typesRCR.ElementAt(0).Equals("Todos"))
            {
                foreach (String s in names)
                {
                    List<RCR> temp = getRCRDoneByUser(s, StartDate, EndDate);
                    double starsSum = 0;
                    foreach (RCR r in temp)
                    {
                        if (r.Complejidad.Trim().Equals("Alta"))
                        {
                            starsSum += 3;
                        }
                        else if (r.Complejidad.Trim().Equals("Media"))
                        {
                            starsSum += 1;
                        }
                        else
                        {
                            starsSum += 0.5;
                        }
                    }
                    Models.leaderboardViewModel tempRating = new Models.leaderboardViewModel();
                    tempRating.username = s;
                    tempRating.rcrsDone = "" + temp.Count;
                    tempRating.score = "" + starsSum;
                    response.Add(tempRating);
                }
            }
            else
            {
                foreach (String s in names)
                {
                    List<RCR> temp = getRCRDoneByUser(s, StartDate, EndDate, typesRCR);
                    double starsSum = 0;
                    foreach (RCR r in temp)
                    {
                        if (typesRCR.Contains(r.Tipo_RCR))
                        {
                            if (r.Complejidad.Trim().Equals("Alta"))
                            {
                                starsSum += 3;
                            }
                            else if (r.Complejidad.Trim().Equals("Media"))
                            {
                                starsSum += 1;
                            }
                            else
                            {
                                starsSum += 0.5;
                            }
                        }
                    }
                    Models.leaderboardViewModel tempRating = new Models.leaderboardViewModel();
                    tempRating.username = s;
                    tempRating.rcrsDone = "" + temp.Count;
                    tempRating.score = "" + starsSum;
                    response.Add(tempRating);
                }
            }
            return response;
        }
    }
}
