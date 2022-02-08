using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class LogBLL
    {
        LogDAO dao = new LogDAO();
        public static void AddLog(int ProcessType, string TableName, int ProcessID)
        {
            LogDAO.AddLog(ProcessType, TableName, ProcessID);
        }

        public List<LogDTO> GetLogs()
        {
            return dao.GetLogs();
        }
    }
}
