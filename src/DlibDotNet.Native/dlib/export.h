#ifdef _WINDOWS 
#define DLLEXPORT extern "C" __declspec(dllexport)
#else 
#define DLLEXPORT extern "C"
// size_t is missing for non windows system
#include <sys/types.h>
#endif