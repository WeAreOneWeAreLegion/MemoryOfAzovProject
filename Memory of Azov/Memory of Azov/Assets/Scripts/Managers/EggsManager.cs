using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsManager : MonoSingleton<EggsManager> {

    public Texture[] textures;

    private bool[] checker;
    private int currentIndex = 0;

	void Start ()
    {
        checker = new bool[textures.Length];
    }
	
	public Texture GetRandomTexture()
    {
        currentIndex  = Random.Range(0, textures.Length);

        while (checker[currentIndex])
            currentIndex = Random.Range(0, textures.Length);

        checker[currentIndex] = true;

        return textures[currentIndex];
    }

    public Texture GetCurrentTexture()
    {
        return textures[currentIndex];
    }

}
