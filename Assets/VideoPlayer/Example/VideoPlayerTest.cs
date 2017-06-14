using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerTest : MonoBehaviour {
    public string URL;
    public RenderTexture MovieTexture;
    public RenderTextureToImage UI_Image;

    IEnumerator Start () {
        var player = GetComponent<VideoPlayer>();
        var audioSource = gameObject.GetComponent<AudioSource>();

        if (!player)
            player = gameObject.AddComponent<VideoPlayer>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();

        MovieTexture = new RenderTexture(1280, 720, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);

        player.renderMode = VideoRenderMode.RenderTexture;
        player.targetTexture = MovieTexture;

        player.SetTargetAudioSource(0, audioSource);
        player.audioOutputMode = VideoAudioOutputMode.AudioSource;
        player.EnableAudioTrack(0, true);

		player.url = URL;
        player.Prepare();

        while (!player.isPrepared)
            yield return null;
        
		if (UI_Image)
			UI_Image.Texture = MovieTexture;
        
		GetComponent<MeshRenderer>().material.mainTexture = MovieTexture;

        foreach (var v in FindObjectsOfType<RenderTextureToMainTex>())
            v.GetComponent<MeshRenderer>().sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;

        foreach (var v in FindObjectsOfType<RenderTextureToLightCookie>())
            v.Texture = MovieTexture;

        player.Play();
        audioSource.Play();

        while (player.isPlaying)
        {
            yield return null;
        }

        Debug.Log("Playback complete");
	}
}
