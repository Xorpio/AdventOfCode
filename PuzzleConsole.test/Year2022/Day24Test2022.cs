﻿using PuzzleConsole.Year2022.Day24;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "24")]
public partial class Day24Test2022 {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day24();

        var sampleInput = """
        #.######
        #>>.<^<#
        #.<..<<#
        #>v.><>#
        #<^v^^>#
        ######.#
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test part 1", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(18);
        });

        scenario.Fact("Small sample test part 2", () =>
        {
            int.Parse(sut.Solve(lines)[1]).Should().Be(54);
        });
    }
}