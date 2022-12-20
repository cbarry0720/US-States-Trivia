using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alien : MonoBehaviour
{
    public GameObject player;
    public GameObject orb;
    public TMP_Text health;
    public float alien_speed = 0.02f;
    public float orb_speed = 0.04f;
    private GameObject[] orbs = new GameObject[10];
    private float[] orb_times = new float[10];
    private Animator animator;

    public AudioClip plasmaShot;
    public float vol = 3f;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("state", 0);
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 11, transform.position.z), new Vector3(player.transform.position.x, 11, player.transform.position.z), alien_speed);
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

        for (int i = 0; i < orbs.Length; i++)
        {
            if (orbs[i] != null)
            {
                orb_times[i] += Time.deltaTime;
                if (orb_times[i] > 16)
                {
                    Destroy(orbs[i]);
                    orbs[i] = null;
                    orb_times[i] = 0;
                }
            }
        }

        if (Time.frameCount % 240 == 0)
        {
            animator.SetInteger("state", 1);
            for (int i = 0; i < orbs.Length; i++)
            {
                if (orbs[i] == null)
                {
                    orbs[i] = Instantiate(orb, transform.position, Quaternion.identity);
                    source.PlayOneShot(plasmaShot, vol);
                    break;
                }
            }
        }
        for (int i = 0; i < orbs.Length; i++)
        {
            GameObject orb = orbs[i];
            if (orbs[i] != null)
            {
                orbs[i].transform.position = Vector3.MoveTowards(orbs[i].transform.position, player.transform.position, orb_speed);
                RaycastHit hit;
                try
                {
                    if (Physics.Raycast(new Vector3(orb.transform.position.x, orb.transform.position.y + 1, orb.transform.position.z), Vector3.down, out hit, 1f))
                    {
                        if (hit.collider.name == "PlayerArmature")
                        {
                            Destroy(orb);
                            orbs[i] = null;
                            health.text = "Health: " + (int.Parse(health.text.Substring(8)) - 5);

                            hit.collider.gameObject.GetComponent<AudioSource>().PlayOneShot(
                                hit.collider.gameObject.GetComponent<Player>().takeDamage,
                                hit.collider.gameObject.GetComponent<Player>().vol
                            );
                        }
                        else if (hit.collider.name != "alien character")
                        {
                            Destroy(orb);
                            orbs[i] = null;
                            orb_times[i] = 0;
                        }
                    }
                }
                catch (System.NullReferenceException e)
                {
                    continue;
                }
            }
        }
    }
}
