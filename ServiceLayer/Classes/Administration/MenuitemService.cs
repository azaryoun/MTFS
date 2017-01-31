using System.Collections.Generic;
using System.Linq;
using MTFS.DAL.Context;
using System.Data.Entity;
using MTFS.Business.Domain.Model;
using MTFS.Business.Dtos.DtoClasses;
using MTFS.Business.Services.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace MTFS.Business.Services.Classes
{
    public class MenuitemService : IMenuitemService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Menuitem> _Menuitems;
        private readonly IDbSet<AccessgroupMenuitem> _AccessgroupMenuitems;
         
        public MenuitemService(IUnitOfWork contextIOC)
        {

            _uow = contextIOC;
            _Menuitems = _uow.Set<Menuitem>();
            _AccessgroupMenuitems = _uow.Set<AccessgroupMenuitem>();

        }
         
        public async Task<List<MenuitemDto>> getMenuitemsDto()
        { 
            var lnqMenuitems =await  _Menuitems.Include(r => r.menutitle)
                                                .AsNoTracking()
                                                .OrderBy(x => x.menutitle.displayOrder)
                                                .ToListAsync();

            return Mapper.Map<IEnumerable<Menuitem>, List<MenuitemDto>>(lnqMenuitems);
            //foreach (var itmMenuitem in lnqMenuitems)
            //{
            //    var oMenuitem = new  MenuitemDto();
            //    oMenuitem.id = itmMenuitem.id;
            //    oMenuitem.itemText = itmMenuitem.menutitle.titleText + "/" + itmMenuitem.itemText;
            //    oMenuitem.itemStyle = itmMenuitem.itemStyle;
            //    lstMenuitemsDto.Add(oMenuitem);
            //}

            //return (lstMenuitemsDto);


        }


        public async Task<List<MenuitemDto>> getMenuitemsDto(BaseDto baseDto)
        {

            //if (isItemAdmin == false)
            //    lnqMenuitems = lnqMenuitems
            //       .Where(x => x.accessgroupMenuitems.Any(
            //           y => y.accessgroup.accessgroupUsers.Any(
            //               z => z.userId == userId)))
            //        ;


            //var lnqMenuitems = _Menuitems.AsQueryable();


            //lnqMenuitems = lnqMenuitems.OrderBy(
            //    x => x.menutitle.displayOrder)
            //    ;

            //lnqMenuitems = lnqMenuitems.Include(r => r.menutitle);

            var lnqMenuitems =await  _Menuitems.Include(r => r.menutitle)
                                               .AsNoTracking()
                                               .OrderBy(x => x.menutitle.displayOrder)
                                               .ToListAsync();

            var lstMenuitems = new List<MenuitemDto>();
             
            foreach (var itmMenuitem in lnqMenuitems)
            {
                var oMenuitem = new MenuitemDto();
                oMenuitem.id = itmMenuitem.id;
                oMenuitem.itemText = itmMenuitem.menutitle.titleText + "/" + itmMenuitem.itemText;
                oMenuitem.itemStyle = itmMenuitem.itemStyle;

                var lnqAccessgroupMenuitems =await  _AccessgroupMenuitems
                                                    .SingleOrDefaultAsync(x =>
                                                    x.accessgroupId == baseDto.id &&
                                                    x.menuitemId == itmMenuitem.id  );

                if (lnqAccessgroupMenuitems != null)
                    oMenuitem.isChecked = true;
                 
                lstMenuitems.Add(oMenuitem);
            }

            return (lstMenuitems);
        }


    }

}
