using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureToMainTex : MonoBehaviour {
	private RenderTexture m_tex;
	public RenderTexture Texture
	{
		get
		{
			return m_tex;
		}
		set
		{
			m_tex = value;
            GetComponent<MeshRenderer>().material.mainTexture = m_tex;
		}
	}
}
