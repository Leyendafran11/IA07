using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaController : MonoBehaviour
{
	bool cargando;
	GameObject conga;

	private void Start()
	{
		cargando = false;
		StartCoroutine("cargandoBateria");
	}


	private void OnTriggerEnter(Collider other)
	{
        if (other.transform.CompareTag("conga"))
        {
			this.GetComponent<Renderer>().material.color = Color.green;
			cargando = true;
			conga = other.transform.gameObject;
        }

    }

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.CompareTag("conga"))
		{
			this.GetComponent<Renderer>().material.color = Color.red;
			cargando = false;
			conga = null;
		}
	}

	private IEnumerator cargandoBateria()
	{
		while (true)
		{
			if (cargando = true && conga != null)
			{
				conga.GetComponent<Cerebro>().cargaBateria();
			}

			yield return new WaitForSeconds(0.1f);
		}
	}
}
