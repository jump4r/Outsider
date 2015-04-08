using UnityEngine;
using System.Collections;
using System;

public class Sinus : MonoBehaviour {
	//unoptimized version
	private static System.Random RandomNumber = new System.Random();
	public double frequency = 440;
	public double frequency2;
	public double gain = 0.02;

	public double skew;
	private double increment;
	private double phase;
	private double sampling_frequency = 44100;

	void Awake() {
		skew = (float)Math.Sin((float)RandomNumber.NextDouble()*2.0f*Math.PI) * 2.0f;
		frequency2 = skew + frequency;
	}

	void OnAudioFilterRead(float[] data, int channels) {
		// update increment in case frequency has changed
		increment = (frequency + skew) * 2 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels) {
			phase = phase + increment;
			// this is where we copy audio data to make them 
			// "available" to Unity
			data[i] = data[i] * (float)(gain * Math.Sin(phase));
			// if we have stero, we copy the mono data to each channel
			if (channels == 2) data[i + 1] = data[i];
			if (phase > 2 * Math.PI) phase = 0;
		}
	}
}
