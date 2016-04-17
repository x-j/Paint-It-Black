using System;

namespace Monte_Carlo_Method_of_Integration {
    class Program {
        static void Main(string[] args) {

            int n; //stopień wielomianu
            int[] wsp; //współczynniki wielomianu
            int k; //początek przedziału całkowania
            int l; //koniec przedziału całkowania
            int t; //liczba rzutow monetą


            
            Console.WriteLine("Podaj stopień wielomianu: ");
            n = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj współczynniki wielomianu (w jednej linii, rozdzielane spacją):");
            string [] temp = (Console.ReadLine().Split(' '));
            wsp = new int[temp.Length];
            for (int i = 0; i < temp.Length; i++) wsp[i] = int.Parse(temp[i]);

            Console.WriteLine("Podaj początek przedziału całkowania: ");
            k = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj koniec przedziału całkowania: ");
            l = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj liczbę rzutów monetą: ");
            t = int.Parse(Console.ReadLine());

            Console.WriteLine(wsp.ToString());



        }
    }
}
