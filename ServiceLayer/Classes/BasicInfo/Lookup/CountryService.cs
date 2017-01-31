using AutoMapper;
using MTFS.DAL.Context;
using MTFS.Business.Domain.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Country> _Countries;

        public CountryService(IUnitOfWork uow)
        {

            _uow = uow;
            _Countries = _uow.Set<Country>();

        }
        public async Task<List<DdlDto>>  getCountriesDdlDto()
        {
            return (Mapper.Map<IEnumerable<Country>, List<DdlDto >>(await  _Countries.OrderBy(o=>o.countryName).AsNoTracking().ToListAsync()));
        }
    }
}
