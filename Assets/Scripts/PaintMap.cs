using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
//Textures used
//https://assetstore.unity.com/packages/2d/textures-materials/seamless-textures-realistic-105177
//https://assetstore.unity.com/packages/tools/level-design/enviro-spawn-55594
//https://assetstore.unity.com/packages/3d/vegetation/plants/yughues-free-bushes-13168
//https://assetstore.unity.com/packages/3d/vegetation/trees/realistic-tree-9-rainbow-tree-54622
//https://assetstore.unity.com/packages/3d/characters/easyroads3d-free-v3-987
//https://www.youtube.com/watch?v=kuoTRMQXx6s&feature=youtu.be



public class PaintMap : MonoBehaviour {
    [System.Serializable]
    //Splatmaps Old---------------------------------------
    public class SplatHeightsOld
    {
        public int textureIndex;
        public int startingHeight;
    }
    public SplatHeightsOld[] splatHeightsOld;
    public void CallSplat()
    {
        TerrainData terrainData = Terrain.activeTerrain.terrainData;
        float[,,] splatmapData = new float[terrainData.alphamapWidth,
                                            terrainData.alphamapHeight,
                                            terrainData.alphamapLayers];
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float terrainHeight = terrainData.GetHeight(y, x);

                float[] splat = new float[splatHeightsOld.Length];

                for (int i = 0; i < splatHeightsOld.Length; i++)
                {
                    if (i == splatHeightsOld.Length - 1 && terrainHeight >= splatHeightsOld[i].startingHeight)
                        splat[i] = 1;

                    else if (terrainHeight >= splatHeightsOld[i].startingHeight &&
                        terrainHeight <= splatHeightsOld[i + 1].startingHeight)
                        splat[i] = 1;
                }

                for (int j = 0; j < splatHeightsOld.Length; j++)
                    splatmapData[x, y, j] = splat[j];
            }
            terrainData.SetAlphamaps(0, 0, splatmapData);
        }
    }
    
    // Use this for initialization
    void Start () {
        //CallSplat();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
