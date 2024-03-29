﻿using AdventOfCode.Solvers.Year2023.Day14;

namespace AdventOfCode.Solvers.Tests.Year2023.Day14;

public class Day14SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    O....#....
    O.OO#....#
    .....##...
    OO.#O....O
    .O.....O#.
    O.#..O.#.#
    ..O..#O..O
    .......O..
    #....###..
    #OO..#....
    """;

    public Day14SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 14 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day14SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day14Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("136");
    }

    [Fact(DisplayName ="2023 Day 14 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day14SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day14Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("64");
    }}
