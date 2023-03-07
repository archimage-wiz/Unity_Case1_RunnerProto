using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private float player_speed = 2;
    [SerializeField] private float player_speed_delta = 0.05f;
    private float _force_jump = 250;
    private float _side_speed = 15;
    [SerializeField] private Rigidbody _player_rigidbody;
    private bool _can_jump = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5.1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate() {
    }
    private void FixedUpdate() {
        transform.position += transform.forward * player_speed * Time.deltaTime;
        player_speed += player_speed_delta * Time.deltaTime;
        if (_can_jump) {
            if (Input.GetKey(KeyCode.Space)) {
                _can_jump = false;
                _player_rigidbody.AddForce(transform.up * _force_jump);
            }
            var direction = Input.GetAxis("Horizontal");
            _player_rigidbody.velocity += direction * _side_speed * Time.deltaTime * transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        _can_jump = true;
    }

}
