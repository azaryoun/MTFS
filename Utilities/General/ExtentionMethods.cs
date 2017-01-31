using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTFS.Utilities.General
{
    public static class GeneralExtentionMethods
    {
  


        public static string GetPersianDate(this DateTime dateTime)
        {

            var persCal = new System.Globalization.PersianCalendar();
            return (persCal.GetYear(dateTime).ToString("0000") + "/" + persCal.GetMonth(dateTime).ToString("00") + "/" + persCal.GetDayOfMonth(dateTime).ToString("00"));

        }

        public static string GetPersianDateWithTime(this DateTime dateTime)
        {

            var strPerianDate = GetPersianDate(dateTime);
            return (strPerianDate + " " + dateTime.ToString("HH:mm:ss"));

        }

        public static DateTime GetGregorianDate(this string stringPdate)
        {

            var persCal = new System.Globalization.PersianCalendar();

            return (persCal.ToDateTime(int.Parse(stringPdate.Substring(0, 4)), int.Parse(stringPdate.Substring(5, 2)), int.Parse(stringPdate.Substring(8, 2)), 0, 0, 0, 0));

        }

        public static string getDataAdminText(this bool? blnValue)
        {

            if (blnValue.Value == true)
                return "ادمین";
            else
                return "نرمال";

        }

        public static string getRealLegalText(this bool? blnValue)
        {

            if (blnValue.Value == true)
                return "حقیقی";
            else
                return "حقوقی";

        }

        public static string getIsActiveText(this bool? blnValue)
        {

            if (blnValue.Value == true)
                return "فعال";
            else
                return "غیر فعال";

        }


    }
}