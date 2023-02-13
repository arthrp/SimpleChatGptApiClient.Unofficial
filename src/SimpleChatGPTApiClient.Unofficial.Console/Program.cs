namespace SimpleChatGPTApiClient.Unofficial.Console;

using System;

class Program
{
    static void Main(string[] args)
    {
        var response = new ChatGptClient(args[0]).GetResponse("Hello!").Result;
        Console.WriteLine(response);
    }
}
