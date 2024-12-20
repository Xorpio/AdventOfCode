﻿using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using AdventOfCode.Lib;
using System.Diagnostics;
using System.Reactive.Threading.Tasks;

namespace PuzzleConsole;

public class StartCommand : Command<StartCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [Description("Jaar van de puzzle")]
        [CommandOption("-y|--year")]
        [DefaultValue("2023")]
        public string Year { get; set; }

        [Description("dag van de puzzle")]
        [CommandOption("-d|--day")]
        [DefaultValue("1")]
        public string Day { get; set; }

        [CommandArgument(0, "<path>")]
        [Description("Path naar input file")]
        public string Filepath { get; set; }

        [CommandOption("--skip-output")]
        public bool skipOutput { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        try
        {

        Stopwatch sw = new Stopwatch();
        sw.Start();
        AnsiConsole.MarkupLine($"[grey]Starting puzzle {settings.Year} day {settings.Day}[/]");
        // var objectToInstantiate = $"PuzzleConsole.Year{settings.Year}.Day{settings.Day}.Day{settings.Day}, PuzzleConsole";

        // AdventOfCode.Solvers.Year2015;
        var objectToInstantiate = $"AdventOfCode.Solvers.Year{settings.Year}.Day{settings.Day}.Day{settings.Day}Solver, AdventOfCode.Solvers";
        // var objectToInstantiate = $"PuzzleConsole.Year{settings.Year}.Day{settings.Day}.Day{settings.Day}, PuzzleConsole";

        var objectType = Type.GetType(objectToInstantiate);

        if (objectType == null)
        {
            AnsiConsole.MarkupLine($"[red]Year({settings.Year}) or Day({settings.Day}) not found[/]");
            return -1;
        }

        BaseSolver instantiatedObject = Activator.CreateInstance(objectType) as BaseSolver;

        if (!File.Exists(settings.Filepath))
        {
            AnsiConsole.MarkupLine($"[red]File not found: {settings.Filepath}[/]");
            return -1;
        }

        var input = File.ReadAllLines(settings.Filepath);

        var a1 = instantiatedObject.Answer1.Select(answer => (Output.Answer1, answer));
        var a2 = instantiatedObject.Answer2.Select(answer => (Output.Answer2, answer));
        var log = instantiatedObject.Logger.Select(message => (Output.Log, message));

        bool part1Done = false;
        bool part2Done = false;

        var x = Observable.Merge(a1, a2, log).Subscribe(tuple =>
        {
            var (output, message) = tuple;
            if (output == Output.Log)
            {
                if (settings.skipOutput)
                {
                    return;
                }
                message = message.Replace("[", "[[");
                message = message.Replace("]", "]]");
                AnsiConsole.MarkupLine($"[grey]{message}[/]");
            }
            else
            {
                part1Done = part1Done || output == Output.Answer1;
                part2Done = part2Done || output == Output.Answer2;
                AnsiConsole.MarkupLine($"[green]{output}(took {sw.Elapsed}):[/]");
                AnsiConsole.MarkupLine($"[green]{message}[/]");

                if(part1Done && part2Done)
                {
                    sw.Stop();
                }
            }
        });

        instantiatedObject.Solve(input);

        var answer1 = instantiatedObject.Answer1.LastAsync().ToTask().Result;
        var answer2 = instantiatedObject.Answer2.LastAsync().ToTask().Result;

        AnsiConsole.MarkupLine($"[green]Answer 1:[/]");
        AnsiConsole.MarkupLine($"[green]{answer1}[/]");
        AnsiConsole.MarkupLine($"[green]Answer 2:[/]");
        AnsiConsole.MarkupLine($"[green]{answer2}[/]");
        AnsiConsole.MarkupLine($"[grey]Done in {sw.Elapsed}[/]");

        return 0;
        }
         catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }

        return -1;
    }
}

public enum Output
{
    Answer1,
    Answer2,
    Log
}
