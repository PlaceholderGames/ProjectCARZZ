using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

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


    //==================WIP=====================================================================================
    ////SPLATMAPS ------------------------------------------
    //public class SplatHeights
    //{
    //    public Texture2D texture = null;
    //    public float minHeight = 0.1f;
    //    public float maxHeight = 0.2f;
    //    public bool remove = false;
    //}

    //public List<SplatHeights> splatHeights = new List<SplatHeights>()
    //{
    //    new SplatHeights()
    //};

    //public void AddNewSplatHeight()
    //{
    //    splatHeights.Add(new SplatHeights());
    //}
    //public void RemoveSplatHeight()
    //{
    //    List<SplatHeights> keptSplatHeights = new List<SplatHeights>();
    //    for(int i = 0; i < splatHeights.Count; i++)
    //    {
    //        if (!splatHeights[i].remove)
    //        {
    //            keptSplatHeights.Add(splatHeights[i]);
    //        }
    //    }
    //    if(keptSplatHeights.Count == 0)//don't want to keep any
    //    {
    //        keptSplatHeights.Add(splatHeights[0]);//add at least 1
    //    }
    //    splatHeights = keptSplatHeights;
    //}

    //public void SplatMaps()
    //{
    //    TerrainData terrainData = Terrain.activeTerrain.terrainData;
    //    SplatPrototype[] newSplatPrototypes;
    //    newSplatPrototypes = new SplatPrototype[splatHeights.Count];
    //    int spindex = 0;
    //    foreach (SplatHeights sh in splatHeights)
    //    {
    //        newSplatPrototypes[spindex] = new SplatPrototype();
    //        newSplatPrototypes[spindex].texture = sh.texture;
    //        newSplatPrototypes[spindex].texture.Apply(true);
    //        spindex++;
    //    }
    //    terrainData.splatPrototypes = newSplatPrototypes;//applies textures to terains avaliable textures for painting
    //}

    //=================================================================================================================

    // Use this for initialization
    void Start () {
        CallSplat();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
