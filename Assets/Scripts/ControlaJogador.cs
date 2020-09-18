using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;
    private Vector3 direcao;
    public GameObject textGameOver;
    public LayerMask MascaraChao;
    public bool Vivo = true;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;

    private void Start() {
        Time.timeScale = 1;
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if(direcao != Vector3.zero)
            animatorJogador.SetBool("Movendo", true);
        else
            animatorJogador.SetBool("Movendo", false);

        if(!Vivo){
            if(Input.GetButtonDown("Fire1")){
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate(){
        var posicaoDesejada = direcao * Time.deltaTime * Velocidade;
        rigidbodyJogador.MovePosition(rigidbodyJogador.position + posicaoDesejada);

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction*100, Color.red);

        RaycastHit impacto;
        
        if(Physics.Raycast(raio, out impacto, 100, MascaraChao)){
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }
}
