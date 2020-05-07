using UnityEngine;
using System.Collections;

public class NotificationCenter {

    // Глобальные нотификации

    private static NotificationCenter Mgr = new NotificationCenter();
    public static NotificationCenter GetI { get { return Mgr; } }

    public delegate void callback(Object arg);

    public Hashtable callback_storage = new Hashtable();

    public NotificationCenter() {
        Mgr=this;
    }

    public void addCallback(string notificationName, callback method_in) {
        // Если уже содержит такое имя события
        if (callback_storage.ContainsKey(notificationName)) {
            // если на такое имя события нету такого же метода
            if (!((ArrayList)callback_storage[notificationName]).Contains(method_in)) {
                // то добавляем новый метод для этого события
                ((ArrayList)callback_storage[notificationName]).Add(method_in);
            }
        // Если такого события нету
        } else {
            // Добавляем событие и пустой массив методов для него
            callback_storage.Add(notificationName, new ArrayList());
            ((ArrayList)callback_storage[notificationName]).Add(method_in);
        }
    }

    public void removeCallback(string notificationName, callback method_in) {
        // Если событие есть
        if (callback_storage.ContainsKey(notificationName)) {
            // Если у этого события есть такой же метод
            if (((ArrayList)callback_storage[notificationName]).Contains(method_in)) {
                // Удаляем этот метод
                ((ArrayList)callback_storage[notificationName]).Remove(method_in);
            }
        }
    }

    public void postNotification(string notificationName, Object arg) {
        if (callback_storage.ContainsKey(notificationName)) {
            int i;
            callback function;
            int startLenght = ((ArrayList)callback_storage[notificationName]).Count;
            int curLenght = startLenght, delta=0;
            for (i=0; i< startLenght; i++) {
                function=((ArrayList)callback_storage[notificationName])[i] as callback;
                function(arg);
                // Проверка на случай изменения размера массива и предотвратить "перепрыгивание" (если перед этим было вызвано removeCallback) ************************
                curLenght = ((ArrayList)callback_storage[notificationName]).Count;
                delta = startLenght - curLenght;
                if (delta>0) {
                    i -= delta;
                    startLenght = curLenght;
                }
            }
        }
    }

    public void clearCallbacks() {
        callback_storage.Clear();
    }
}