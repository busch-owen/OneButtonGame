using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource _musicSource;
    [SerializeField] AudioClip[] musicList;
    AudioClip currentSong;
    // Start is called before the first frame update
    void Start()
    {
        _musicSource.loop = false;
        _musicSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (!_musicSource.isPlaying)
        {
            StartCoroutine(ChangeSong());
        }
        else
            return;
    }
    IEnumerator ChangeSong()
    {
        yield return new WaitForSeconds(2f);
        int rand = Random.Range(0, musicList.Length);
        currentSong = musicList[rand];
        _musicSource.clip = currentSong;
        _musicSource.Play();
    }
}
