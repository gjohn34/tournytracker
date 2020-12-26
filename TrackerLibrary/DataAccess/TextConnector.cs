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
        private const string PeopleFile = "PersonModels.csv";


        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            int currentId = 1;
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id;
            }
            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizesFile(PrizesFile);

            return model;
        }
        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
            int currentId = 0;
            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id;
            }
            model.Id = currentId;

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;

        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
            //throw new NotImplementedException();
        }
    }
}
