﻿using AdventOfCode.Solvers.Year2023.Day21;

namespace AdventOfCode.Solvers.Tests.Year2023.Day21;

public class Day21SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    ...........
    .....###.#.
    .###.##..#.
    ..#.#...#..
    ....#.#....
    .##..S####.
    .##..#...#.
    .......##..
    .##.#.####.
    .##..##.##.
    ...........
    """;

    public Day21SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 21 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day21SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day21Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("16");
    }

    [Fact(DisplayName ="2023 Day 21 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day21SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day21Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("16733044");
    }}
