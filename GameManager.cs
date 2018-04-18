using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region declaration
    [SerializeField] private GameObject[] FireworksPositions; //different spawn points for fireworks
    [SerializeField] private GameObject Particles; //the fireworks
    [SerializeField] private Text text; //information text
    [SerializeField] private GameObject PokeBall; //The pokeball that we use to catch the pokemon
    [SerializeField] private GameObject Pekatchu; //Pikatchu (hidden at first)
    [SerializeField] private float strenght = 2f; //the strength of the throw of the pokeball
    [SerializeField] private bool IsBallShot; //is the pokeball thorwn yet
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsBallShot) //when we tap the phone and the ball isn't thorwn yet 
        {
            PokeBall.transform.parent = null; //the ball doesnt follow the camera anymore cause it s not it's child now
            Rigidbody rb = PokeBall.GetComponent<Rigidbody>(); //we get the rigidBody of the pokeball and store it in rb
            rb.isKinematic = false; //we disable kinematric caracteristic
            Vector3 force = Vector3.Magnitude(gameObject.transform.position - PokeBall.transform.position) *
                            Camera.main.transform.forward;
            //the direction that the ball should follow 
            rb.velocity = force; //we give the force to the rigidbody's velocity
            rb.rotation = new Quaternion(0, 15f, 5f, 0); //we start a rotation of the ball
            IsBallShot = true; //we alert that we thrown the ball
        }
    }

    void OnCollisionEnter(Collision other) //this is a function that gets called when a collision happens
    {
        if (other.gameObject.tag.Equals("pokeball")) //if the object that hit us has a tag called "pokeball"
        {
            StartCoroutine(Capture()); //when the pokeball hits the pokemon, we call a function
        }
    }

    IEnumerator Capture() //this is called a Coroutine, it helps us do things with time intervals between them
    {
        yield return new WaitForSeconds(.5f); //yiel means break, we stop and wait for half a second

        #region pokemon animation showing
        for (int i = 10 - 1; i >= 0; i--)
        {
            PokeBall.transform.localScale = new Vector3((float) i / 20, (float) i / 20, (float) i / 20);
            gameObject.transform.localScale = new Vector3((float) i / 20, (float) i / 20, (float) i / 20);
            yield return new WaitForSeconds(.1f);
        }
        #endregion

        text.text = "Let's see if you caught the pokemon..."; //we change the info text
        yield return new WaitForSeconds(1f); //we wait for a second

        #region pokemon animation showing (pikatchu)
        for (int i = 0; i <= 10; i++)
        {
            Pekatchu.transform.localScale = new Vector3((float) i / 20, (float) i / 20, (float) i / 20);
            yield return new WaitForSeconds(.05f);
        }
        #endregion

        yield return new WaitForSeconds(1f); //we wait for a second
        Pekatchu.GetComponent<Animator>().enabled = false; //we stop an animation that no one noticed
        text.text = "You caught the pokemon!"; //we change the info text
        Instantiate(Particles, FireworksPositions[0].transform); //creating a firework at location 1
        Instantiate(Particles, FireworksPositions[1].transform); //creating a firework at location 2
    }
}