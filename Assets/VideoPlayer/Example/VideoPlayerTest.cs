using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerTest : MonoBehaviour {
    public string URL;
    public RenderTexture MovieTexture;
    public RenderTextureToImage UI_Image;

    void Start () {
        var player = GetComponent<VideoPlayer>();
        if (!player)
            player = gameObject.AddComponent<VideoPlayer>();
        
        MovieTexture = new RenderTexture(1280, 720, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
        if (UI_Image)
        {
            UI_Image.Texture = MovieTexture;
        }


		player.url = URL;
        player.renderMode = VideoRenderMode.RenderTexture;
        player.targetTexture = MovieTexture;
        player.Play();

        GetComponent<MeshRenderer>().material.mainTexture = MovieTexture;
	}
	
}
