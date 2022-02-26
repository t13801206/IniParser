# IniParser

.INIファイルを読み書きするライブラリ

![Animation](https://user-images.githubusercontent.com/40709570/155830237-22809b71-4848-459a-bad8-eae7d44344cb.gif)

## Settings.ini

```ini
[Section0]

;comment key0
Key0 = v0

;comment key1
;hello
Key1 = v1

;comment key2
;string
Key2 = v2

[Section1]

;comment key3
Key3 = 304

;comment key4
Key4 = 450

[Section2]

;this must double
Key6 = 1.23

;Key7 comment
Key7 = 3.45

;this must double
Key8 = 4.56

[Person]

;this is name
;ex Taro Jiro Ichiro
Name = hollywood

;this type must int
Age = 188
```


## Dependency

- https://github.com/runceel/ReactiveProperty 
- https://github.com/rickyah/ini-parser