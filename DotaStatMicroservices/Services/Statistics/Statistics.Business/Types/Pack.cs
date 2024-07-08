using Statistics.Business.Enums;

namespace Statistics.Business.Types;

public class Pack
{
    public readonly List<HeroMatchResult> HeroWinCouples = [];
}
public class HeroMatchResult
{
    public int FirstHero { get; set; }
    public int SecondHero { get; set; }
    public HeroRelations HeroRelations;
    public MatchResult MatchResult;
}