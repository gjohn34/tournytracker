﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Model;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string filename)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{filename}";
        }
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach(string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel prize = new PrizeModel();
                prize.Id = int.Parse(cols[0]);
                prize.PlaceNumber = int.Parse(cols[1]);
                prize.PlaceName = cols[2];
                prize.PrizeAmount = decimal.Parse(cols[3]);
                prize.PrizePercentage = double.Parse(cols[4]);
                output.Add(prize);
            }
            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel prize = new PersonModel();
                prize.Id = int.Parse(cols[0]);
                prize.FirstName = cols[1];
                prize.LastName = cols[2];
                prize.EmailAddress = cols[3];
                prize.CellPhoneNumber = cols[4];
                output.Add(prize);
            }
            return output;
        }

        public static void SaveToPrizesFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel model in models)
            {
                lines.Add($"{model.Id},{model.PlaceNumber},{model.PlaceName},{model.PrizeAmount},{model.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);

        }
        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel model in models)
            {
                lines.Add($"{model.Id},{model.FirstName},{model.LastName},{model.EmailAddress},{model.CellPhoneNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }












    }
}
