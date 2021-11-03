using DotaStat.Business.Interfaces.Types;

namespace DotaStat.Business.Interfaces.Models
{
    public class Pack
    {
        public HeroMatchResult[] HeroWinСouples = new HeroMatchResult[4455];
    }
    public class HeroMatchResult
    {
        public int FirstHero { get; set; }
        public int SecondHero { get; set; }
        public HeroRelations HeroRelations;
        public MatchResult MatchResult;
    }
}
