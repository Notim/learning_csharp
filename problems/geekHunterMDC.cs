using System;

public class Test
{
    public static void Main() {
        int index = int.Parse(Console.ReadLine());
        // Console.WriteLine("index = " + index);
        
        for (int i = index ; i != 0; i--){
            string line;
            while ((line = Console.ReadLine()) != null) {
                
                string[] numbers = line.Split(' ');
                
                int fir = int.Parse(numbers[0]) >= int.Parse(numbers[1])
                    ? int.Parse(numbers[0])
                    : int.Parse(numbers[1]);
                
                int sec = int.Parse(numbers[0]) < int.Parse(numbers[1])
                    ? int.Parse(numbers[0])
                    : int.Parse(numbers[1]);
                
            
                if(sec == 0) {
                    Console.WriteLine(fir);
                } else {
                    int rest = fir % sec , div = sec;
                    
                    while (rest > 0) {
                        div = rest ;
                        rest = fir % div;
                        fir = div;
                        
                    }
                    Console.WriteLine(div);
                }
            }    
        }
        
    }
}

