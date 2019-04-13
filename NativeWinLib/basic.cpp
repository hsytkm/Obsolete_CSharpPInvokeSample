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

// Integer(In/Out) 加算
DllExport int AddIntegerFromLib(int x, int y) {
	return x + y;
}
	
// bool(In/Out) 論理積
DllExport bool GetLogicalConjunctionFromLib(bool b1, bool b2) {
	return b1 & b2;
}

// string(In/Out) 大文字化
DllExport void ToUpperFromLib(const char* src, char* dest, int destLength) {
	for (int i = 0; i <= strlen(src); i++) {
		dest[i] = toupper(src[i]);
	}
}

// byte[](In)
DllExport int InByteArrayLib(unsigned char* data, int dataLength) {
	if (dataLength != 4) return 0;
	return (data[3] << 24) | (data[2] << 16) | (data[1] << 8) | data[0];
}

// return string
DllExport bool GetStringFromLib(char* dst, int dstlength) {
	if (dstlength < 1) return false;

	for (int i = 0; i < dstlength; i++) {
		dst[i] = '0' + (i % 10);
	}
	return true;
}
