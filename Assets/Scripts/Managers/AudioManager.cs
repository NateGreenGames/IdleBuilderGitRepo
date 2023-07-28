using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource primarySource;
    public AudioClip[] musicList;
    public float[] musicVolumes;

    private int lastSongPlayed;
    private int nextSong;

    public void Start()
    {
        primarySource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(MusicBox());
    }


    private IEnumerator MusicBox()
    {
        SelectNewSongIndex();
        lastSongPlayed = nextSong;
        primarySource.PlayOneShot(musicList[nextSong], musicVolumes[nextSong]);
        yield return new WaitForSeconds(musicList[nextSong].length);
        StartCoroutine(MusicBox());
    }

    private void SelectNewSongIndex()
    {
        int songIndex = Random.Range(0, musicList.Length);
        if(songIndex == lastSongPlayed)
        {
            SelectNewSongIndex();
        }
        else
        {
            nextSong = songIndex;
        }
    }
}
