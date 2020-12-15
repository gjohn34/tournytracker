using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";


        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            int lastId = 1;
            if (prizes.Count > 0)
            {
                lastId = prizes.OrderByDescending(x => x.Id).First().Id;

            }
            model.Id = lastId + 1;

            prizes.Add(model);

            prizes.SaveToPrizesFile(PrizesFile);

            return model;
        }

    }
}
