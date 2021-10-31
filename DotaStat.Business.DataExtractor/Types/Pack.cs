using System.Collections;
using System.Collections.Generic;

namespace DotaStat.Business.Interfaces.Types
{
    public class Pack
    {
        public HeroMatchResult[] HeroWinrates = new HeroMatchResult[4455];
    }
    public class HeroMatchResult
    {
        public int FirstHero { get; set; }
        public int SecondHero { get; set; }
        public HeroRelations HeroRelations;
        public MatchResult MatchResult;
    }
}
