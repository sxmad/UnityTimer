# UnityTimer
timer util for unity, easy to use timer.

### Timer Util

```c#
1.HasTimer(id);
2.AddTimer(float _rate, Action _callBack);
3.AddTimer(float _rate, int _ticks, Action _callBack, Action _completeCallback = null);
4.AddTimer(float _rate, Action<object[]> _callBack, params object[] _args);
5.AddTimer(float _rate, int _ticks, Action<object[]> _callBack, params object[] _args);
6.RemoveTimer(int _timerId);
```

### Singleton Util

```
// use XMonoSingleton
public class XTime : XMonoSingleton<XTime>{
	// ...
}

// use XSingleton
public class XTime : XSingleton<XTime>{
	// ...
}
```

