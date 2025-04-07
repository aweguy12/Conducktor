/*
 * Name: Jack Gu
 * Date: 4/4/25
 * Desc: Makes transparent areas of Images actually transparent for clicks
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
}
