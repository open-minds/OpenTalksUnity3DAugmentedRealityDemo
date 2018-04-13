using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoldierSceneManager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text endingText;
    [SerializeField] private Image image;
    [SerializeField] private GameObject Soldier;
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameObject[] Ballons;
    [SerializeField] private Transform[] BallonSpawner;
    [SerializeField] private AudioSource musicPlayer;
    private bool IsAttack;
    private bool IsDance;
    private bool IsTurn;
    
	// Update is called once per frame
	void Update () {
        //When we tap the phone
	    if (Input.GetMouseButtonDown(0))
	    {
	        text.text = "Fight!";
	        Soldier.GetComponent<Animator>().SetBool("Aim",true);
	        Zombie.GetComponent<Animator>().SetBool("attack",true);
	        IsAttack = true;
	        StartCoroutine(endingAnimation());
	    }

	    #region WeirdCode
	    if(IsAttack) Zombie.transform.position = Vector3.Lerp(Zombie.transform.position, Soldier.transform.position , Time.deltaTime / 4);
	    if (IsDance)
	    {
	        Vector3 target = new Vector3(Camera.main.transform.position.x,
	            1, Camera.main.transform.position.z);
            Quaternion q = Quaternion.LookRotation(target - Zombie.transform.position);
	        Zombie.transform.rotation = Quaternion.Lerp(Zombie.transform.rotation, q, Time.deltaTime);
	        }
	    if (IsTurn)
	    {
	        Vector3 target = new Vector3(Camera.main.transform.position.x,
	            1, Camera.main.transform.position.z);
	        Quaternion q = Quaternion.LookRotation(target - Soldier.transform.position);
	        Soldier.transform.rotation = Quaternion.Lerp(Soldier.transform.rotation, q, Time.deltaTime);
        }
        //if(Canvas.activeInHierarchy) Canvas.transform.LookAt(Camera.main.transform);
	    #endregion
    }
    
    IEnumerator endingAnimation()
    {
        yield return new WaitForSeconds(2.5f); //Zombie walking
        IsDance = true; 
        IsAttack = false;
        Zombie.GetComponent<Animator>().SetBool("attack", false); //Zombie stop walking
        yield return new WaitForSeconds(2f); //Zombie turning
        Zombie.GetComponent<Animator>().SetBool("dance",true); //Zombie dancing
        StartCoroutine(spawningBallons());
        yield return new WaitForSeconds(2f); 
        IsTurn = true;  //SpaceBoy turning
        yield return new WaitForSeconds(2f); //Awkward pause
        Canvas.SetActive(true); //Talk popup
    }

    #region SecondWeirdCode
    IEnumerator spawningBallons()
    {
        musicPlayer.Play();
        for (int i = 0; i < 20; i++)
        {
            int index = Random.Range(0, BallonSpawner.Length);
            GameObject ballon = Instantiate(Ballons[Random.Range(0, Ballons.Length)], BallonSpawner[index]);
            ballon.GetComponent<Rigidbody>().velocity = ballon.transform.up * 10f;
            Destroy(ballon, 3f);
            yield return new WaitForSeconds((float) Random.Range(8,15)/10);
        }
    }
    #endregion

    public void loadSecretPorject()
    {
        SceneManager.LoadScene("Main");
    }
}
