using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaperBack
{
    class Encoding
    {
        List<int> genPolyn;
        public Encoding() { }

        public Encoding(int procent)
        {
            startParam.K = Convert.ToInt32(Math.Truncate(startParam.N / (1 + (2 * ((double)procent / 100)))));
            genPolyn = MyMath.createGenPolynom(startParam.N, startParam.K);
        }

        public List<List<int>> startEncoding(List<int> message)
        {
            List<int> arr = new List<int>();

            List<List<int>> array = new List<List<int>>();
            for (int i = 0; i < message.Count; i++)
            {
                arr.Add(message[i]);

                if (i % startParam.K == startParam.K-1)
                {
                    for (int j = startParam.K; j < startParam.N; j++)
                    {
                        arr.Add(0);
                    }
                    array.Add(arr);
                    arr = new List<int>();
                }

            }

            //for (int i = message.Count; i < startParam.N; i++)
            //{
            //    message.Add(0);
            //}

            for (int i = 0; i < array.Count; i++)
            {
                List<int> ostatok2 = MyMath.DeleniePolynomov(array[i], genPolyn);
                array[i] = MyMath.AdditionPolynom3(array[i], ostatok2);
            }

            return array;

            //List<int> ostatok = MyMath.DeleniePolynomov(message, genPolyn);

            //List<int> messEncod = MyMath.AdditionPolynom3(message, ostatok);

            //return messEncod;
        }
    }
}
