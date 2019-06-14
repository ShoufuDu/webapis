using System;
using System.Threading.Tasks;

namespace MyProducts.Models
{
    public class MyArrayCalc : IArrayCalc
    {
        public MyArrayCalc()
        {

        }

        //Swap the two ends data to complete the process of reversing
        public async Task<int[]> Reverse(int[] nums)
        {
            if (nums.Length == 0)
                return nums;

            int[] res = await Task.Run(() =>
            {
                int len = nums.Length;
                int[] result = new int[len];
                for (int i = 0; i < len; i++)
                {
                    result[i] = nums[len - i - 1];
                }

                return result;
            });

            return res;
        }


        public async Task<int[]> Delete(int position, int[] nums)
        {
            int len = nums.Length;

            if (position < 0 || position >= len)
                throw new ArgumentException();

            int[] res = await Task.Run(() =>
            {
                int[] result = new int[len - 1];
                int i = 0, index = 0;
                while (i < len)
                {
                    if (i != position)
                        result[index++] = nums[i];
                    i++;
                }

                return result;
            });

            return res;
        }
    }
}
