#ifdef _WINDOWS 
#define DLLEXPORT extern "C" __declspec(dllexport)
#else 
#define DLLEXPORT extern "C"
#endif