using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    static class Program {
        private static void Main(string[] args) {
            var ta = new TesteAsync();

            const int staredThreads = 300;
            
            var listTasks = new Task[staredThreads];
            for (var i = 0; i < staredThreads; i++) {
                listTasks[i] = ta.MakePrintAsync(i+1, 6, 0);
            }
            
            Task.WaitAll(listTasks);

            Console.Out.WriteLine("finish with: " + (ta.TaskCount + staredThreads) + " Tasks Created!");
        }
    }
    class TesteAsync {
        public int TaskCount = new int();
        
        private readonly Random _random;
        private readonly object _lockObj;
        public TesteAsync()
        {
            this._random = new Random();
            this._lockObj = new object();
        }

        public async Task<object> MakePrintAsync(decimal idThread, int times, int lvl) {
            
            var waitTime = this._random.Next(1, 20); lvl += 1;
            
            await Task.Delay((int) (waitTime * (idThread / lvl)));

            var listTasks = new Task<object>[times];
            for (var i = 0; i < times; i++) { 
                listTasks[i] = this.MakePrintAsync(idThread, times - 1, lvl);
                lock (_lockObj) {
                    TaskCount++;    
                }
            }
            
            await Task.WhenAll(listTasks);
            
            await Console.Out.WriteLineAsync($"> thread {idThread:000} run in {waitTime:000}ms at { new string('>', lvl) }");
            
            return 0;
        }
    }

}