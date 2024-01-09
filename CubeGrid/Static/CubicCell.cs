using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGrid.Static
{
    public readonly record struct CubicCell(int X, int Y, int Z)
    {
        public IEnumerable<CubicCell> EnumerateNextCells()
        {
            yield return this with { X = this.X - 1 };
            yield return this with { X = this.X + 1 };
            yield return this with { Y = this.Y - 1 };
            yield return this with { Y = this.Y + 1 };
            yield return this with { Z = this.Z + 1 };
            yield return this with { Z = this.Z + 1 };
        }

        public static CubicCell? ParseOrNull(string? s)
        {
            var split = (s ?? "").Split(',');
            if (split.Length == 3 && int.TryParse(split[0], out int x) && int.TryParse(split[1], out int y) && int.TryParse(split[2], out int z))
            {
                return new(x, y, z);
            }
            else
            {
                return null;
            }
        }
    }
}
