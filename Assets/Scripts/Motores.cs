using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Motores : MonoBehaviour
{
    private NavMeshAgent nv;
    private Vector3 destino;
    private bool estaMoviendose;
    
    private void Awake()
    {
        nv = this.GetComponent<NavMeshAgent>();
    }

    public void parar()
    {
        nv.SetDestination(this.transform.position);
        estaMoviendose = false;
    }

    public void mover(Vector3 destino)
    {
        if (!destino.Equals(Vector3.zero))
        {
            this.destino = destino;
            StartCoroutine("irADestino");
        }
    }

    private IEnumerator irADestino()
    {
        estaMoviendose = true;
        nv.SetDestination(this.destino);

        while(Vector3.Distance(transform.position, this.destino) > 1.0f)
        {
            yield return new WaitForFixedUpdate();
        }
        
        estaMoviendose = false;
    }

    public bool seMueve()
    {
        return estaMoviendose;
    }
}
