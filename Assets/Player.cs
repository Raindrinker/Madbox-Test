using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {

    public int position = 1;
    public Boolean playerControlled = true;

    public GameObject canvas;

    public Path path;
    
    private GameObject character;
    private GameObject camera;

    private Boolean win = false;
    
    void Start() {
        
        Debug.Log(path.getStart(position));
        
        transform.position = path.getStart(position);
        
        character = transform.GetChild(0).gameObject;
        camera = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update() {

        if (win) {
            return;
        }

        if (Input.GetMouseButton(0) || !playerControlled) {
            transform.position = path.advance(transform.position, 10.0f * Time.deltaTime, position);
            character.GetComponent<Animator>().SetBool("running", true);
        }
        else {
            character.GetComponent<Animator>().SetBool("running", false);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, path.getDirection(), 0.05f);

        if (path.isFinish() && !win) {
            if (playerControlled) {
                camera.GetComponent<Animation>().Play();
                canvas.SetActive(true);
            }

            win = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("hazard")) {
            restart();
        }
    }

    private void restart() {
        path.reset();
        transform.position = path.getStart(position);
        transform.rotation = path.getDirection();
        
        character = transform.GetChild(0).gameObject;
    }

    public void restartGame() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
