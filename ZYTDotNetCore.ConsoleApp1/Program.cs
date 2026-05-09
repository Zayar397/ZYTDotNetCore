// See https://aka.ms/new-console-template for more information
using ZYTDotNetCore.ConsoleApp1;

Console.WriteLine("Hello, World!");

HTTP_CLIENT_EXAMPLE httpClient = new HTTP_CLIENT_EXAMPLE();
//await httpClient.Read();
//await httpClient.Edit(1);
//await httpClient.Create(101,"Testing...","This is testing for insert.");
await httpClient.Update(200,1,"Testing...","This is testing for update.");
