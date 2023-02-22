using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUIElement : MonoBehaviour
{
    [SerializeField] private Transform blackPanel;

    [SerializeField] private bool hasToScale;
    [SerializeField] private float timePassed;
    [SerializeField] private float time;
    // Start is called before the first frame update
    void Awake()
    {
        blackPanel.localScale = new Vector3(1f, 0f, 1f);
        hasToScale = false;
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasToScale)
        {
            timePassed += Time.deltaTime;
            blackPanel.localScale = new Vector3(1f, Mathf.Lerp(0f, 1f, timePassed / time), 1f);
            if (timePassed > time)
            {
                hasToScale = false;
                Destroy(this.gameObject);
            }
        }
    }

    public void StartScale(float time)
    {
        hasToScale = true;
        this.time = time;
    }
}
