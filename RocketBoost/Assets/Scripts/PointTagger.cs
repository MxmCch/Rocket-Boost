using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTagger : MonoBehaviour
{
    bool isActive = true;
    
    void OnTriggerEnter(Collider other) {
        if (isActive)
        {
            isActive = false;
            int pointText = other.gameObject.GetComponent<Tagger>().points; 
            pointText++;
            other.gameObject.GetComponent<Tagger>().points = pointText;
            Destroy(this.gameObject);
        }
    }
}
