using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainUI : MonoBehaviour
{
    public void Retry()
    {
        Invoke("Reset", .2f);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Reset()
    {
        GameManager.instance.ResetRound();
    }
}
