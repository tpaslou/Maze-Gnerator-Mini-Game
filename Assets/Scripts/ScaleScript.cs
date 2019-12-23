using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void ScalePlus()
    {
        this.transform.localScale+=new Vector3(0.1f,0,0.1f);

    }

    public void ScaleMinus()
    {
        this.transform.localScale-=new Vector3(0.1f,0,0.1f);

    }
}
