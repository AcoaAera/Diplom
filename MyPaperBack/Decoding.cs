using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaperBack
{
    class Decoding
    {
        List<int> genPolyn;

        public static int k = 255;
        public static int t = (startParam.N - k) / 2;

        public Decoding() { }

        public Decoding(int procent)
        {
            k = Convert.ToInt32(Math.Truncate(startParam.N / (1 + (2 * ((double)procent / 100)))));
            genPolyn = MyMath.createGenPolynom(startParam.N, k);
        }

        public List<int> startDecoding(List<int> message)
        {
            List<int> ErrorPolynom = MyMath.DeleniePolynomov(message, genPolyn);
            if (ErrorPolynom.Count == 0)
            {
                message.RemoveRange(k, startParam.N);
                return message;
            }

            List<int> SyndromErrorPolynom = MyMath.CalcSyndromErrorPolynom(ErrorPolynom); //S(x)

            int[,] Matrix = MyMath.CreateMatrix(SyndromErrorPolynom);

            int[] Vektor = MyMath.CreateVektor(SyndromErrorPolynom); //V(x)

            int[,] InverseMatrix = MyMath.CreateInverseMatrix(Matrix); //M^(-1)

            int[] NewMatrix = MyMath.MultMatrix(InverseMatrix, Vektor); //M^(-1) * V(x)

            List<int> PolynomLokatorov = MyMath.CreatePolynomLokatorov(NewMatrix); //L(x)

            List<int> PolynomErrors = MyMath.CreatePolynomErrors(PolynomLokatorov, SyndromErrorPolynom); //W(x) = L(x) * S(x)

            PolynomErrors.RemoveRange(0, PolynomErrors.Count - (startParam.N - k)); // Коэффициенты, которые старше N-K, обнуляются
            MyMath.Del0(PolynomErrors);

            List<int> DifPolynomLokatorov = MyMath.DifPolynom(PolynomLokatorov); //L'(x)

            List<int> LokatorKorni = MyMath.SearchLokatorKorneiErrors(PolynomLokatorov); //Здесь отмечены единицей корни. Далее расчет Yi

            List<int> Korni = MyMath.SearchMnogiteleiErrors(LokatorKorni, PolynomErrors, DifPolynomLokatorov);

            List<int> messageTRUE = MyMath.AdditionPolynom3(Korni, message);

            messageTRUE.RemoveRange(k, startParam.N);

            return messageTRUE;
        }

    }
}
