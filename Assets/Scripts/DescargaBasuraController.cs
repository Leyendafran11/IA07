using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescargaBasuraController : MonoBehaviour
{
	bool descargando;
	GameObject conga;
	GameObject puntoDescarga;
	public GameObject basura;

	private void Start()
	{
		puntoDescarga = this.transform.GetChild(0).transform.gameObject;
		descargando = false;
		StartCoroutine("descargaBasura");
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("conga"))
		{
			this.GetComponent<Renderer>().material.color = Color.green;
			descargando = true;
			conga = other.transform.gameObject;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.CompareTag("conga"))
		{
			this.GetComponent<Renderer>().material.color = Color.red;
			descargando = false;
			conga = null;
		}
	}

	private IEnumerator descargaBasura()
	{
		while (true)
		{
			if (descargando == true && conga != null)
			{
				Debug.Log("Descarga");
				conga.GetComponent<Cerebro>().descargandoBasura();
				Instantiate(basura, puntoDescarga.transform.position, Quaternion.identity);
			}

			yield return new WaitForSeconds(0.5f);
		}
	}
}
