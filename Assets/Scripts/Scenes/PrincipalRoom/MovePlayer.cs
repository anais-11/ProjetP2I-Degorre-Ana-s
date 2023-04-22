using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f; //Vitesse de déplacement du joueur
    public bool canMove; //Permet d'autoriser le joueur à se déplacer ou non
    private Animator animator; //Récupère l'animator qui permet d'ajouter les animations définies
    public Rigidbody2D rigidBody; //Récupère le composant du personnage

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        //On récupère les composant nécessaires
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame


    private void Update()
    {
        //On vérifie si le joueur est en train d'appuyer sur une flêche du clavier, et s'il est autorisé à se déplacer
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) && canMove)
        {
            //On récupère les coordonnées du joueur
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                //On change la valeur de l'entier de la variable dans l'animator pour activer la bonne animation
                //Droit ou gauche
                animator.SetInteger("Direction", (int)horizontal);
            }
            else
            {
                //On change la valeur de l'entier de la variable dans l'animator pour activer la bonne animation
                //Haut ou bas
                animator.SetInteger("Direction", (int)vertical * 2);
            }

            //On définit un nouveau vecteur de position pour déplacer le personnage
            Vector2 movement = new Vector2(horizontal, vertical);
            rigidBody.MovePosition(rigidBody.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            //Lorsqu'on veut que l'animation du joueur soit statique
            animator.SetInteger("Direction", 0);
        }

    }

    public void Replay()
    {
        canMove = true;
        rigidBody.transform.position = new Vector2(-0.13f, -2.18f);
    }

}

