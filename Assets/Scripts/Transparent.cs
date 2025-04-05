/*
 * Name: Jack Gu
 * Date: 4/4/25
 * Desc: Lets UI raycasts pass through transparent areas so that mouse controls are more precise
 */

using UnityEngine;
using UnityEngine.UI;

public class Transparent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
