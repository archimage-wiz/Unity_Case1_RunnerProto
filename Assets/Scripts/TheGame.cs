using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TheGame : MonoBehaviour
{

    [SerializeField] private GameObject _start_panel;
    [SerializeField] private GameObject _ground_panel;
    [SerializeField] private GameObject _spikes;
    [SerializeField] private GameObject _bridge;
    [SerializeField] private GameObject _player;
    [SerializeField] private Text _ui_text;

    private Queue<GameObject> gameObjects = new Queue<GameObject>();
    private int _queue_counter = 0;
    private Coroutine _engine;
    private float _left_offset = 0;
    private float _center_offset = 0;
    private float _right_offset = 0;
    [SerializeField]
    private float hardeness = 10.0f;
    private int line_choice = 1;
    private int _player_health = 100;


    void Start() {

        _left_offset = _center_offset = _right_offset = _ground_panel.transform.localScale.z * 10;
        _engine = StartCoroutine(CreateBlocks());

    }

    public IEnumerator CreateBlocks() {

        while (true) {

            GameObject new_obj = null;
            var spawn_choice = UnityEngine.Random.Range(0.0f, hardeness);
            float _add_offset_z = 1.0f;

            if (spawn_choice >= 0 && spawn_choice <= 5) { 
                new_obj = Instantiate(_ground_panel);
                _add_offset_z = 6;
            }
            if (spawn_choice >= 6 && spawn_choice <= 7.5f) { 
                new_obj = Instantiate(_bridge);
                _add_offset_z = 3;
            }
            // 8 empty hole
            if (spawn_choice >= 8.5f) { 
                new_obj = Instantiate(_spikes);
                _add_offset_z = 0;
            }

            float _offset_x = 0;
            float _offset_z = 0;
            if (line_choice == 0) {
                _offset_x = -1.0f;
                _offset_z = _left_offset;
                _left_offset += _add_offset_z;
            }
            if (line_choice == 1) {
                _offset_x = 0;
                _offset_z = _center_offset;
                _center_offset += _add_offset_z;
            }
            if (line_choice == 2) {
                _offset_x = 1.0f;
                _offset_z = _right_offset;
                _right_offset += _add_offset_z;
            }
            if (new_obj != null) { 
                new_obj.transform.position = _start_panel.transform.position + new Vector3(_offset_x, 0, _offset_z);
                gameObjects.Enqueue(new_obj);
                _queue_counter++;
                if (_queue_counter > 250) { 
                    var tmp_obj = gameObjects.Dequeue();
                    Destroy(tmp_obj);
                }
            }

            hardeness += 0.01f;

            line_choice++;
            if (line_choice >= 3) line_choice = 0;

            if (_player.transform.position.y < -1.0f) {
                Damage(100);
            }

            var new_text = "Progress: " + Math.Round(_player.transform.position.z / 10, 2).ToString() + "\n";
            new_text += "Health: ";
            new_text += (_player_health < 0) ? 0 : _player_health;
            new_text += "\n";
            new_text += "Hardeness: " + hardeness.ToString();
            _ui_text.text = new_text;

            if (_player_health < 0) {
                
                EditorApplication.isPaused = true;
            }

            if ((_left_offset -_player.transform.position.z) < 25 || (_center_offset - _player.transform.position.z) < 25 || (_right_offset - _player.transform.position.z) < 25) {
                yield return null;
            } else {
                yield return new WaitForSeconds(1.0f);
            }
            
        }
    }


    public void Damage(int damage) {
        _player_health -= damage;
        //Debug.Log(_player_health);
    }



}
