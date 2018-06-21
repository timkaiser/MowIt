using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gras : MonoBehaviour {
    int xIndex, yIndex;

    public Material m_low;
    public Material m_high;

    // Update is called once per frame
    void Update () {
        if (GlobalVariables.get().isMowed(xIndex,yIndex))
        {
            this.gameObject.GetComponent<MeshRenderer>().material = m_low;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = m_high;
        }
	}


    public void setPosition(int x, int y)
    {
        this.xIndex = x;
        this.yIndex = y;
    }
}
