using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solvers.Year2023.Day21;

public class Day21Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var evenPoints = 0;
        var oddPoints = 0;
        Point start = new Point(0, 0);
        char[,] grid = new char[puzzle.Length, puzzle[0].Length];
        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[0].Length; col++)
            {
                grid[row, col] = puzzle[row][col];
                if (puzzle[row][col] == 'S')
                {
                    start = new Point(row, col);
                    if ((row + col) % 2 == 0)
                    {
                        logger.OnNext("Start is even");
                    }
                    else{
                        logger.OnNext("Start is odd");}
                }

                if ((row + col) % 2 == 0 && puzzle[row][col] != '#')
                {
                    evenPoints++;
                }
                else
                {
                    oddPoints++;
                }
            }
        }

        logger.OnNext($"even: {evenPoints} odd: {oddPoints}");

        int maxRow = grid.GetLength(0);
        int maxCol = grid.GetLength(1);

        Queue<(Point point, Point dimension)> queue = new();

        queue.Enqueue((start, new Point(0, 0)));

        double steps = puzzle.Length > 20 ? 64 : 6;

        bool even = false;

        Dictionary<(Point point, Point dimension), bool> visited = new();
        Dictionary<(Point point, Point dimension), bool> pvisited = new();

        Dictionary<Point, List<(string key, int step)>> p = [];
        if (puzzle.Length > 20)
        {
            p.Add(new Point(-2, -2), new ());
            p.Add(new Point(-2, 0), new ());
            p.Add(new Point(-2, 2), new ());
            p.Add(new Point(0, -2), new ());
            p.Add(new Point(0, 0), new ());
            p.Add(new Point(0, 2), new ());
            p.Add(new Point(2, -2), new ());
            p.Add(new Point(2, 0), new ());
            p.Add(new Point(2, 2), new ());
        }
        else
        {
            p.Add(new Point(-4, -4), new ());
            p.Add(new Point(-4, 0), new ());
            p.Add(new Point(-4, 4), new ());
            p.Add(new Point(0, -4), new ());
            p.Add(new Point(0, 0), new ());
            p.Add(new Point(0, 4), new ());
            p.Add(new Point(4, -4), new ());
            p.Add(new Point(4, 0), new ());
            p.Add(new Point(4, 4), new ());
        }

        var seenDimensions = new Dictionary<Point, int>();
        // var NorthHist = new List<string>();
        // var NorthHist2 = new List<string>();

        var hist = new Dictionary<Point, char[,]>();
        bool done = false;
        int i = 0;
        while (!done)
        {
            var newQueue = new Queue<(Point point, Point dimension)>();
            even = !even;
            while(queue.Any())
            {
                var next = queue.Dequeue();

                var (row, col) = next.point;

                if (visited.ContainsKey(next))
                {
                    continue;
                }

                if (!seenDimensions.ContainsKey(next.dimension))
                {
                    seenDimensions.Add(next.dimension, i);
                    // logger.OnNext($"New dimension: {next.dimension} - Point: {next.point} - {i}");
                }

                if (row > 0 && grid[row - 1, col] != '#')
                {
                    newQueue.Enqueue((new Point(row - 1, col), next.dimension));
                }
                if (row < maxRow - 1 && grid[row + 1, col] != '#')
                {
                    newQueue.Enqueue((new Point(row + 1, col), next.dimension));
                }
                if (col > 0 && grid[row, col - 1] != '#')
                {
                    newQueue.Enqueue((new Point(row, col -1), next.dimension));
                }
                if (col < maxCol - 1 && grid[row, col + 1] != '#')
                {
                    newQueue.Enqueue((new Point(row, col + 1), next.dimension));
                }

                if (row == 0)
                {
                    newQueue.Enqueue((new Point(maxRow - 1, col), next.dimension with { row = next.dimension.row - 1 }));
                }
                if (row == maxRow - 1)
                {
                    newQueue.Enqueue((new Point(0, col), next.dimension with { row = next.dimension.row + 1 }));
                }
                if (col == 0)
                {
                    newQueue.Enqueue((new Point(row, maxCol -1), next.dimension with { col = next.dimension.col - 1 }));
                }
                if (col == maxCol - 1)
                {
                    newQueue.Enqueue((new Point(row, 0), next.dimension with { col = next.dimension.col + 1 }));
                }

                visited[next] = even;
                if (p.ContainsKey(next.dimension))
                {
                    pvisited[next] = even;
                }
            }

            if (i == steps)
            {
                GiveAnswer1(visited.Count(s => s.Value == even && s.Key.dimension == new Point(0, 0)));
            }

            if (p.All(i => i.Value.Count > 1 && i.Value[^1].key == p[new Point(0,0)][^1].key)) 
            {
                logger.OnNext($"Step {i} - {queue.Count} - {p[new Point(0,0)].Count()}");
                done = true;
            }   

            foreach(var d in pvisited)
            {
                if (!hist.ContainsKey(d.Key.dimension))
                {
                    hist[d.Key.dimension] = new char[maxRow + 1, maxCol + 1];
                    //add rocks
                    for(int r = 0; r < maxRow; r++)
                    {
                        for(int c = 0; c < maxCol; c++)
                        {
                            hist[d.Key.dimension][r, c] = grid[r, c];
                        }
                    }
                }

                hist[d.Key.dimension][d.Key.point.row, d.Key.point.col] = 'O';
            }
            foreach(var h in hist)
            {
                var k = "";
                for(int r = 0; r < maxRow; r++)
                {
                    for(int c = 0; c < maxCol; c++)
                    {
                        k += h.Value[r, c];
                    }
                }

                if (p.ContainsKey(h.Key) && !p[h.Key].Any(s => s.key == k))
                {
                    p[h.Key].Add((k, i));
                }
            }

            // if (hist.ContainsKey(North with {row = North.row - 1}))
            // {
            //     throw new Exception("North is not north on step " + i);
            // }

            var lines = new List<string>();
            if (visited.Any(s => s.Key.dimension == new Point(0,-4)))
            {
                lines.Add("step: " + i);
                for(int r = 0; r < maxRow; r++)
                {
                    var line = "";
                    for(int c = 0; c < maxCol; c++)
                    {
                        if (visited.ContainsKey((new Point(r, c), new Point(0,-4))))
                        {
                            line += "O";
                        }
                        else
                        {
                            line += grid[r, c];
                        }
                    }

                    lines.Add(line);
                }

                if (lines.All(l => !l.Contains(".")))
                {
                    lines.Clear();
                }
                else{
                    foreach(var l in lines)
                    {
                        logger.OnNext(l);
                    }
                }
                logger.OnNext("");
            }


            // if (i == 100)
            // {
            //     break;
            // }

            queue = new Queue<(Point point, Point dimension)>(newQueue.Distinct());

            // logger.OnNext($"Step {i} - {queue.Count}");

            i++;
        }

        var mod = seenDimensions[new Point(3,0)] - seenDimensions[new Point(2,0)];

        steps = steps == 6 ? 100 : 26501365;

        // maxRow = seenDimensions.Select(d => d.Key.row).Max() + 2;
        // maxCol = seenDimensions.Select(d => d.Key.col).Max() + 2;
        // var minRow = seenDimensions.Select(d => d.Key.row).Min() -1;
        // var minCol = seenDimensions.Select(d => d.Key.col).Min() -1;

        foreach (var pp in p)
        {
            logger.OnNext($"{pp.Key} - {pp.Value.Count}");
            foreach(var ppp in pp.Value)
            {
                logger.OnNext($"{ppp.key[0..10]} - {ppp.step} - {ppp.key.Where(c => c == 'O').Count()}");
            }
            logger.OnNext("");
        }

        // for(int r = minRow; r < maxRow; r++)
        // {
        //     var line = "";
        //     for(int c = minCol; c < maxCol; c++)
        //     {
        //         if (seenDimensions.ContainsKey(new Point(r, c)))
        //         {
        //             line += seenDimensions[new Point(r, c)].ToString().PadLeft(4);
        //         }
        //         else
        //         {
        //             line += "--- ";
        //         }
        //     }

        //     logger.OnNext(line);
        // }


        // foreach(var a in analysis)
        // {
        //     var values = string.Join(',', a.Value.Select(v => $"({v.row},{v.col})"));
        //     if (!a.Key.Contains("."))
        //     {
        //         if (a.Value[0].row == 0 && a.Value[0].col > 0 && a.Value.Any(v => v.row != 0 || v.col <= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].row == 0 && a.Value[0].col < 0 && a.Value.Any(v => v.row != 0 || v.col >= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].col == 0 && a.Value[0].row > 0 && a.Value.Any(v => v.col != 0 || v.row <= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].col == 0 && a.Value[0].row < 0 && a.Value.Any(v => v.col != 0 || v.row >= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].row > 0 && a.Value[0].col > 0 && a.Value.Any(v => v.row <= 0 || v.col <= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].row > 0 && a.Value[0].col < 0 && a.Value.Any(v => v.row <= 0 || v.col >= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].row < 0 && a.Value[0].col > 0 && a.Value.Any(v => v.row >= 0 || v.col <= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }
        //         if (a.Value[0].row < 0 && a.Value[0].col < 0 && a.Value.Any(v => v.row >= 0 || v.col >= 0))
        //         {
        //             throw new Exception($"{a.Key} - {values}");
        //         }

        //     }
        // }


        GiveAnswer2("");
    }
}

public record Point(int row, int col);
