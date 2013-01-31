using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.DAL
{
    public class TRRepository
    {
        DAL.PEIS_DATABASEEntities _containerTimeReport;

        public TRRepository(DAL.PEIS_DATABASEEntities _containerTR)
        {
            this._containerTimeReport = _containerTR;
        }

        internal Int32 getResourceID(String name)
        {
            return 0;
        }

        internal Double getReportByDate(DateTime StartDate, DateTime EndDate, String UserID)
        {
            List<DAL.GTTimePeriod> idSemanas = _containerTimeReport.GTTimePeriod.Where(t => t.gtStart >= StartDate && t.gtStart <= EndDate).ToList();
            List<GTAssignment> gtIDGetAssignment = _containerTimeReport.GTAssignment.Where(t => t.gtResourceID.Equals(UserID) && t.gtTaskIntID == 33408).ToList();
            Decimal gtAssign = gtIDGetAssignment.ElementAt(0).gtID;
            List<DAL.GTTimeEntry> ReportByDate1 = new List<GTTimeEntry>();
            ReportByDate1 = (from ts in _containerTimeReport.GTTimeSheet
                             join te in _containerTimeReport.GTTimeEntry on ts.gtID equals te.gtTimeSheetID
                             join tp in _containerTimeReport.GTTimePeriod on ts.gtTimePeriodID equals tp.gtID
                             where te.gtAssignmentID == gtAssign
                             where (tp.gtStart >= StartDate) && (tp.gtStart <= EndDate)
                             select te).ToList();

            /*List<Decimal> gtAssignID = new List<Decimal>();
            /*foreach(GTAssignment gtassign in gtIDGetAssignment)
            {
                gtAssignID.Add((decimal)gtassign.gtExternalID);
            }
            List<Decimal> TimeSheets = new List<decimal>();
            foreach(GTTimePeriod Semana in idSemanas)
            {
                Decimal user = Convert.ToDecimal(UserID);
                Decimal idSemana = Semana.gtID;
                GTTimeSheet gtIDGetSheet = _containerTimeReport.GTTimeSheet.Where(t => t.gtTimePeriodID == idSemana && t.gtResourceID == user).FirstOrDefault(); //t.gtTimePeriodID==idSemana && 
                if (gtIDGetSheet != null)
                {
                    TimeSheets.Add(gtIDGetSheet.gtID);
                }
            }
            List<DAL.GTTimeEntry> ReportByDate = new List<GTTimeEntry>();
            foreach(Decimal d in TimeSheets)
            {
                ReportByDate.Add(_containerTimeReport.GTTimeEntry.Where(t => t.gtAssignmentID == gtAssign && t.gtTimeSheetID == (d)).FirstOrDefault());//&& t.gtTimeSheetID.Equals(d) || 293089 293368 293632 293902
            }
            

            
            
            */
            Double totalHours = new Double();
            foreach (GTTimeEntry te in ReportByDate1)
            {
                if (te == null)
                {
                    totalHours += 0;
                }
                else
                {
                    double hours = Convert.ToDouble(te.gtActualWeek);
                    totalHours += hours;
                }
            }
            return totalHours;
        }
    }
}
