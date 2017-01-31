using System.Collections.Generic;
using System.Threading.Tasks;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using MTFS.DAL.Context;
using MTFS.Business.Domain.Model;
using System.Data.Entity;
using AutoMapper;
using System.Linq;

namespace MTFS.Business.Services.Classes
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Currency> _Currencies;

        public CurrencyService(IUnitOfWork uow)
        {

            _uow = uow;
            _Currencies = _uow.Set<Currency>();

        }
        public async Task<List<DdlDto>> getCurrenciesDdlDto()
        {
            return (Mapper.Map<IEnumerable<Currency>, List<DdlDto>>(await _Currencies.OrderBy(o => o.name).AsNoTracking().ToListAsync()));
        }
    }
}
