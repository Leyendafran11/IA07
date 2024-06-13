using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerebro : MonoBehaviour
{
    [SerializeField]private int bateria;
	private Vector3 destino;
	private Motores motores;
	private Sensores sensores;
	private int basura;

	public GameObject zonaCarga;
	public GameObject zonaBasura;

	private void Awake()
	{
		motores = this.GetComponent<Motores>();
		sensores = this.GetComponent<Sensores>();
	}

	private void Start()
	{
		basura = 0;
		bateria = 100;
		StartCoroutine("descargaBateria");
	}

	public void cargaBateria()
	{
		bateria++;
	}

	public void descargandoBasura()
	{
		basura--;
	}

	private IEnumerator descargaBateria()
	{
		while (true)
		{
			if (bateria > 0)
			{
				bateria--;
			}

			yield return new WaitForSeconds(0.2f);
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("basura"))
		{
			basura++;
			Destroy(other.transform.gameObject);
		}
	}

	[Task]
	private void bateriaBaja()
	{
		if (bateria < 20)
		{
			motores.parar();
			destino = zonaCarga.transform.position;	
			Task.current.Succeed();

		}
		else
		{
			Task.current.Fail();
		}
	}

	[Task]
	private void irDestino()
	{
		motores.mover(destino);
		Task.current.Succeed();
	}

	[Task]
	private void llegoDestino()
	{
		if (!motores.seMueve())
		{
			destino = Vector3.zero;
			Task.current.Succeed();
		}

	}

	[Task]
	private void destinoAleatorio()
	{
		motores.parar();
		destino = new Vector3(Random.Range(-4.0f, 4.0f), 0, Random.Range(-4.0f, 4.0f));
		Task.current.Succeed();
	}


	[Task]
	private void bateriaCargada()
	{

		if (bateria == 100)
		{
			Task.current.Succeed();
		}

	}

	[Task]
	private void basuraEncontrada()
	{
		if (sensores.objetivoEncontrado())
		{
			//motores.parar();
			destino = sensores.getDestino();
			Task.current.Succeed();
		}
		/*else
		{
			Task.current.Fail();
		}*/
	}

	[Task]
	private void basuraDescargada()
	{
		if (basura == 0)
		{
			Task.current.Succeed();
		}
	}

	[Task]
	private void basuraLlena()
	{
		if (basura > 5)
		{
			motores.parar();
			destino = zonaBasura.transform.position;
			Task.current.Succeed();
		}
		else
		{
			Task.current.Fail();
		}
	}







}
