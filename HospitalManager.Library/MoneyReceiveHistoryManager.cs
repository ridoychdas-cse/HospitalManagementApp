using HospitalModels.Library;
using HospitalRepository.Library;
using System.Collections.Generic;

namespace HospitalManager.Library
{
    public class MoneyReceiveHistoryManager
    {
        private readonly MoneyReceiveHistoryRepository _mrRepository;

        public MoneyReceiveHistoryManager()
        {
            _mrRepository = new MoneyReceiveHistoryRepository();
        }

        public IEnumerable<MoneyReceiveHistory> GetAllByDate(string date)
        {
            return _mrRepository.GetAllByDate(date);
        }

        public IEnumerable<MoneyReceiveHistory> GetAllByDate(string startDate,string endDate)
        {
            return _mrRepository.GetAllByDate(startDate, endDate);
        }
    }
}
