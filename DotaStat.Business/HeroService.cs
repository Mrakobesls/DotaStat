using System.Collections.Generic;
using DB_UnitOfWork.Inteface;
using DotaStat.Business.Interfaces;
using DotaStat.SteamPoweredApiProvider;
using System.Linq;
using DotaStat.Data.EntityFramework.Model;

namespace DotaStat.Business
{
    public class HeroService : GenericService, IHeroService
    {
        public HeroService(IUnitOfWork uow) : base(uow)
        {
        }

        public void EnsureAllHeroesExists()
        {
            var dotaHeroes = SteamPoweredApi.GetHeroes().Result.Heroes.OrderBy(x => x.Id);
            var localHeroesCount = _uow.Heroes.ReadAll().Count();

            if (dotaHeroes.Count() != localHeroesCount)
            {
                _uow.Heroes.ResetTable();
                foreach (var hero in dotaHeroes)
                {
                    _uow.Heroes.Create(new Hero()
                    {
                        Id = (byte)hero.Id,
                        Name = hero.LocalizedName
                    });
                }
                _uow.SaveChanges();
            }
        }

        public List<Hero> GetAllHeroes()
        {
            return _uow.Heroes.ReadAll().ToList();
        }
    }
}
