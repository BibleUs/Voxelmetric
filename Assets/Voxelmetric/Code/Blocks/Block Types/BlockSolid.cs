﻿using UnityEngine;
using System.Collections;

public class BlockSolid : BlockController
{

    public BlockSolid() : base() { }

    public override void AddBlockData(Chunk chunk, BlockPos pos, MeshData meshData)
    {
        PreRender(chunk, pos);

        if (!chunk.GetBlock(pos.Add(0, 1, 0)).controller.IsSolid(Direction.down))
            BuildFace(chunk, pos, meshData, Direction.up);

        if (!chunk.GetBlock(pos.Add(0, -1, 0)).controller.IsSolid(Direction.up))
            BuildFace(chunk, pos, meshData, Direction.down);

        if (!chunk.GetBlock(pos.Add(0, 0, 1)).controller.IsSolid(Direction.south))
            BuildFace(chunk, pos, meshData, Direction.north);

        if (!chunk.GetBlock(pos.Add(0, 0, -1)).controller.IsSolid(Direction.north))
            BuildFace(chunk, pos, meshData, Direction.south);

        if (!chunk.GetBlock(pos.Add(1, 0, 0)).controller.IsSolid(Direction.west))
            BuildFace(chunk, pos, meshData, Direction.east);

        if (!chunk.GetBlock(pos.Add(-1, 0, 0)).controller.IsSolid(Direction.east))
            BuildFace(chunk, pos, meshData, Direction.west);

        PostRender(chunk, pos);
    }

    public override void BuildFace(Chunk chunk, BlockPos pos, MeshData meshData, Direction direction)
    {
        BlockBuilder.BuildRenderer(chunk, pos, meshData, direction, this);
        BlockBuilder.BuildTexture(chunk, pos, meshData, direction, this, Config.Textures.Stone);
        BlockBuilder.BuildColors(chunk, pos, meshData, direction, this);
        if (Config.Toggle.UseCollisionMesh)
        {
            BlockBuilder.BuildCollider(chunk, pos, meshData, direction, this);
        }
    }

    public override bool IsSolid(Direction direction) { return true; }

}