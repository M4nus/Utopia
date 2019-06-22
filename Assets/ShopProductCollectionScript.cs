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
    void Start()
    {
        ChoosenProducts = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickProduct(int productIndex)
    {
        ChoosenProducts.Add(productIndex);
        if (ChoosenProducts.Count == 3)
        {
            Debug.Log("All products choosen: " + ChoosenProducts[0] + " " + ChoosenProducts[1] + " " + ChoosenProducts[2]);
            
        }
    }
}
