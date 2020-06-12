# MultiThread Face Detection C++

This program show dnn face detection does NOT work on multi-thread.

## How to use?

## 1. Build

1. Type the following command
````
$ pwsh BuildBoost.ps1
$ pwsh Build.ps1
````

## 2. Run

### Single Thread

````
$ git lfs pull
$ pwsh .\Run.ps1 1
deserialize: mmod_human_face_detector.dat
thread_num: 1
load_image: 2007_007763.jpg
prepare thread
wait all thread
start thread_id: 0
[(2488, 1899) (2828, 2239)]
[(737, 1543) (1020, 1826)]
[(3107, 695) (3447, 1035)]
[(2371, 683) (2654, 966)]
[(1450, 1742) (1686, 1978)]
[(1597, 740) (1880, 1023)]
[(1306, 954) (1542, 1190)]
finish thread_id: 0
finish all thread
````

### Multi Thread

````
$ git lfs pull
$ pwsh .\Run.ps1 2
deserialize: mmod_human_face_detector.dat
thread_num: 2
load_image: 2007_007763.jpg
prepare thread
wait all thread
start thread_id: 0
start thread_id: 1
````