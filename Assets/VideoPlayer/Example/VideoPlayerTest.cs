using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerTest : MonoBehaviour
{
    public string URL;
    public RenderTexture MovieTexture;
    public RenderTextureToImage UI_Image;
    VideoPlayer player;
    AudioSource audioSource;
    Coroutine ActiveCoroutine;

    IEnumerator Start()
    {
        player = GetComponent<VideoPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();

        if (!player)
            player = gameObject.AddComponent<VideoPlayer>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();

        MovieTexture = new RenderTexture(1280, 720, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);

        ActiveCoroutine = StartCoroutine(PlayVideo());
        yield return ActiveCoroutine;
    }

    IEnumerator PlayVideo()
    {
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
			yield return null;
    }

    void OnGUI()
    {
        var rect = new Rect(2, Screen.height - 150 - 2, 500, 150);
        GUI.Box(rect, GUIContent.none);

        rect = new Rect(rect.x + 5, rect.y + 5, rect.width - 10, rect.height - 10);
        GUILayout.BeginArea(rect);
        GUILayout.Label("Video URL");
        URL = GUILayout.TextField(URL);

        if (GUILayout.Button("Open URL"))
        {
            StopCoroutine(ActiveCoroutine);
            StartCoroutine(PlayVideo());
        }

        GUILayout.BeginHorizontal();

        float seconds = ((float)player.frame) / player.frameRate;
        float endseconds = ((float)player.frameCount) / player.frameRate;

        var timef = Mathf.Round(seconds);
        var endtimef = Mathf.Round(endseconds);

        var time = System.TimeSpan.FromSeconds(timef).ToString();
        var endtime = System.TimeSpan.FromSeconds(endtimef).ToString();

        GUILayout.Label(time + "/" + endtime, GUILayout.MaxWidth(120));
        var temp = player.frame;
        temp = (long)GUILayout.HorizontalSlider(player.frame, 0, player.frameCount);
        if (temp != player.frame)
            player.frame = temp;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
        {
            player.frame = 0;
        }

		if (GUILayout.Button("<"))
		{
			--player.frame;
		}

        if (player.isPlaying)
        {
            if (GUILayout.Button("| |"))
            {
                player.Pause();
                audioSource.Pause();
            }
        }
        else
        {
            if (GUILayout.Button("|>"))
            {
                player.Play();
                audioSource.Play();
            }
        }

		if (GUILayout.Button(">"))
		{
			++player.frame;
		}

		if (GUILayout.Button(">>"))
		{
            player.frame = (long)player.frameCount;
		}

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}
