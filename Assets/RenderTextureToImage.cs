using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureToImage : MonoBehaviour {
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
            GetComponent<RawImage>().texture = m_tex;
        }
    }
}
