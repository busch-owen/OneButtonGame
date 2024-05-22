using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] AudioClip[] musicList;
    AudioClip currentSong;
    
    private void Awake()
    {
        int rand = Random.Range(0, musicList.Length);
        currentSong = musicList[rand];
        _musicSource.clip = currentSong;
        _musicSource.Play();
        _musicSource.loop = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_musicSource.isPlaying)
        {
            //StartCoroutine(ChangeSong());
            int rand = Random.Range(0, musicList.Length);
            currentSong = musicList[rand];
            _musicSource.clip = currentSong;
            _musicSource.Play();
        }
        else
            return;
    }
    /*IEnumerator ChangeSong()
    {
        yield return new WaitForSeconds(2f);
        int rand = Random.Range(0, musicList.Length);
        currentSong = musicList[rand];
        _musicSource.clip = currentSong;
        _musicSource.Play();
    }*/
}
