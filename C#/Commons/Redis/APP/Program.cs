using System;
using System.Threading.Tasks;

using APP.Examples;

namespace APP {

    internal static class Program {

        // nedds up the redis server on docker to run
        // sudo sudo docker run --name redis-docker -p 127.0.0.1:6379:6379 -d redis
        private static async Task Main(string[] args) {
            var simpleStringExample = new SimpleSring();

            Console.WriteLine("Simple Strings with JSON!");

            await simpleStringExample.Start(args);

        }

    }

}