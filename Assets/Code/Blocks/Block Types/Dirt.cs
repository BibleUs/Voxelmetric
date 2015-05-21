﻿using UnityEngine;
using System.Collections;

public class Dirt : BlockSolid {

    public Dirt() : base() { }

    public override void BuildFace(Chunk chunk, BlockPos pos, MeshData meshData, Direction direction)
    {
        BlockBuilder.BuildRenderer(chunk, pos, meshData, direction, this);
        BlockBuilder.BuildTexture(chunk, pos, meshData, direction, this, Config.Textures.Dirt);
        BlockBuilder.BuildColors(chunk, pos, meshData, direction, this);
        if (Config.Toggle.UseCollisionMesh)
        {
            BlockBuilder.BuildCollider(chunk, pos, meshData, direction, this);
        }
    }
}
