using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();

        }
        public string CreatePilot(string fullName)
        {
            
            var pilot = pilotRepository.FindByName(fullName);
            if (pilot != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            pilotRepository.Add(new Pilot(fullName));
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            
            var carExisting = carRepository.FindByName(model);
            if (carExisting != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type != "Ferrari" && type != "Williams" )
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar,type));
            }

            IFormulaOneCar car = null;

            if (type == "Ferrari")
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == "Williams")
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }
            carRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
           var raceExisting = raceRepository.FindByName(raceName);
           if (raceExisting != null)
           {
               throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
           }
           raceRepository.Add(new Race(raceName, numberOfLaps));
           return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);

        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var findPilot = pilotRepository.FindByName(pilotName);
            if (findPilot == null || findPilot.Car == null)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            var carModelFound = carRepository.FindByName(carModel);
            if (carModelFound == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }
            findPilot.AddCar(carModelFound);
            carRepository.Remove(carModelFound);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName , carModelFound.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var raceExisting = raceRepository.FindByName(raceName);
            if (raceExisting == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var pilot = pilotRepository.FindByName(pilotFullName);
            if (pilot == null || !pilot.CanRace || raceExisting.Pilots.Contains(pilot))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage,
                    pilotFullName));
            }
            raceExisting.Pilots.Add(pilot);
            return string.Format(string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName));
        }

        public string StartRace(string raceName)
        {
            var raceFind = raceRepository.FindByName(raceName);
            if (raceFind == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            if (raceFind.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (raceFind.TookPlace)
            {
                throw new InvalidOperationException(
                    string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            List<IPilot> pilots = raceFind.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(raceFind.NumberOfLaps)).ToList();
            pilots[0].WinRace();
            raceFind.TookPlace = true;
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(OutputMessages.PilotFirstPlace, pilots[0].FullName, raceName));
            sb.AppendLine(string.Format(OutputMessages.PilotSecondPlace, pilots[1].FullName, raceName));
            sb.AppendLine(string.Format(OutputMessages.PilotThirdPlace, pilots[2].FullName, raceName));

            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var race in raceRepository.Models.Where(x => x.TookPlace == true))
            {
                sb.AppendLine(race.RaceInfo());
            }
            return sb.ToString().Trim();
        }

        public string PilotReport()
        {
            StringBuilder info = new StringBuilder();
            foreach (var pilot in pilotRepository.Models.OrderByDescending(x => x.NumberOfWins))
            {
                info.AppendLine(pilot.ToString());
            }
            return info.ToString().Trim();  
        }
    }
}
