using HospitalModels.Library;
using System;
using System.Collections.Generic;

namespace HospitalRepository.Library
{
    public class MoneyReceiveHistoryRepository
    {
        private readonly string _connectionString = DataManagers.DataManager.ConnectionString();

        public IEnumerable<MoneyReceiveHistory> GetAllByDate(string date)
        {
            var list = new List<MoneyReceiveHistory>();

            string query = "select t1.EntryDate, t3.FirstName +' '+t3.LastName as MoneyReceiveBy, t1.PayMethode, t2.PayAmount as PayAmount from MoneyReceiveDtl as t1 inner join MoneyReceiveMst as t2 on t1.MoneyReceiveMstId=t2.Id left join Users as t3 on t1.MoneyReceiveBy=t3.Id WHERE Convert(date, t1.EntryDate)='" + date + "'";

            var reader = DataManagers.DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var mrHistory = new MoneyReceiveHistory();
                    mrHistory.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                    mrHistory.MoneyReceiveBy = reader["MoneyReceiveBy"].ToString();
                    mrHistory.PayMethode = reader["PayMethode"].ToString();
                    mrHistory.PayAmount = Convert.ToDecimal(reader["PayAmount"]);

                    list.Add(mrHistory);
                }
            }
            return list;
        }

        public IEnumerable<MoneyReceiveHistory> GetAllByDate(string startDate,string EndDate)
        {
            var list = new List<MoneyReceiveHistory>();

            string query = "select t1.EntryDate, t3.FirstName +' '+t3.LastName as MoneyReceiveBy, t1.PayMethode, t2.PayAmount as PayAmount from MoneyReceiveDtl as t1 inner join MoneyReceiveMst as t2 on t1.MoneyReceiveMstId=t2.Id left join Users as t3 on t1.MoneyReceiveBy=t3.Id WHERE t2.PayAmount>0 and  Convert(date, t1.EntryDate,103) between convert(date,'" + startDate+"',103) and convert(date,'"+EndDate+"',103)";

            var reader = DataManagers.DataManager.SqlDataReader(query, _connectionString);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var mrHistory = new MoneyReceiveHistory();
                    mrHistory.EntryDate = Convert.ToDateTime(reader["EntryDate"]);
                    mrHistory.MoneyReceiveBy = reader["MoneyReceiveBy"].ToString();
                    mrHistory.PayMethode = reader["PayMethode"].ToString();
                    mrHistory.PayAmount = Convert.ToDecimal(reader["PayAmount"]);

                    list.Add(mrHistory);
                }
            }
            return list;
        }

    }
}
