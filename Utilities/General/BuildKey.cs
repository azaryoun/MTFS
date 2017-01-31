using System;

namespace MTFS.Utilities.General
{
    public static  class BuildKey
    {
        public static string BuildReferenceKey(string currentMax)
        {
            string part1 = currentMax.Substring(0, 1);
            string part2 = currentMax.Substring(1, 1);
            string part3 = currentMax.Substring(2, 1);
            string part4 = currentMax.Substring(3, 1);
            string part5 = currentMax.Substring(4, 1);
            string part3_counter = string.Empty ;

            if (part1 == "0" && part2 == "0" && part3 == "0" && part4 == "0" && part5 != "0")
            {
                byte byte_Part5 = Convert.ToByte(part5);
                byte_Part5 += 1;

                if (byte_Part5 <= 9)
                    part3_counter = "0000" + byte_Part5.ToString();
                else
                    part3_counter = "00010";

            }

            else if (part1 == "0" && part2 == "0" && part3 == "0" && part4 != "0")
            {
                byte part4_5 = Convert.ToByte((part4 + part5));
                part4_5 += 1;

                if (part4_5 <= 99)
                    part3_counter = "000" + part4_5.ToString();
                else
                    part3_counter = "00100";

            }

            else if (part1 == "0" && part2 == "0" && part3 != "0")
            {
                int part3_4_5 = Convert.ToInt16((part3 + part4 + part5));
                part3_4_5 += 1;

                if (part3_4_5 <= 999)
                    part3_counter = "00" + part3_4_5.ToString();
                else
                    part3_counter = "01000";

            }

            else if (part1 == "0" && part2 != "0")
            {
                int part2_3_4_5 = Convert.ToInt16((part2 + part3 + part4 + part5));
                part2_3_4_5 += 1;

                if (part2_3_4_5 <= 9999)
                    part3_counter = "0" + part2_3_4_5.ToString();
                else
                    part3_counter = "10000";

            }

            else if (part1 != "0")
            {
                if (true)
                {
                    int part1_2_3_4_5 = Convert.ToInt32((part1 + part2 + part3 + part4 + part5));
                    part1_2_3_4_5 += 1;

                    if (part1_2_3_4_5 <= 99999)
                        part3_counter = part1_2_3_4_5.ToString();
                    else
                        throw new OverflowException();

                }
            }

            return part3_counter;
        }
    }
}
