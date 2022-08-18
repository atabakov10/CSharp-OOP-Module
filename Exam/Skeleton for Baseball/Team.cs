using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basketball
{
    public class Team
    {
        private List<Player> players;
        private string name;
        private int _openPosition;
        private char group;

        public Team(string name, int openPosition, char group)
        {
            this.Players = new List<Player>();
            this.Name = name;
            this.OpenPosition = openPosition;
            this.Group = group;
        }

        public List<Player> Players { get => this.players; set => players = value; }
        public string Name { get => this.name; set => this.name = value; }
        public int OpenPosition { get => this._openPosition; set => this._openPosition = value; }
        public char Group { get => this.group; set => this.group = value; }
        public int Count
        {
            get => this.Players.Count;
        }

        public string AddPlayer(Player player)
        {
            if (this.OpenPosition <= 0)
            {
                return "There are no more open positions.";
            }

            if (string.IsNullOrEmpty(player.Name))
            {
                return "Invalid player's information.";
            }
            if (player.Rating < 80)
            {
                return "Invalid player's rating";
            }

            this.OpenPosition--;
            this.players.Add(player);
            return $"Successfully added {player.Name} to the team. Remaining open positions: {this.OpenPosition}.";
        }
        public bool RemovePlayer(string name)
        {
            var playerToRemove = this.Players.FirstOrDefault(p => p.Name == name);
            
            return this.Players.Remove(playerToRemove);
        }

        public int RemovePlayerByPosition(string position)
        {
            var playersToRemove = this.Players.FindAll(p => p.Position == position);

            foreach (var player in playersToRemove)
            {
                this.Players.Remove(player);
                this.OpenPosition++;
            }

            if (playersToRemove.Count > 0)
            {
                return playersToRemove.Count();
            }
            else
            {
                return 0;
            }
        }

        public Player RetirePlayer(string name)
        {

            var playerToRetire = this.Players.FirstOrDefault(p => p.Name == name);
            if (playerToRetire != null)
            {
                playerToRetire.Retired = true;

                if (playerToRetire != null)
                {
                    return playerToRetire;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public List<Player> AwardPlayers(int games)
        {
            return this.Players.Where(p => p.Games >= games).ToList();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            var playerNotRetired = Players.Where(p => p.Retired == false);
            sb.AppendLine($"Active players competing for Team {Name} from Group {Group}:");
            foreach (var players in playerNotRetired)
            {
                sb.AppendLine($"{players}");
            }
            return sb.ToString().Trim();
        }

    }
}
