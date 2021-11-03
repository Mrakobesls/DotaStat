using DotaStat.Data.EntityFramework.Model;
using System.Collections.Generic;

namespace DotaStat.Business.Interfaces
{
    public interface IHeroService
    {
        public List<Hero> GetAllHeroes();
        public void EnsureAllHeroesExists();

    }
}
