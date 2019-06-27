using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool BoardOrange = true;
    public bool ShopOrange = true;
    public bool ColorOrange = true;
    public SpriteRenderer sprite;


    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        sprite.GetComponent<SpriteRenderer>();
    }       

    IEnumerator GoToEndScreen()
    {
        Debug.Log("StartPhaseOut");
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(3.0f);
        if (ShopOrange && BoardOrange && ColorOrange)
        {
            Debug.Log("Load Scene End Orange");
            SceneManager.LoadScene("EndOrange");
        }
        else
        {
            bool stopLoop = false;
            int pomSceneChoice = 0;
            for(int i=0;i<200&&!stopLoop;i++)
            {
                pomSceneChoice = (int)Random.Range(1, 4);
                if (pomSceneChoice == 1 && !ColorOrange)
                {
                    stopLoop = true;
                    Debug.Log("Load Scene End Blue Color");
                    SceneManager.LoadScene("EndBlueColor");
                }
                else if (pomSceneChoice == 2 && !ShopOrange)
                {
                    stopLoop = true;
                    Debug.Log("Load Scene End Blue Shop");
                    SceneManager.LoadScene("EndBlueShop");
                }
                else if (pomSceneChoice == 3 && !BoardOrange)
                {
                    stopLoop = true;
                    Debug.Log("Load Scene End Blue Board");
                    SceneManager.LoadScene("EndBlueBoard");
                }
                else
                {
                    Debug.Log("Tried to end game and failed");
                }
                if (i == 100)
                {
                    Debug.Log("100 tries later");
                }           
            }
        }     
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
    }
}
