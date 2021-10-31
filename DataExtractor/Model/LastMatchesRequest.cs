using Newtonsoft.Json;

namespace DotaStat.DataExtractor.Model
{
    public class LastMatchesRequest
    {
        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public class Result
    {

        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("num_results")]
        public int NumResults { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
        [JsonProperty("results_remaining")]
        public int ResultsRemaining { get; set; }
        [JsonProperty("matches")]
        public Match[] Matches { get; set; }
    }

    public class Match
    {
        [JsonProperty("matchId")]
        public long MatchId { get; set; }
        [JsonProperty("radiant_win")]
        public bool RadiantWin { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("match_seq_num")]
        public long MatchSeqNum { get; set; }
        [JsonProperty("start_time")]
        public int StartTime { get; set; }
        [JsonProperty("lobby_type")]
        public int LobbyType { get; set; }
        [JsonProperty("radiant_team_id")]
        public int RadiantTeamId { get; set; }
        [JsonProperty("dire_team_id")]
        public int DireTeamId { get; set; }
        [JsonProperty("players")]
        public Player[] Players { get; set; }
        [JsonProperty("picks_bans")]
        public PicksBans[] PicksBans { get; set; }
    }

    public class Player
    {
        [JsonProperty("account_id")]
        public long AccountId { get; set; }
        [JsonProperty("player_slot")]
        public int PlayerSlot { get; set; }
        [JsonProperty("hero_id")]
        public int HeroId { get; set; }
    }

    public class PicksBans
    {
        [JsonProperty("is_pick")]
        public bool IsPick { get; set; }
        [JsonProperty("hero_id")]
        public int HeroId { get; set; }
        [JsonProperty("team")]
        public int Team { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
    }
}