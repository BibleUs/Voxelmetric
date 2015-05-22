﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

[Serializable]
public class Save
{
    public Dictionary<BlockPos, Block> blocks = new Dictionary<BlockPos, Block>();
    public bool changed = false;

    public Save(Chunk chunk)
    {
        //Because existing saved blocks aren't marked as modified we have to add the
        //blocks already in the save fie if there is one. Then add 
        AddSavedBlocks(chunk);

        for (int x = 0; x < Config.Env.ChunkSize; x++)
        {
            for (int y = 0; y < Config.Env.ChunkSize; y++)
            {
                for (int z = 0; z < Config.Env.ChunkSize; z++)
                {
                    BlockPos pos = new BlockPos(x, y, z);
                    if (chunk.GetBlock(pos).modified)
                    {
                        //remove any existing blocks in the dictionary as they're
                        //from the existing save and are overwritten
                        blocks.Remove(pos);
                        blocks.Add(pos, chunk.GetBlock(pos));
                        changed = true;
                    }
                }
            }
        }
    }

    void AddSavedBlocks(Chunk chunk){
        string saveFile = Serialization.SaveLocation(chunk.world.worldName);
        saveFile += Serialization.FileName(chunk.pos);

        if (!File.Exists(saveFile))
            return;

        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFile, FileMode.Open);

        Save save = (Save)formatter.Deserialize(stream);

        foreach (var block in save.blocks)
        {
            blocks.Add(new BlockPos(block.Key.x, block.Key.y, block.Key.z), block.Value);
        }

        stream.Close();
    }
}