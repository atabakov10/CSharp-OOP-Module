using System;
using System.Collections.Generic;
using System.Text;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public abstract class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;

        private readonly ICollection<IPilot> pilots;


        protected Race(string raceName, int numberOfLaps)
        {
            this.RaceName = raceName;
            this.NumberOfLaps = numberOfLaps;
            pilots = new List<IPilot>();
        }
        public string RaceName
        {
            get => raceName;

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }
                raceName = value;
            }
        }
        public int NumberOfLaps
        {
            get => numberOfLaps;

            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidLapNumbers));
                }
                numberOfLaps = value;
            }
        }
        public bool TookPlace { get; set; } = false;
        public ICollection<IPilot> Pilots
        {
            get => pilots;
        }
        public void AddPilot(IPilot pilot)
        {
            Pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {raceName} race has:")
                .AppendLine($"Participants: {Pilots.Count}")
                .AppendLine($"Number of laps: {NumberOfLaps}")
                .AppendLine($"Took place: {(TookPlace ? "Yes" : "No")}");

            return sb.ToString().Trim();
        }
    }
}
