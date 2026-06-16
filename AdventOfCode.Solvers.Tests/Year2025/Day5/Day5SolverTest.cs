using AdventOfCode.Solvers.Year2025.Day5;

namespace AdventOfCode.Solvers.Tests.Year2025.Day5;

public class Day5SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    sampleinput
    """;

    public Day5SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2025 Day 5 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day5SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day5Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("?");
    }

    [Fact(DisplayName = "2025 Day 5 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day5SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day5Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }
}
