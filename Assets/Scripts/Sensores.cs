using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sensores : MonoBehaviour
{
    private GameObject[] sensores;
    private GameObject objetivo;
    private bool encontrado;
    private RaycastHit rayo;

    private void Awake()
    {
        sensores = new GameObject[13];
        sensores[0] = transform.GetChild(0).transform.GetChild(0).transform.gameObject;
        sensores[1] = transform.GetChild(0).transform.GetChild(1).transform.gameObject;
        sensores[2] = transform.GetChild(0).transform.GetChild(2).transform.gameObject;
        sensores[3] = transform.GetChild(0).transform.GetChild(3).transform.gameObject;
        sensores[4] = transform.GetChild(0).transform.GetChild(4).transform.gameObject;
        sensores[5] = transform.GetChild(0).transform.GetChild(5).transform.gameObject;
        sensores[6] = transform.GetChild(0).transform.GetChild(6).transform.gameObject;
        sensores[7] = transform.GetChild(0).transform.GetChild(7).transform.gameObject;
        sensores[8] = transform.GetChild(0).transform.GetChild(8).transform.gameObject;
        sensores[9] = transform.GetChild(0).transform.GetChild(9).transform.gameObject;
        sensores[10] = transform.GetChild(0).transform.GetChild(10).transform.gameObject;
        sensores[11] = transform.GetChild(0).transform.GetChild(11).transform.gameObject;
        sensores[12] = transform.GetChild(0).transform.GetChild(12).transform.gameObject;

    }

    private void Start()
    {
        
        StartCoroutine("reset");
        
    }

    public bool objetivoEncontrado()
    {
        return encontrado;
    }

    public Vector3 getDestino()
    {        
        if (encontrado == true && objetivo != null)
        {
            return objetivo.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 12; i++)
        {
            if (Physics.Raycast(sensores[i].transform.position, sensores[i].transform.forward, out rayo, 15))
            {
                if (rayo.transform.gameObject.tag == "basura")
                {
                    encontrado = true;
                    objetivo = rayo.transform.gameObject;
                    Debug.DrawRay(sensores[i].transform.position, sensores[i].transform.forward * rayo.distance, Color.red);
                }
                else
                {
                    Debug.DrawRay(sensores[i].transform.position, sensores[i].transform.forward * rayo.distance, Color.white);
                }
            }
            else
            {
                Debug.DrawRay(sensores[i].transform.position, sensores[i].transform.forward * 15, Color.white);
            }
        }
    }

    private IEnumerator reset()
    {
        while (true)
        {
            if (encontrado == true || objetivo != null)
            {
                encontrado = false;
                objetivo = null;
            }

            yield return new WaitForSeconds(0.5f);

        }
    }
   
}
