using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Jogador;
    public float Velocidade = 5;

    private Rigidbody rigidbodyInimigo;
    private Animator animatorInimigo;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyInimigo = GetComponent<Rigidbody>();
        animatorInimigo = GetComponent<Animator>();

        Jogador = GameObject.FindWithTag("Player");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        Vector3 direcao = Jogador.transform.position - transform.position;
        //Fazer o Zumbi olhar para o jogador
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyInimigo.MoveRotation(novaRotacao);

        //2 por causa do raio do colisor de cada um dos personagens, que é igual a 1
        //O .5 é um fator de ajuste por causa do arrasto dos personagens
        if(distancia > 2.5){
            rigidbodyInimigo.MovePosition(rigidbodyInimigo.position + direcao.normalized * Velocidade * Time.deltaTime);            
            animatorInimigo.SetBool("Atacando", false);
        }
        else{
            animatorInimigo.SetBool("Atacando", true);
        }
    }

    void AtacaJogador(){
        Time.timeScale = 0;
        Jogador.GetComponent<ControlaJogador>().textGameOver.SetActive(true);
        Jogador.GetComponent<ControlaJogador>().Vivo = false;
    }
}
