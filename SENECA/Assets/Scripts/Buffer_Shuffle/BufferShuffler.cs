using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BufferShuffler : MonoBehaviour
{
    public AudioClip ClipToShuffle;
    public float SecondsPerShuffle;
    public float SecondsPerCrossfade;

	public float maxClipLength;

    private AudioClip _clipToShuffle;

    private float[] _clipData = new float[0];
    private float[] _clipDataR = new float[0];
    private float[] _clipDataL = new float[0];

    private float[] _nextClipData = new float[0];
    private float[] _nextClipDataR = new float[0];
    private float[] _nextClipDataL = new float[0];

    private AudioSource _bufferShufflerAudioSource;
    private int _buffersPerShuffle;
    private int _crossFadeSamples;
    private int _currentShuffleBuffer;

    private int _shuffleCounter;
    private int _theLastStartIndex;
    private bool _firstShuffle;

    private int _startIndex;
    private int _clipLengthSamples;
    private int _clipChannels;
    private int _bufferSize;
    private bool _stereo;
    private bool _clipLoaded;
    private bool _clipSwapped;
    private int _fadeIndex;
    private int _dspSize;
    private int _qSize;
    private int _outputSampleRate;
    private int _clipSampleRate;

    private System.Random _randomGenerator;
    private const string EMPTY_AUDIO_FILE = "Audio/Vo/Empty";

	void Awake()
    {
        if (ClipToShuffle == null)
        {
            ClipToShuffle = Resources.Load(EMPTY_AUDIO_FILE) as AudioClip;
        }

		maxClipLength = ((float)Mathf.RoundToInt (ClipToShuffle.length * 10f)) / 10f - SecondsPerCrossfade * 2f;
		SecondsPerCrossfade = 0.05f;
		SecondsPerShuffle = maxClipLength * 0.9f;
	}

    public void Start()
    {
		_bufferShufflerAudioSource = GetComponent<AudioSource> ();

        _randomGenerator = new System.Random();
        AudioSettings.GetDSPBufferSize(out _dspSize, out _qSize);
        _outputSampleRate = AudioSettings.outputSampleRate;
        LoadNewClip(ClipToShuffle);
		StartCoroutine (PlaySound ());
    }

    public void SetSecondsPerShuffle(float secondsPerShuffle)
    {
        if (SecondsPerShuffle > ClipToShuffle.length)
        {
            Debug.LogError("Seconds Per Shuffle longer than length of clip. " +
                           "Seconds Per Shuffle must be less than the length of the audio clip.");
        }
        SecondsPerShuffle = secondsPerShuffle;

    }

    public void SetSecondsPerCrossfade(float secondsPerCrossfade)
    {
        if (SecondsPerCrossfade > SecondsPerShuffle/2)
        {
            Debug.LogError("Seconds Per Crossfade longer than length of shuffle. " +
                           "Seconds Per Shuffle must be less than the length of the shuffle. " +
                           "Reccomended length is <= " + SecondsPerShuffle/2);
        }
        SecondsPerCrossfade = secondsPerCrossfade;
    }

    public void LoadNewClip(AudioClip clip)
    {
        LoadNewClip(clip, SecondsPerShuffle, SecondsPerCrossfade);
    }
    public void LoadNewClip(AudioClip clip, float secondsPerShuffle)
    {
        LoadNewClip(clip, secondsPerShuffle, SecondsPerCrossfade);
    }
    public void LoadNewClip(AudioClip clip, float secondsPerShuffle, float secondsPerCrossfade)
    {
        _clipSwapped = false;
        _clipLoaded = false;
        _clipToShuffle = clip;
        ClipToShuffle = clip;
        _clipChannels = clip.channels;
        _clipLengthSamples = clip.samples;
        _clipSampleRate = clip.frequency;
        Debug.Log(clip.name);
        Debug.Log(clip.frequency);
        Debug.Log(_outputSampleRate);
        if (_clipSampleRate != _outputSampleRate)
        {
            Debug.LogError("Clip sample rate doesn't match output sample rate. Alter clip sample rate in clip import settings, or output sample rate in Project->Preferences->Audio");
            return;
        }

        _nextClipData = new float[_clipLengthSamples];
        _nextClipDataR = new float[(_clipLengthSamples / _clipChannels)];
        _nextClipDataL = new float[(_clipLengthSamples / _clipChannels)];

        _stereo = _clipChannels == 2;
        clip.GetData(_nextClipData, 0);

        if (_stereo)
        {
            for (var i = 0; i < _clipLengthSamples-1; i = i + _clipChannels)
            {
                _nextClipDataL[i / 2] = _nextClipData[i];
                _nextClipDataR[i / 2] = _nextClipData[i + 1];
            }
        }
        else
        {
            _clipToShuffle.GetData(_nextClipDataL, 0);
        }
        SetSecondsPerShuffle(secondsPerShuffle);
        SetSecondsPerCrossfade(secondsPerCrossfade);
        _clipLoaded = true;
    }

    void Update()
	{
	//	Mathf.Clamp (SecondsPerShuffle, SecondsPerCrossfade, maxClipLength);

        if (_clipToShuffle != ClipToShuffle)
        {
            LoadNewClip(ClipToShuffle);
        }


    }

    private int GetStartIndex()
    {
        if (_currentShuffleBuffer == _shuffleCounter)
        {
            _theLastStartIndex = _startIndex + (_bufferSize/_clipChannels);
            _currentShuffleBuffer = _buffersPerShuffle;
            _shuffleCounter = 0;
            int maxEndIndex = (_clipLengthSamples-((_bufferSize*_currentShuffleBuffer) + _crossFadeSamples))/_clipChannels;
            _startIndex = _randomGenerator.Next(_crossFadeSamples, maxEndIndex-1);
            _startIndex = _startIndex - _crossFadeSamples;
            _firstShuffle = true;
            _fadeIndex = 0;
            return _startIndex;
        }
        else
        {
            _shuffleCounter++;
            _startIndex = _startIndex + (_bufferSize / _clipChannels);
            _firstShuffle = false;
            return _startIndex;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
		//if clip isn't loaded, just return
        if (!_clipLoaded) return;

		//if clip is stereo, then the buffer size equals the length of the array
		//this is b/c of how audioclip array data distribution works
        if (_stereo)
        {
            _bufferSize = data.Length;
        }
        else //if the clip is mono? then the buffer size equals the length of the array divided by the number of channels
        {
            _bufferSize = data.Length / channels;
        }

        float bufferSizeInSeconds = (float)_bufferSize/(float)_outputSampleRate;
        _buffersPerShuffle = Mathf.RoundToInt(SecondsPerShuffle / bufferSizeInSeconds);
        _crossFadeSamples = _bufferSize * Mathf.RoundToInt(SecondsPerCrossfade / bufferSizeInSeconds);
        if (_clipLoaded && !_clipSwapped)
        {
            _clipData = _nextClipData;
            _clipDataR = _nextClipDataR;
            _clipDataL = _nextClipDataL;
            _shuffleCounter = _currentShuffleBuffer;
        }
        int theStartIndex = GetStartIndex();
        if (_clipLoaded && !_clipSwapped)
        {
            _fadeIndex = _crossFadeSamples;
            _clipSwapped = true;
        }
        if (!_clipSwapped) return;

		//go through the entire clip array, apply filter (shuffle around audio data)
        var clipIndex = 0;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            if ((_fadeIndex + clipIndex) < _crossFadeSamples)
            {
                int currentClipIndex = theStartIndex + clipIndex;
                int lastClipIndex = _theLastStartIndex + clipIndex;
                int progressIndex = clipIndex + _fadeIndex;
                float currentClipPercent = Mathf.Sin((0.5f * progressIndex +Mathf.PI)/_crossFadeSamples);
                float lastClipPercent = Mathf.Cos((0.5f * progressIndex +Mathf.PI)/_crossFadeSamples);
                data[i] = (_clipDataL[currentClipIndex] * currentClipPercent) + (_clipDataL[lastClipIndex] * lastClipPercent);
                if (channels == 2 && _stereo)
                    data[i + 1] = (_clipDataR[currentClipIndex]*currentClipPercent) +
                                  (_clipDataR[lastClipIndex] * lastClipPercent);
                else if (channels == 2)
                    data[i + 1] = (_clipDataL[currentClipIndex + 1] * currentClipPercent) +
                                  (_clipDataL[lastClipIndex] * lastClipPercent);
            }
            else
            {
                data[i] = _clipDataL[theStartIndex + clipIndex];
                if (channels == 2 && _stereo) data[i + 1] = _clipDataR[theStartIndex + clipIndex];
                else if (channels == 2) data[i + 1] = _clipDataL[theStartIndex + clipIndex];
            }
            clipIndex++;
        }
        _fadeIndex += clipIndex;
        _theLastStartIndex += clipIndex;
    }

	public bool soundIsPlaying(){
		return _bufferShufflerAudioSource.isPlaying;
	}

	public IEnumerator PlaySound(){
		_bufferShufflerAudioSource.Play ();
		yield return new WaitForSeconds (maxClipLength);
		_bufferShufflerAudioSource.Stop ();
		yield return new WaitForSeconds (2f);
	}

	public float CurrentAudioTime(){
		return _bufferShufflerAudioSource.time;
	}
}