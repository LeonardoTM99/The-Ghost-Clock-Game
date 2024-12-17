using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFade : MonoBehaviour
{
    public Animator TextAnim;

    private void Start()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(3);

        TextAnim.SetBool("isFade", true);
        TextAnim.SetBool("isFadeOut", false);

        yield return new WaitForSeconds(13);

        TextAnim.SetBool("isFade", false);
        TextAnim.SetBool("isFadeOut", true);

        yield return new WaitForSeconds(6);

        SceneManager.LoadScene("Downtown");
        ResetGame();

    }

    void ResetGame()
    {

        ItemCounter.Instance.itemCount = 0;
        MainManager.Instance.npcItemGivenStatus.Clear();
        CountdownManager.Instance.SetRemainingTime(900);
    }
}
