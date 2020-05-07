using System;
using System.Collections.Generic;
using UnityEngine;

// Наследуемся, чтоб вешать листерны и создавать нотификации

public class EventDispatcher : MonoBehaviour {

    public enum EVENTS { };
    Dictionary<string, Action<GameObject>> eventDictionary = new Dictionary<string, Action<GameObject>>();
	static Dictionary<string, Action<GameObject>> eventGlobalDictionary = new Dictionary<string, Action<GameObject>>();

	public void AddListener<T>(T eventName, Action<GameObject> listener) {
		_AddListener(eventName.ToString(), listener, eventDictionary);

	}

	public static void AddGlobalListener<T>(T eventName, Action<GameObject> listener) {
		_AddListener(eventName.ToString(), listener, eventGlobalDictionary);
	}

	static void _AddListener(string eventName, Action<GameObject> listener, Dictionary<string, Action<GameObject>> dictionary) {
		_RemoveListener(eventName, listener, dictionary);

		if (!dictionary.ContainsKey(eventName)) {
			dictionary.Add(eventName, default);
		}

		dictionary[eventName] += listener;
	}

	public void RemoveListener<T>(T eventName, Action<GameObject> listener = null) {
		_RemoveListener(eventName.ToString(), listener, eventDictionary);
	}

	public static void RemoveGlobalListener<T>(T eventName, Action<GameObject> listener = null) {
		_RemoveListener(eventName.ToString(), listener, eventGlobalDictionary);
	}

	static void _RemoveListener(string eventName, Action<GameObject> listener, Dictionary<string, Action<GameObject>> dictionary) {
		if (dictionary.ContainsKey(eventName)) {
			if (listener != null) {
				dictionary[eventName] -= listener;
			} else {
				dictionary.Remove(eventName);
			}
		}
	}

	public void RemoveAllListeners() {
		_RemoveAllListeners(eventDictionary);
	}

	public static void RemoveAllGlobalListeners() {
		_RemoveAllListeners(eventGlobalDictionary);
	}

	static void _RemoveAllListeners(Dictionary<string, Action<GameObject>> dictionary) {
		dictionary.Clear();
	}

	public void DispatchEvent<T>(T eventName, GameObject go = null) {
		_DispatchEvent(eventName.ToString(), go, eventDictionary);
	}

	public static void DispatchGlobalEvent<T>(T eventName, GameObject go = null) {
		_DispatchEvent(eventName.ToString(), go, eventGlobalDictionary);
	}

	static void _DispatchEvent(string eventName, GameObject go, Dictionary<string, Action<GameObject>> dictionary) {
		if (dictionary.ContainsKey(eventName)) {
			dictionary[eventName]?.Invoke(go);
		}
	}

	private void OnDestroy() {
		RemoveAllListeners();
	}
}