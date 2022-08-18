using System;
using System.Text;

namespace Basketball
{
    public class Player
    {
        private string name;
        private string position;
        private double rating;
        private int games;
        private bool retired;

        public Player(string name, string position, double rating, int games)
        {
            this.Name = name;
            this.Position = position;
            this.Rating = rating;
            this.Games = games;
            this.Retired = false;
        }

        public string Name { get => this.name; set => this.name = value; }
        public string Position { get => this.position; set => this.position = value; }
        public double Rating { get => this.rating; set => this.rating = value; }
        public int Games { get => this.games; set => this.games = value; }
        public bool Retired { get => this.retired; set => this.retired = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"-Player: {Name}")
                .AppendLine($"--Position: {Position}")
                .AppendLine($"--Rating: {Rating}")
                .AppendLine($"--Games played: {Games}");
            return sb.ToString();
        }
    }
}