﻿using System.Diagnostics;
using AdventOfCode.Solvers.Year2023.Day9;

namespace AdventOfCode.Solvers.Tests.Year2023.Day9;

public class Day9SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    0 3 6 9 12 15
    1 3 6 10 15 21
    10 13 16 21 30 45
    """;

    public Day9SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 9 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day9SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day9Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("114");
    }

    [Fact(DisplayName ="2023 Day 9 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day9SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day9Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("2");
    }
}
