using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject PokeBall;
    [SerializeField] private GameObject Pekatchu;
    [SerializeField] private float strenght = 10f;
    [SerializeField] private bool IsBallShot = false;
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0) && !IsBallShot)
	    {
	        PokeBall.transform.parent = null;
	        Rigidbody rb = PokeBall.GetComponent<Rigidbody>();
	        rb.isKinematic = false;
	        Vector3 force = gameObject.transform.position - PokeBall.transform.position;
	        rb.velocity = force * strenght;
            rb.rotation = new Quaternion(0,15f,5f,0);
	        IsBallShot = true;
	    }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("pokeball"))
        {
            StartCoroutine(Capture());
        }
    }

    IEnumerator Capture()
    {
        for (int i = 10 - 1; i >= 0; i--)
        {  
            PokeBall.transform.localScale = new Vector3((float)i / 20, (float)i / 20, (float)i / 20);
            gameObject.transform.localScale = new Vector3((float)i / 20, (float)i / 20, (float)i / 20);
            yield return new WaitForSeconds(.1f);
        }
        text.text = "Let's see if you caught the pokemon...";
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= 10; i++)
        {
            Pekatchu.transform.localScale = new Vector3((float)i / 20, (float)i / 20, (float)i / 20);
            yield return new WaitForSeconds(.05f);
        }
        yield return new WaitForSeconds(2f);
        Pekatchu.GetComponent<Animator>().enabled = false;
        text.text = "You caught the pokemon!";
    }
}
