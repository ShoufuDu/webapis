using System;
using System.Threading.Tasks;

namespace MyProducts.Models
{
    public interface IArrayCalc
    {
            Task<int[]> Reverse(int[] nums);
            Task<int[]> Delete(int position, int[] nums);
    }
}
