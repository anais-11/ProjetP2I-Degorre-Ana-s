using UnityEngine;
using System.Collections;

public class Diams : MonoBehaviour
{
    public int id { get; set; }
    public string color { get; set; }
    public bool isInCollision;
    StroopGameManager gameManager;
    public GameObject interactObject;
    public Animator interactAnimator;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<StroopGameManager>();
        interactObject = GameObject.Find("Interact");
        interactAnimator = interactObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCollision && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            interactAnimator.SetBool("isOpen", false);
            VerifyDiamsColor();
        }

    }

    private void OnCollisionEnter(Collision infoCollision)
    {
        isInCollision = true;
        interactAnimator.SetBool("isOpen", true);

    }
    private void OnCollisionExit(Collision infoCollision)
    {
        isInCollision = false;
        interactAnimator.SetBool("isOpen", false);
    }

    public void VerifyDiamsColor()
    {
        gameManager.VerifyOrderDiams(color);
    }

    
}
