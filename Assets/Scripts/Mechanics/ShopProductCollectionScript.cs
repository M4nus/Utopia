using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopProductCollectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> ChoosenProducts;
    public GameObject Antoni;
    public bool AllowProducktPickup = false;
    public GameObject Elevator;
    public GameObject ControlLights;
    void Start()
    {
        ChoosenProducts = new List<int>();
    }

    void PickProduct(int productIndex)
    {
        ChoosenProducts.Add(productIndex);
        FindObjectOfType<AudioManager>().Play("itemTake");
        if (ChoosenProducts.Count == 3)
        {
            Debug.Log("All products choosen: " + ChoosenProducts[0] + " " + ChoosenProducts[1] + " " + ChoosenProducts[2]);
            if((ChoosenProducts[0]==0 || ChoosenProducts[0] == 11 || ChoosenProducts[0] == 18) &&
               (ChoosenProducts[1] == 0 || ChoosenProducts[1] == 11 || ChoosenProducts[1] == 18) &&
               (ChoosenProducts[2] == 0 || ChoosenProducts[2] == 11 || ChoosenProducts[2] == 18))
            {
                ControlLights.SendMessage("setOrangeActive");
            }
            else
            {
                ControlLights.SendMessage("setBlueActive");
                GameObject.Find("GameSettings").GetComponent<GameSettingsScript>().ShopOrange = false;
            }

        }
    }
}
