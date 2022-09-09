// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Task[] task = new Task[] { Task.Run( () => { Task.Delay(1000); Console.WriteLine("here1"); }) };

Task.WaitAll ( task);
   
Console.WriteLine("here2");

Console.Read();