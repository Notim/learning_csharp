using System;

namespace uri_1151 {
    class Program {
        static void Main(string[] args)
        {

            var inVal = Console.ReadLine();
            
            int index = Convert.ToInt32(inVal);

            var sp1 = 0;
            var sp2 = 1;
            var aux = 0;
            
            for (int i = 0; i != index; i++) {
                Console.Write(sp1);
                if(i != index -1 ) Console.Write( " ");
                    
                aux = sp1;
                sp1 += sp2;
                sp2 = aux;
            }
            Console.Write("\n");
        }
    }
}