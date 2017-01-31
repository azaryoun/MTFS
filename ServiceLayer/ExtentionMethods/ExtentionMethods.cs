using MTFS.Business.Domain.Model;
using System.Data.Entity;
using System.Linq;

namespace MTFS.Business.Services.ExtentionMethods
{
    public static class ExtentionMethods
    {
        public static IQueryable<T> GetPageRecords<T>(this IQueryable<T> queryable, int pageNo,int recordCountPerPage) where T : Base

        {

            var intFrom = (pageNo - 1) * recordCountPerPage;

            queryable = queryable
                 .OrderByDescending(x => x.id)
                 .Skip(intFrom)
                 .Take(recordCountPerPage)
                 .AsNoTracking()
                 ;

            return queryable;

        }

    }

}
