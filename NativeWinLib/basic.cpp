#include "pch.h"
#include <iostream>

#ifdef __cplusplus
#define DllExport extern "C" __declspec(dllexport)
#else
#define DllExport __declspec(dllexport)
#endif

// return integer
DllExport int GetIntFromLib() {
	return 1234;
}

DllExport int AddIntegerFromLib(int x, int y) {
	return x + y;
}
	
// 論理積
DllExport bool GetLogicalConjunctionFromLib(bool b1, bool b2) {
	return b1 & b2;
}

// 論理積
DllExport void ToUpperFromLib(const char* src, char* dest, int destLength) {
	for (int i = 0; i <= strlen(src); i++) {
		dest[i] = toupper(src[i]);
	}
}

// return string
DllExport bool GetStringFromLib(char* data, int buflength) {
	if (buflength < 1) return false;

	for (int i = 0; i < buflength; i++) {
		data[i] = '0' + (i % 10);
	}
	return true;
}
