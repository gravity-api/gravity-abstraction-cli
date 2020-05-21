# Gravity Abstraction Cli
CLI (command line integration) parser, for reading Gravity formatted text commands.

# Quick Start
Install NuGet package ```Gravity.Abstraction.Cli```  

```csharp
using Gravity.Abstraction.Cli;
using System;

namespace CliFactorySample
{
    internal static class Program
    {
        private static void Main()
        {
            // some command line
            const string cli = "{{$ --arg1:some text --arg2:1 --arg3:0.5 --arg4}}";

            // option #1: extract all arguments
            var arguments1 = new CliFactory(cli).Parse();

            // option #2: extract all arguments
            var arguments2 = new CliFactory().Parse(cli);

            // output
            foreach (var argument in arguments1)
            {
                var value = string.IsNullOrEmpty(argument.Value) ? "true" : argument.Value;
                Console.WriteLine($"MANE {argument.Key}; VALUE: {value}");
            }

            Console.WriteLine();

            foreach (var argument in arguments2)
            {
                var value = string.IsNullOrEmpty(argument.Value) ? "true" : argument.Value;
                Console.WriteLine($"MANE {argument.Key}; VALUE: {value}");
            }

            // wait for user input
            Console.ReadLine();
        }
    }
/*
}
----------
- OUTPUT -
----------
MANE arg1; VALUE: some text
MANE arg2; VALUE: 1
MANE arg3; VALUE: 0.5
MANE arg4; VALUE: true

MANE arg1; VALUE: some text
MANE arg2; VALUE: 1
MANE arg3; VALUE: 0.5
MANE arg4; VALUE: true
*/
```