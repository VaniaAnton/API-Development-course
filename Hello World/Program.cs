using System;


namespace HelloWorld
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            Task firstTask = new Task(() =>
            {
                Thread.Sleep(100);
                System.Console.WriteLine("Task 1");
            });
            firstTask.Start();
            Task secondTask = ConsoleAfterDelayAsync("Task 2", 150);

            ConsoleAfterDelay("Delay", 101);
            Task ThirdTask = ConsoleAfterDelayAsync("Task 3", 50);

            await firstTask;
            System.Console.WriteLine("After the task was created");

        }

        static void ConsoleAfterDelay(string text, int delayTime)
        {
            Thread.Sleep(delayTime);
            System.Console.WriteLine(text);
        }
        static async Task ConsoleAfterDelayAsync(string text, int delayTime)
        {
            await Task.Delay(delayTime);
            System.Console.WriteLine(text);
        }
    }
}