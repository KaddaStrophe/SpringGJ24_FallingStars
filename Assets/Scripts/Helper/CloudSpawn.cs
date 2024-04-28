using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour {
    [SerializeField]
    RectTransform rect = default;
    [SerializeField]
    GameObject cloudContainer = default;
    [SerializeField]
    List<Cloud> clouds = default;
    [SerializeField]
    float cloudSpawnRate = 2f;

    bool waitForCloudSpawn = false;

    protected void FixedUpdate() {
        if (!waitForCloudSpawn) {
            StartCoroutine(SpawnCloudWithDelay());
            waitForCloudSpawn = true;
        }
    }

    IEnumerator SpawnCloudWithDelay() {
        yield return new WaitForSeconds(cloudSpawnRate);
        float cloudPos = cloudContainer.transform.position.y - Random.Range(0, Mathf.Abs(rect.sizeDelta.y));
        var cloudInstance = Instantiate(clouds[Random.Range(0, clouds.Count)], cloudContainer.transform);
        cloudInstance.transform.position = new Vector3(cloudContainer.transform.position.x, cloudPos, cloudContainer.transform.position.z);
        cloudInstance.transform.localScale = new Vector3(.4f, .4f, .4f);
        waitForCloudSpawn = false;
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Cloud")) {
            Destroy(collision.gameObject);
        }
    }
}
