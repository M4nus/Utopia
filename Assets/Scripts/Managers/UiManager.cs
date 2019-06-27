using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject introObj;
    public SpriteRenderer sprite;
    Animation _intro;


    // Start is called before the first frame update
    void Start()
    {
        sprite.GetComponent<SpriteRenderer>();
        _intro = introObj.GetComponent<Animation>();
        StartCoroutine(Queue());
    }

    IEnumerator Queue()
    {
        Debug.Log(_intro);
        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(30f);
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(ChangeScene());
    }

    public IEnumerator FadeIn()
    {
        float currentAlpha = sprite.color.a;

        while(currentAlpha < 1f)
        {
            currentAlpha += Time.deltaTime;
            sprite.color = new Color(0, 0, 0, currentAlpha);
            yield return null;
        }
        Debug.Log("FadeIn");
    }

    public IEnumerator FadeOut()
    {
        float currentAlpha = sprite.color.a;

        while(currentAlpha > 0f)
        {
            currentAlpha -= Time.deltaTime;
            sprite.color = new Color(0, 0, 0, currentAlpha);
            yield return null;
        }
        Debug.Log("FadeOut");
    }

    IEnumerator ChangeScene()
    {
        SceneManager.LoadScene("4RoomMap");
        yield return null;
    }
}
