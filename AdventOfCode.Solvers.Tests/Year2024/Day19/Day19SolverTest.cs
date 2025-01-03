﻿using AdventOfCode.Solvers.Year2024.Day19;

namespace AdventOfCode.Solvers.Tests.Year2024.Day19;

public class Day19SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    r, wr, b, g, bwu, rb, gb, br

    brwrr
    bggr
    gbbr
    rrbgbr
    ubwu
    bwurrg
    brgr
    bbrgwb
    """;

    public Day19SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 19 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day19SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day19Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("6");
    }

    [Fact(DisplayName = "2024 Day 19 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day19SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day19Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("16");
    }
}
