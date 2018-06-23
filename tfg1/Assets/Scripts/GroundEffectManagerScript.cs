using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffectManagerScript : MonoBehaviour {
    public Material Left, Right;

    public float min, mid, max;
    public float auxp1, auxp2;
    public float speed;
	void Start () {
        Left.SetFloat("Vector1_6E442B6E", min);
        Right.SetFloat("Vector1_6E442B6E", min);
        auxp1 = min;
        auxp2 = min;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartRightEffect(Transform pos)
    {
        Right.SetVector("Vector3_7D528C8C", pos.position);
        StartCoroutine(RightGroundEffect());
    }

    public void StartLeftEffect(Transform pos)
    {
        Left.SetVector("Vector3_7D528C8C", pos.position);
        StartCoroutine(LeftGroundEffect());
    }
    IEnumerator LeftGroundEffect()
    {
        while(auxp1 <= max)
        {
            Left.SetFloat("Vector1_6E442B6E", auxp1);
            auxp1 += Time.deltaTime * speed;
            yield return null;
        }
        auxp1 = min;

        yield return null;
    }
    IEnumerator RightGroundEffect()
    {
        while (auxp2 <= max)
        {
            Right.SetFloat("Vector1_6E442B6E", auxp2);
            auxp2 += Time.deltaTime * speed;
            yield return null;
        }
        auxp2 = min;

        yield return null;
    }
}
