using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnSceneAwake : MonoBehaviour
{
    public bool loadGame = false;

    private void Start()
    {
        if(loadGame)
            SavingLoading.instance.OnClick_Load();
    }
}
