using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Запуск корутины-задержки вызова функции

public class ClassDelay  {

    public delegate void CallBack();

    // задержка Фреймов
    public static Coroutine DelayFramesCallBack(MonoBehaviour behavior, int delayFrames, CallBack callBack) {
        return behavior.StartCoroutine(Routine());
        IEnumerator Routine() {
            while (--delayFrames>=0)  yield return new WaitForFixedUpdate();
            callBack?.Invoke();
        }
    }

    // задержка в секундах
    public static Coroutine DelaySecondCallBack(MonoBehaviour behavior, float delaySecond, CallBack callBack) {
        return behavior.StartCoroutine(RoutineSecond());
        IEnumerator RoutineSecond() {
            yield return new WaitForSeconds(delaySecond);
            callBack?.Invoke();
        }
    }

}
