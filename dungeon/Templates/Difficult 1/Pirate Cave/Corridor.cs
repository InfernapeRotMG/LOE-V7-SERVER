﻿using LoESoft.Dungeon.utils;
using RotMG.Common.Rasterizer;

namespace LoESoft.Dungeon.templates.Difficult_1.Pirate_Cave
{
    internal class Corridor : MapCorridor
    {
        public override void Rasterize(Room src, Room dst, Point srcPos, Point dstPos)
        {
            Default(srcPos, dstPos, new DungeonTile
            {
                TileType = PirateCaveTemplate.BrownLines
            });
        }
    }
}