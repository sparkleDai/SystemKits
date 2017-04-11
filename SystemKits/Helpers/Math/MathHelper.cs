using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 数学帮助类
    /// </summary>
    public class MathHelper
    {
        #region ComputeMaxCommonDivisor
        /// <summary>
        /// 最大公约数(最大公因数)(采用辗转相除法)
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ComputeMaxCommonDivisor(int m, int n)
        {
            if (m == 0 || n == 0)
            {//0和其它数字没有公约数，记作0.
                return 0;
            }
            int remainder = 0;//余数
            if (m % n == 0)
            {
                return n;
            }
            else
            {
                do
                {
                    remainder = m % n;
                    m = n;
                    n = remainder;
                } while (remainder > 0);
            }
            if (n == 0)
            {
                return m;
            }
            return n;
        }


        /// <summary>
        /// 计算最大公约数
        /// </summary>
        /// <param name="digits">整数数组</param>
        /// <returns></returns>
        public static int ComputeMaxCommonDivisor(params int[] digits)
        {
            if (digits == null || digits.Length <= 0)
            {
                return int.MaxValue;
            }

            if (digits.Length == 1)
            {
                return digits[0];
            }

            int m = digits[0], n = digits[1];
            int divisor = ComputeMaxCommonDivisor(m, n);
            for (int index = 2; index < digits.Length; index++)
            {
                if (divisor==0)
                {
                    break;
                }
                m = divisor;
                n = digits[index];
                divisor = ComputeMaxCommonDivisor(m, n);
            }
            return divisor;
        }

        #endregion

        #region Permutations
        /// <summary>  
        /// 排列循环方法  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long Permutations(int N, int R)
        {
            if (R > N || R <= 0 || N <= 0) throw new ArgumentException("params invalid!");
            long t = 1;
            int i = N;

            while (i != N - R)
            {
                try
                {
                    checked
                    {
                        t *= i;
                    }
                }
                catch
                {
                    throw new OverflowException("overflow happens!");
                }
                --i;
            }
            return t;
        }
        #endregion

        #region Combinations
        /// <summary>  
        /// 组合  
        /// </summary>  
        /// <param name="N"></param>  
        /// <param name="R"></param>  
        /// <returns></returns>  
        public static long Combinations(int N, int R)
        {
            return Permutations(N, R) / Permutations(R, R);
        }
        #endregion
    }
}
