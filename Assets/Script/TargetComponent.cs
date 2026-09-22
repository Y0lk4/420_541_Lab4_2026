using UnityEngine;

public class TargetComponent : MonoBehaviour
{

    // variable of the renderer of the target to modify the target color
    private Renderer targetRenderer;
    //the original color of the target
    private Color originalColor;
    //the color the target will change to when hit.
    public Color hitColor = Color.green;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the renderer component and assign it or well.. have its reference to the variable.
        targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalColor = targetRenderer.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OllisionEnter(Collision collision)
    {
        //gets the object that collides with the target
        //and then checks if its a projectile by looking at its tag.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //gets the score from the game manager and increments it
            //using its IncrementScore() method 
            GameManager.Instance.IncrementScore();

            if (targetRenderer != null)
            {
                //changes the color of the target when hit
                //and if its not null
                targetRenderer.material.color = hitColor;
            }

            // change the color back to default after 5 seconds
            //calls the resetcolor() method
            Invoke("ResetColor", 5f);
        }
    }

    private void ResetColor()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = originalColor;
        }
    }
}
