using UnityEngine;

public class ECdestroyMe : MonoBehaviour
{
    float timer;
    public float deathtimer = 10;

	private void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= deathtimer)
            Destroy(gameObject);
	}
}