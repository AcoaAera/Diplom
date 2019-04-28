using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaperBack
{
    public static class MyMath
    {
        /// <summary>
        /// Генерация порождающего полинома
        /// </summary>
        /// <param name="N">Количество всех символов в строке</param>
        /// <param name="k">Количество полезных символов</param>
        /// <returns></returns>
        public static List<int> createGenPolynom(int N, int k)
        {

            List<int> a = new List<int>();// { 1, 13, 12, 8, 7 };
            List<int> b = new List<int>();// { 1, 10, 14 };

            List<int> z = new List<int>();

            int e = 1;
            a.Clear();
            a.Add(1);
            a.Add(fieldGalua.Table0[1]);

            while (e < (N - k))
            {
                e++;

                b.Clear();
                b.Add(1);
                b.Add(fieldGalua.Table0[e]);

                z.Clear();
                for (int i = 0; i < a.Count + b.Count - 1; i++)
                    z.Add(0);

                for (int i = 0; i < a.Count; i++)
                    for (int j = 0; j < b.Count; j++)
                    {
                        int q = MulGalua(a[i], b[j]);
                        z[i + j] ^= q;
                    }

                a.Clear();
                for (int i = 0; i < z.Count; i++)
                    a.Add(z[i]);
            }

            return a;
        }

        /// <summary>
        /// Сложение остатка от деления полиномов и входного сообщения
        /// </summary>
        /// <param name="message">Входное сообщение</param>
        /// <param name="ostatok">Остаток от деления</param>
        /// <returns></returns>
        //static List<int> Slogenie(List<int> message, List<int> ostatok)
        //{
        //    for (int i = 1; i <= ostatok.Count; i++)
        //        message[message.Count - i] = ostatok[ostatok.Count - i];
        //    return message;
        //}

        /// <summary>
        /// Удаление нулей в начале полинома
        /// </summary>
        /// <param name="list">Полином</param>
        public static void Del0(List<int> list)
        {
            while (list[0] == 0)
            {
                list.RemoveAt(0);
                Del0(list);
            }
        }

        /// <summary>
        /// Сложение полиномов по правилам поля Галуа (XOR)
        /// </summary>
        /// <param name="polyn1">Первый полином</param>
        /// <param name="polyn2">Второй полином</param>
        /// <returns></returns>
        //private static List<int> Addition(List<int> polyn1, List<int> polyn2) //В данном случае polyn1 = message, //polyn2 = полином полученный умножением множителя на делитель
        //{
        //    List<int> q = new List<int>();
        //    for (int i = 0; i < polyn1.Count; i++)
        //        q.Add(polyn1[i] ^ polyn2[i]);     //сложение по XOR
        //    Del0(q);                              //Удаление нулей в начале списка, при сложении двух полиномов
        //    return q;
        //}

        /// <summary>
        /// Умножение двух чисел в поле Галуа
        /// </summary>
        /// <param name="a">Первое слагаемое</param>
        /// <param name="b">Второе слагаемое</param>
        /// <returns></returns>
        public static int MulGalua(int a, int b)
        {
            int _a = 0;
            int _b = 0;

            int _z = 0;

            for (int i = 0; i < fieldGalua.Table0.Length; i++)
            {
                if (fieldGalua.Table0[i] == a)
                { _a = i; break; }
            }
            for (int i = 0; i < fieldGalua.Table0.Length; i++)
            {
                if (fieldGalua.Table0[i] == b)
                { _b = i; break; }
            }

            int c = (_a + _b) % 255; ///НАДО МЕНЯТЬ ЧИСЛО

            _z = fieldGalua.Table0[c];
            return _z;
        }

        /// <summary>
        /// Сложение полиномов по правилам поля Галуа (XOR)
        /// </summary>
        /// <param name="polyn1">Первый полином</param>
        /// <param name="polyn2">Второй полином</param>
        /// <returns></returns>
        public static List<int> AdditionPolynom3(List<int> polyn1, List<int> polyn2)
        {
            //int min = polyn1.Count <= polyn2.Count ? polyn1.Count : polyn2.Count;
            //int max = polyn1.Count >= polyn2.Count ? polyn1.Count : polyn2.Count;

            List<int> q = new List<int>();

            if (polyn1.Count >= polyn2.Count)
            {
                for (int i = 0; i < polyn1.Count; i++)
                    q.Add(polyn1[i]);

                int min = polyn2.Count;
                for (int i = 0; i < min; i++)
                    q[polyn1.Count - 1 - i] ^= polyn2[min - 1 - i];

            }
            else
            {
                for (int i = 0; i < polyn2.Count; i++)
                    q.Add(polyn2[i]);

                int min = polyn1.Count;
                for (int i = 0; i < min; i++)
                    q[polyn2.Count - 1 - i] ^= polyn1[min - 1 - i];
            }

            Del0(q);

            return q;
        }

        /// <summary>
        /// Деление полиномов и получение остатка от деления
        /// </summary>
        /// <param name="dividend">Делимое</param>
        /// <param name="divider">Делитель</param>
        /// <returns></returns>
        public static List<int> DeleniePolynomov(List<int> dividend, List<int> divider) //умножение делителя на первое число делимого
        {
            List<int> mess = new List<int>(); //остаток от деления
            for (int i = 0; i < dividend.Count; i++)
                mess.Add(dividend[i]);

            while (mess.Count >= divider.Count)  //Пока степерь делителя не превысит степень делимого
            {
                List<int> arrDop = new List<int>(); //Полином полученный как производение делителя на первое слагаемое делимого  
                int mnogitel = mess[0];          //Берем число из делимого

                for (int i = 0; i < mess.Count; i++)
                    if (i >= divider.Count)
                        arrDop.Add(0);
                    else
                        arrDop.Add(MulGalua(mnogitel, divider[i]));

                //message = Addition(message, arrDop);
                mess = AdditionPolynom3(mess, arrDop);

            }
            return mess;
        }

        public static List<int> ConvertToList(byte[] arr)
        {
            List<int> mess = new List<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                //mess.Add(Convert.ToInt32(arr[i].ToString()));
                mess.Add(arr[i]);
            }

            return mess;
        }

        /// <summary>
        /// Расчет многочлена синдрома ошибки
        /// </summary>
        /// <param name="ErrorPolynom">Полином ошибки</param>
        /// <returns></returns>
        public static List<int> CalcSyndromErrorPolynom(List<int> ErrorPolynom)
        {
            ErrorPolynom.Reverse();
            List<int> q = new List<int>();
            for (int i = 0; i < ErrorPolynom.Count; i++)
            {
                q.Add(0);
                for (int j = 0; j < ErrorPolynom.Count; j++)
                {
                    int z = ((i + 1) * j) % 15;
                    if (ErrorPolynom[j] == 0) continue;
                    int x = MulGalua(ErrorPolynom[j], fieldGalua.Table0[z]);
                    q[i] = q[i] ^ x;
                }
            }

            return q;
        }

        public static List<int> DifPolynom(List<int> polynomLokatorov)
        {
            List<int> help = new List<int>();

            for (int i = 0; i < polynomLokatorov.Count; i++)
                help.Add(polynomLokatorov[i]);

            int count = help.Count - 1;

            for (int i = 0; i < help.Count; i++)
            {
                if (count % 2 == 0)
                    help[i] = 0;
                count--;
            }

            help.RemoveAt(help.Count - 1);
            return help;
        }

        public static List<int> CreatePolynomErrors(List<int> pL, List<int> sEP)
        {
            Console.WriteLine();
            sEP.Reverse();

            int z = pL.Count + sEP.Count - 1;
            List<int> pE = new List<int>(z); //pE = polynomErrors
            for (int i = 0; i < z; i++)
                pE.Add(0);

            for (int i = 0; i < pL.Count; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < sEP.Count; j++)
                {
                    if (pL[i] == 0 || sEP[j] == 0)
                        continue;
                    Console.Write(pL[i] + "x^(" + (pL.Count - i - 1) + ") * " + sEP[j] + "x^(" + (sEP.Count - j - 1) + ") + ");
                    int q = MulGalua(pL[i], sEP[j]);
                    pE[i + j] ^= q;
                }
            }
            return pE;
        }

        public static List<int> CreatePolynomLokatorov(int[] newMatrix)
        {
            List<int> PolynomLokatorov = new List<int>();
            for (int i = newMatrix.Length - 1; i >= 0; i--)
                PolynomLokatorov.Add(newMatrix[i]);
            PolynomLokatorov.Add(1);

            return PolynomLokatorov;
        }

        public static int[] MultMatrix(int[,] inverseMatrix, int[] vektor)
        {
            int l = startParam.T;

            int n = startParam.T;

            int m = startParam.T;

            int[] newMatrix = new int[startParam.T];

            for (int i = 0; i < l; i++)
                for (int j = 0; j < n; j++)
                {
                    int q = inverseMatrix[i, j];
                    int w = vektor[j];
                    if (q == 0 || w == 0) continue;
                    newMatrix[i] ^= MulGalua(q, w);
                }

            return newMatrix;
        }

        public static int[,] CreateInverseMatrix(int[,] matrix)
        {

            int[,] _returnMatrix = new int[startParam.T, startParam.T];

            int[,] IM = new int[startParam.T, 2 * startParam.T]; //{
                                           //{15, 15, 0,  1, 0, 0 },
                                           //{15, 15,  15, 0, 1, 0},
                                           //{3,  2,  15, 0, 0, 1 } };

            //Запись матрицы в новую матрицу
            for (int i = 0; i < startParam.T; i++)
                for (int j = 0; j < startParam.T; j++)
                    IM[i, j] = matrix[i, j];

            //Написание единичной матрицы в правой части
            for (int i = 0; i < startParam.T; i++)
                IM[i, i + startParam.T] = 1;

            //проверка на вырожденность матрицы
            for (int i = 0; i < startParam.T; i++)
            {
                int count = 0;
                for (int j = 0; j < startParam.T; j++)
                    if (IM[j, i] == 0)
                        count++;
                if (count == startParam.T)
                {
                    Console.WriteLine("Матрица вырожденная");
                    break;
                }
            }

            //Нули под диагональю
            for (int z = 0; z < startParam.T - 1; z++)
                for (int i = 0; i < startParam.T - 1 - z; i++)
                {
                    int count = 1;
                    while (IM[z, z] == 0)
                    {
                        int[] C = new int[6];
                        for (int k = 0; k < 6; k++)
                        {
                            C[k] = IM[i + count, k];
                            IM[i + count, k] = IM[i + 1 + count, k];
                            IM[i + 1 + count, k] = C[k];
                        }
                        count++;
                    }

                    int K = DivGalua(IM[i + z + 1, z], IM[z, z]);
                    //Console.WriteLine("Делимое: " + IM[i + z + 1, z]);
                    for (int j = 0; j < startParam.T * 2; j++)
                    {
                        if (IM[z, j] == 0)
                            continue;
                        IM[i + z + 1, j] ^= MulGalua(IM[z, j], K);
                    }
                }

            for (int z = 0; z < startParam.T - 1; z++)
                for (int i = 0; i < startParam.T - 1 - z; i++)
                {
                    if (IM[startParam.T - 1 - z - 1 - i, startParam.T - 1 - z] == 0)
                        continue;
                    int K = DivGalua(IM[startParam.T - 1 - z - 1 - i, startParam.T - 1 - z], IM[startParam.T - 1 - z, startParam.T - 1 - z]);
                    for (int j = 0; j < startParam.T * 2; j++)
                    {
                        if (IM[startParam.T - 1 - z, j] == 0)
                            continue;
                        IM[startParam.T - 1 - 1 - i - z, j] ^= MulGalua(IM[startParam.T - 1 - z, j], K);
                    }
                }

            for (int i = 0; i < startParam.T; i++)
            {
                for (int j = 0; j < startParam.T * 2; j++)
                    if (i != j && IM[i, j] != 0)
                        IM[i, j] = DivGalua(IM[i, j], IM[i, i]);
                IM[i, i] = 1;
            }

            for (int i = 0; i < startParam.T; i++)
                for (int j = 0; j < startParam.T; j++)
                    _returnMatrix[i, j] = IM[i, j + startParam.T];

            return _returnMatrix;
        }

        /// <summary>
        /// Построение вектора V
        /// </summary>
        /// <param name="syndromErrorPolynom">Полином синдрома ошибки</param>
        /// <returns></returns>
        public static int[] CreateVektor(List<int> syndromErrorPolynom)
        {
            int[] Vektor = new int[startParam.T];

            for (int i = 0; i < startParam.T; i++)
                Vektor[i] = syndromErrorPolynom[startParam.T + i];

            return Vektor;
        }

        /// <summary>
        /// Построение матрицы М
        /// </summary>
        /// <param name="syndromErrorPolynom">Полином синдрома ошибки</param>
        /// <returns></returns>
        public static int[,] CreateMatrix(List<int> syndromErrorPolynom)
        {
            int[,] Matrix = new int[startParam.T, startParam.T];

            for (int i = 0; i < startParam.T; i++)
                for (int j = 0; j < startParam.T; j++)
                    Matrix[i, j] = syndromErrorPolynom[startParam.T - 1 + i - j];

            return Matrix;
        }

        public static List<int> SearchMnogiteleiErrors(List<int> lokatorKorni, List<int> PolynomErrors, List<int> DifPolynomLokatorov)
        {
            List<int> korni = new List<int>();
            for (int i = 0; i < lokatorKorni.Count; i++)
            {
                if (lokatorKorni[i] == 1)
                {
                    int delimoe = ValueFunction(PolynomErrors, i);
                    int delitel = ValueFunction(DifPolynomLokatorov, i);
                    korni.Add(DivGalua(delimoe, delitel));
                }
                else
                {
                    korni.Add(0);
                }
            }

            return korni;
        }

        public static List<int> SearchLokatorKorneiErrors(List<int> polynomLokatorov)
        {
            List<int> korni = new List<int>();

            for (int i = 0; i < fieldGalua.Table0.Length; i++)
            {
                int q = ValueFunction(polynomLokatorov, i);
                if (q == 0)
                {
                    korni.Add(1);
                    continue;
                }
                korni.Add(0);
            }

            return korni;
        }

        /// <summary>
        /// Расчет значения функции с одной переменной
        /// </summary>
        /// <param name="polynomLokatorov">Функция</param>
        /// <param name="z">Значение неизвестной</param>
        /// <returns></returns>
        private static int ValueFunction(List<int> polynomLokatorov, int z)
        {
            int q = 0;
            for (int i = 0; i < polynomLokatorov.Count; i++)
            {
                if (polynomLokatorov[i] == 0)
                    continue;
                q ^= MulGalua(polynomLokatorov[i], StepenGalua(z, (polynomLokatorov.Count - i - 1)));
            }

            return q;
        }

        /// <summary>
        /// Деление двух чисел в поле Галуа
        /// </summary>
        /// <param name="a">Делимое</param>
        /// <param name="b">Делитель</param>
        /// <returns></returns>
        static int DivGalua(int a, int b)
        {
            int _a = 0;
            int _b = 0;

            for (int i = 0; i < fieldGalua.Table0.Length; i++)
            {
                if (a == fieldGalua.Table0[i])
                    _a = i;
                if (b == fieldGalua.Table0[i])
                    _b = i;
            }

            int del = _a - _b;
            if (del < 0) del += 15;

            return fieldGalua.Table0[del];
        }

        /// <summary>
        /// Расчет степени числа в поле Галуа
        /// </summary>
        /// <param name="z">Индекс числа в поле Галуа</param>
        /// <param name="x">Степень</param>
        /// <returns></returns>
        private static int StepenGalua(int z, int x)
        {
            int q = 0;
            for (int i = 0; i < x; i++)
                q = MulGalua(q, fieldGalua.Table0[z]);

            return q;
        }


    }
}
