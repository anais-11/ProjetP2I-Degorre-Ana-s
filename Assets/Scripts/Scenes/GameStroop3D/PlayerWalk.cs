using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float speed;
    public CapsuleCollider playerCollider;
    public HealthState healthState;


    // Start is called before the first frame update
    void Start()
    {
        healthState = FindObjectOfType<HealthState>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        //Le joueur ne peut avancer seulement si c'est autorisé
        if (healthState.canMove == true)
        {
            //Si on tourne à droite
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 90 * Time.deltaTime, 0);
            }

            //Si on tourne à gauche
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, -90 * Time.deltaTime, 0);
            }

            //Si on avance
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            //Si on recule
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, 0, -(speed / 2) * Time.deltaTime);
            }
        }
       
    }
}
